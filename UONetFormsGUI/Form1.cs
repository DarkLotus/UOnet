using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UONetFormsGUI
{
    public partial class Form1 : Form
    {
        Globals globals;
        Thread t;
        public Form1()
        {
            InitializeComponent();
            globals = new Globals { UO = new uoNet.UO() };
            globals.UO.Open(2);
            t = new Thread(new ThreadStart(refresh));
            t.Start();
            ToolStripDropDown dropdown = new ToolStripDropDown();
            for (int i = 1; i <= globals.UO.CliCnt;i++) {
                var toolstripItem = new ToolStripMenuItem("Client: " + i);
                toolstripItem.Click += (e, v) => { globals.UO.CliNr = i; };
                dropdown.Items.Add(toolstripItem);
            }
            switchClientToolStripMenuItem.DropDown = dropdown;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (t != null && t.IsAlive)
                t.Abort();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
           
        }
        private void refresh()
        {
            Thread.Sleep(100);
            if(treeView1.Nodes.Count == 0 )
            {
                List<TreeNode> nodes = new List<TreeNode>();
                PropertyInfo[] properties = typeof(uoNet.UO).GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    var node = new TreeNode(property.Name + ": " + property.GetValue(globals.UO));
                    node.Tag = property;
                    nodes.Add(node);
                }
                MethodInvoker doit = delegate {
                   treeView1.Nodes.AddRange(nodes.ToArray());
                };
                treeView1.Invoke(doit);
            }
            else
            {

                MethodInvoker doit = delegate {
                    treeView1.BeginUpdate();
                    foreach (var n in treeView1.Nodes)
                    {
                        var node = (n as TreeNode);
                        var index = treeView1.Nodes.IndexOf(node);
                        treeView1.Nodes.Remove(node);
                        var prop = (node.Tag as PropertyInfo);
                        node.Text = prop.Name + ": " + prop.GetValue(globals.UO);


                        treeView1.Nodes.Insert(index, node);
                    }
                    treeView1.Update();
                    treeView1.EndUpdate();

                };
                treeView1.Invoke(doit);
            }
            
            

            refresh();
        }

        private async void btnStartStop_Click(object sender, EventArgs e)
        {
            
            
            try
            {
                var script = CSharpScript.Create<string>("", null, typeof(uoNet.UO)).ContinueWith(txtScriptInput.Text);
                var result = await script.RunAsync(new uoNet.UO());
                
                //var result = await CSharpScript.EvaluateAsync("UO.Open();", globals: globals);
                MethodInvoker doit = delegate { txtDebugOutput.Text = DateTime.Now.ToShortTimeString() + " : " + result.ReturnValue + "\r\n" + txtDebugOutput.Text; };
                txtDebugOutput.Invoke(doit);
            }
            catch (CompilationErrorException err)
            {
                MethodInvoker doit = delegate { txtDebugOutput.Text = DateTime.Now.ToShortTimeString() + " : " + err.Message + "\r\n" + txtDebugOutput.Text; };
                txtDebugOutput.Invoke(doit);
            }
        }

        public class Globals
        {
            public uoNet.UO UO;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Set filter options and filter index.
            openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;

            openFileDialog1.Multiselect = true;

            // Call the ShowDialog method to show the dialog box.
            var userClickedOK = openFileDialog1.ShowDialog();

            // Process input if the user clicked OK.
            if (userClickedOK.HasFlag(DialogResult.OK))
            {
                // Open the selected file to read.
                System.IO.Stream fileStream = openFileDialog1.OpenFile();

                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                {
                    // Read the first line from the file and write it the textbox.
                    txtScriptInput.Text = reader.ReadToEnd();
                }
                fileStream.Close();
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text|*.txt";
            saveFileDialog1.Title = "Save a File";
            saveFileDialog1.ShowDialog();

            // If the file name is not an empty string open it for saving.
            if (saveFileDialog1.FileName != "")
            {
                // Saves the Image via a FileStream created by the OpenFile method.
                System.IO.FileStream fs =
                   (System.IO.FileStream)saveFileDialog1.OpenFile();
                // Saves the Image in the appropriate ImageFormat based upon the
                // File type selected in the dialog box.
                // NOTE that the FilterIndex property is one-based.
                txtScriptInput.SaveFile(fs, RichTextBoxStreamType.PlainText);
                fs.Flush();
                fs.Close();
            }
        }
    }
}
