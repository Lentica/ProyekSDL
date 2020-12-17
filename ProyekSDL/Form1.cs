using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyekSDL
{
    public partial class Form1 : Form
    {
        AVLTree avl;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //buka file text
            OpenFileDialog fileOpener = new OpenFileDialog();

            //set property dari openfiledialog
            fileOpener.DefaultExt = "txt";
            fileOpener.Title = "buka file test case";
            fileOpener.Filter = "text files (*.txt)|*.txt|All Files (*.*)|*.*";
            fileOpener.FilterIndex = 1;
            fileOpener.CheckFileExists = true;
            fileOpener.CheckPathExists = true;

            //jalankan bila dialog telah selesai dibuka dan hasilnya adalah "OK"
            if (fileOpener.ShowDialog() == DialogResult.OK)
            {
                //tunjukan path dari text file pada label
                lblTestCase.Text = fileOpener.FileName;

                //baca setiap line dari text file dan masukkan ke array
                string[] testCaseLines = System.IO.File.ReadAllLines(@fileOpener.FileName);

                //array untuk memisah command, key, value dari setiap line
                string[] commands = new string[3];

                //baca setiap line yg didapat dari text file dan
                //lakukan split dari setiap line berdasarkan char whitespace
                for (int i = 0; i < testCaseLines.Length; i++)
                {
                    commands = testCaseLines[i].Split(' ');//split berdasarkan whitespace

                    //operationModeSelector digunakan untuk memilih mode operasi
                    //yaitu mode avl tree atau red black tree
                    //keyword untuk avl adalah "avl"
                    //sedangkan red black tree adalah "rbt". penggunaan= operationModeSelector(mode, command, key, value);
                    string mode = cbMode.Text;//mode diambil dari comboBox cbMode

                    if (commands[0] == "D")//delete
                        operationModeSelector(mode, commands[0], int.Parse(commands[1]));
                    else if (commands[0] == "T")//traverse
                        operationModeSelector(mode, commands[0]);
                    else if (commands[0] == "S")//search
                        operationModeSelector(mode, commands[0], int.Parse(commands[1]));
                    else
                        operationModeSelector(mode, commands[0], int.Parse(commands[1]), commands[2]);
                }
            }
        }

        void operationModeSelector(string mode, string command, int key=0, string value = "")
        {
            if (command == "I")
            {
                //Insert
                if (mode == "rbt")
                {
                    //Red Black Tree
                    MessageBox.Show("rbt Insert to node with ***key: " + key + "*** the value: " + value);
                }
                else
                {   //AVL Tree
                    //MessageBox.Show("avl Insert to node with key: " + key + " the value: " + value);
                    rtbDisplay.Text += "avl Insert node with key: ***" + key + "*** the value: " + value+"\n\n";
                    avl.root = avl.insert(avl.root, key, value);
                    avl.status += "+++-----------------------------------------------------+++";
                    rtbDisplay.Text += avl.status + "\n\n";
                    avl.status = "";
                    //rtbDisplay.Text += statusAVL + "\n";

                }
            }
            else if (command == "U")
            {
                //Update
                if (mode == "rbt")
                {
                    MessageBox.Show("rbt Update node with key: " + key + " with the value: " + value);
                }
                else
                {
                    MessageBox.Show("avl Update node with key: " + key + " with the value: " + value);
                }
            }
            else if(command == "S")
            {
                //Search
                if (mode == "rbt")
                {
                    MessageBox.Show("rbt Delete node with key: " + key);
                }
                else
                {
                    //AVL Tree
                    //MessageBox.Show("avl Insert to node with key: " + key + " the value: " + value);
                    rtbDisplay.Text += "avl search node with key: " + key + "\n";
                    Node res = avl.find(avl.root, key);
                    if (res == null) { rtbDisplay.Text += "key not found" + "\n"; }
                    else
                    {
                        rtbDisplay.Text += "node found with key " + res.key + " and value = " + res.value  + "\n";
                    }
                }
            }
            else if (command == "D")
            {
                //Delete
                if (mode == "rbt")
                {
                    MessageBox.Show("rbt Delete node with key: " + key);
                }
                else
                {
                    //AVL Tree
                    //MessageBox.Show("avl Insert to node with key: " + key + " the value: " + value);
                    rtbDisplay.Text += "avl delete node with key: " + key + "\n";
                    avl.root = avl.deleteNode(avl.root, key);


                    //Node res = avl.delete(avl.root, key);
                    //if(res == null) { rtbDisplay.Text += "key not found" + "\n"; }
                    //else
                    //{
                    //    rtbDisplay.Text += "node deleted" + "\n";
                    //}
                }
            }
            else if (command == "T")
            {
                //Traverse
                if (mode == "rbt")
                {
                    MessageBox.Show("RBT WARP MODE ON! STARTING TRAVERSAL! ");
                }
                else
                {
                    rtbDisplay.Text += "\n";
                    MessageBox.Show("AVL WARP MODE ON! STARTING TRAVERSAL! ");
                    rtbDisplay.Text += "In-Order Traversal Result: \n\n";
                    inOrder(avl.root);
                    rtbDisplay.Text += "\n\n\n";

                    rtbDisplay.Text += "\n";
                    MessageBox.Show("AVL WARP MODE ON! STARTING TRAVERSAL! ");
                    rtbDisplay.Text += "Pre-Order Traversal Result: \n\n";
                    preOrder(avl.root);
                    rtbDisplay.Text += "\n\n\n";
                }
            }
        }

        //traverse AVL
        void preOrder(Node node)
        {
            if (node != null)
            {
                rtbDisplay.Text += "key: "+node.key + " value: "+node.value+" // ";
                preOrder(node.left);
                preOrder(node.right);
            }
        }
        void inOrder(Node node)
        {
            if (node != null)
            {
                inOrder(node.left);
                rtbDisplay.Text += "key: " + node.key + " ++ value: " + node.value + " || ";
                inOrder(node.right);
            }
        }
        void postOrder(Node node)
        {
            if (node != null)
            {
                postOrder(node.left);
                postOrder(node.right);
                rtbDisplay.Text += "key: " + node.key + " value: " + node.value + " // ";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbMode.SelectedIndex = 1; 
            avl = new AVLTree();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            rtbDisplay.Clear();
        }
    }
}
