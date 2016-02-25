using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using UOProxy.Packets.FromServer;

namespace UOProxy
{
    public partial class UOProxy
    {
        
        public TcpListener tcpListener;
        Thread ClientComThread;
        public static bool ProxyMode = false;
        //When using as a proxy, Call this method, Starts listening for local client connection, once client connects, sends connect to Server
        public bool StartListeningForClient(int port)
        {
            try
            {
                SetupHandlers();
                
                tcpListener = new TcpListener(IPAddress.Any, port);
                tcpListener.Start();
                tcpListener.BeginAcceptTcpClient(new AsyncCallback(this.AcceptClientConnection), tcpListener);
                //var client = tcpListener.AcceptTcpClient();
                //AcceptClientConnection(client);
                Logger.Log("Listening for incoming Client connections on: " + IPAddress.Loopback + ":" + port);
                ProxyMode = true;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void AcceptClientConnection(IAsyncResult ar)
        {
            TcpListener client = (TcpListener)ar.AsyncState;
            Logger.Log("Accepted Client Connection, Starting message loop and connecting to server");
            ClientComThread = new Thread(new ParameterizedThreadStart(HandleClientCom));
            TcpClient UOClient = client.EndAcceptTcpClient(ar);
            ClientComThread.Start(UOClient);
            client.BeginAcceptTcpClient(new AsyncCallback(this.AcceptClientConnection), tcpListener);
            
        }


        private void HandleClientCom(object Client)
        {
            TcpClient client = (TcpClient)Client;
            NetworkStream ClientStream = client.GetStream();
            TcpClient Server = ConnectToServer("192.95.29.83", 2593, client);
            byte[] data = new byte[4096];
            while (client.Connected)
            {
                Thread.Sleep(5);
                //Message pump for comm from/to client
                
                if (client.Available <= 0)
                    continue;
                int bytesRead = ClientStream.Read(data, 0, client.Available);
                if (data[0] != 0xBF)
                    HandleClientPacket(data, bytesRead);
                //Logger.Log("From Client: " + BitConverter.ToString(data, 0, bytesRead));
                //Todo parse packet stream, ability to filter certain packet.
                Server.GetStream().Write(data, 0, bytesRead);
                Server.GetStream().Flush();
            }
            if(Server.Connected)
            Server.Close();
        }

      
        byte[] TempdataBuffer = new byte[8192];
        public static bool UseHuffman = false;
        private void HandleServerCom(object cliserv)
        {
            List<byte> IncomingQueue = new List<byte>();
            ClientServer TcpClients = (ClientServer)cliserv;
            HuffmanDecompression Decompressor = new HuffmanDecompression();
            NetworkStream ServerStream = TcpClients.server.GetStream();
            
            while (TcpClients.server.Connected)
            {
                //MessagePump from/to Server
                Thread.Sleep(5);
                if (TcpClients.server.Available <= 0)
                    continue;
                int bytesRead = ServerStream.Read(TempdataBuffer, 0, TcpClients.server.Available);

                byte[] data = new byte[bytesRead];
                Array.Copy(TempdataBuffer, 0, data, 0, bytesRead);

                if (TcpClients.client != null && UseHuffman && TcpClients.client.Connected)
                    TcpClients.client.GetStream().Write(data, 0, bytesRead);// this is here so we send uncompressed for now, No compress method

                if (IncomingQueue.Count > 0)
                {
                    IncomingQueue.AddRange(data); 
                    data = IncomingQueue.ToArray();
                    IncomingQueue = new List<byte>();
                    bytesRead = data.Length;
                }
                if (UseHuffman)
                {
                    
                    byte[] dest = new byte[4096];
                    int destSize = 0;
                    int datalen = data.Length;
                    while (Decompressor.DecompressOnePacket(ref data, bytesRead, ref dest, ref destSize))
                    {                        
                        byte[] destTrimmed = new byte[destSize];
                        Array.Copy(dest, 0, destTrimmed, 0, destSize);
                        //Logger.Log("From Server DeHuffed: " + BitConverter.ToString(destTrimmed, 0, destSize));
                        HandlePacketFromServer(destTrimmed, TcpClients.client);
                        bytesRead = data.Length;
                    }
                    if (data.Length > 0)
                    {
                        //Logger.Log("NoFull Packets adding " + data.Count() + " to queue");
                        IncomingQueue.AddRange(data);
                    }
                }
                else
                {
                    HandlePacketFromServer(data, TcpClients.client);
                    Logger.Log("From Server NoHuff: " + BitConverter.ToString(data, 0, bytesRead));

                    if (TcpClients.client != null)
                    TcpClients.client.GetStream().Write(data, 0, bytesRead);
                    if (data[0] == 0x8c)
                    {
                        TcpClients.server.Close();
                    }
                }

            }
        }

       

        // Connects to a server and returns Server TcpClient if sucessful, Called when using in Nonproxy mode.
        public TcpClient ConnectToServer(string ip, int port)
        {
            try
            {
                TcpClient Server = new TcpClient();
                Server.Connect(IPAddress.Parse(ip), port);
                Thread ServerComThread = new Thread(new ParameterizedThreadStart(HandleServerCom));
                ServerComThread.Start(new ClientServer(null, Server));
                return Server;
            }
            catch
            {
                return null;
            }
            
        }
        private TcpClient ConnectToServer(string ip, int port,TcpClient Client)
        {
            TcpClient Server = new TcpClient();
            Server.Connect(IPAddress.Parse(ip), port);
            Thread ServerComThread = new Thread(new ParameterizedThreadStart(HandleServerCom));
            ServerComThread.Start(new ClientServer(Client, Server));
            return Server;

        }

     

        private class ClientServer
        {
            public TcpClient client;
        public TcpClient server;
        public ClientServer(TcpClient Client, TcpClient Server)
        {
            client = Client;
            server = Server;
        }
        }

     
    }
}
/*
 Proxy function passes thru all messages from/to real client
 * 
 
 
 
 */