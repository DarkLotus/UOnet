using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//Using some code from UltimaXNA
namespace UOProxy.Packets.FromServer
{
    public class _0xDDCompressedGump : Packet
    {
        short length_;
        public int PlayerID;
        public int GumpID, X, Y;

        int compressedGumpLength, decompressedGumpLength;

        public readonly string[] TextLines;
        public _0xDDCompressedGump(UOStream Data)
            : base(Data)
        {
            try
            {
                length_ = Data.ReadShort();
                PlayerID = Data.ReadInt();
                GumpID = Data.ReadInt();
                X = Data.ReadInt();
                Y = Data.ReadInt();
                compressedGumpLength = Data.ReadInt() - 4;
                decompressedGumpLength = Data.ReadInt();
                if (compressedGumpLength > 1)
                {
                    byte[] compressedGumpData = new byte[compressedGumpLength];
                    Data.Read(compressedGumpData, 0, compressedGumpLength);
                    byte[] decompressedData = new byte[decompressedGumpLength];
                    OpenUO.Core.IO.ZlibCompression.Unpack(decompressedData, ref decompressedGumpLength, compressedGumpData, compressedGumpLength);
                    string GumpData = Encoding.ASCII.GetString(decompressedData);

                    int numTextLines = Data.ReadInt();
                    int compressedTextLength = Data.ReadInt() - 4;
                    int decompressedTextLength = Data.ReadInt();
                    byte[] decompressedText = new byte[decompressedTextLength];
                    if (numTextLines > 0 && decompressedTextLength > 0)
                    {
                        byte[] compressedTextData = new byte[compressedTextLength];
                        Data.Read(compressedTextData, 0, compressedTextLength);
                        OpenUO.Core.IO.ZlibCompression.Unpack(decompressedText, ref decompressedTextLength, compressedTextData, compressedTextLength);
                        int index = 0;
                        List<string> lines = new List<string>();
                        for (int i = 0; i < numTextLines; i++)
                        {
                            int length = decompressedText[index] * 256 + decompressedText[index + 1];
                            index += 2;
                            byte[] b = new byte[length * 2];
                            Array.Copy(decompressedText, index, b, 0, length * 2);
                            index += length * 2;
                            lines.Add(Encoding.BigEndianUnicode.GetString(b));
                        }
                        TextLines = lines.ToArray();
                    }
                    else
                    {
                        TextLines = new string[0];
                    }
                
                }


                /*NumberTextLines = Data.ReadInt();
                CompressedTextLen = Data.ReadInt() - 4;
                DecompressedTextLen = Data.ReadInt();
                if (CompressedTextLen > 0)
                {
                    GumpTextData = new byte[CompressedTextLen];
                    Data.Read(GumpTextData, 0, CompressedTextLen);
                }
                */

                //byte[] UncompressedGumpData = new byte[decompressedGumpLength];
                //OpenUO.Core.IO.ZlibCompression.Unpack(UncompressedGumpData,ref decompressedGumpLength,GumpData,compressedGumpLength);

                /*System.IO.MemoryStream outstream = new System.IO.MemoryStream();
                zlib.ZOutputStream outZstream = new zlib.ZOutputStream(outstream);
                System.IO.MemoryStream input = new System.IO.MemoryStream(GumpData);
                CopyStream(input, outZstream);
                outZstream.finish();
                outZstream.end();
                byte[] UncompressedGumpData = new byte[outZstream.TotalOut];
                outZstream.Position = 0;
                outZstream.Read(UncompressedGumpData, 0, (int)outZstream.TotalOut);
                */
                //byte[] UncompressedGumpText = new byte[DecompressedTextLen];
                //OpenUO.Core.IO.ZlibCompression.Unpack(UncompressedGumpText,ref DecompressedTextLen,GumpTextData,DecompressedTextLen);
            }
            catch (Exception e)
            {
                Logger.Log(e.ToString());
            }
            
        }

        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
    }
}
