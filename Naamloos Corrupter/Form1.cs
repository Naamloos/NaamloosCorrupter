using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Naamloos_Corrupter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var FD = new System.Windows.Forms.OpenFileDialog();
            if (FD.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileToOpen = FD.FileName;
                textBox1.Text = fileToOpen;
            }
        }

        public static decimal repeat = 0;
        public static decimal amount = 0;
        public static decimal skip = 0;
        public static bool randomizeAmount = false;
        public static bool randomizeRepeat = false;
        public static string filepath;
        public static byte[] file;
        public static int progressint = 0;
        public static int addval = 0;

        private void button2_Click(object sender, EventArgs e)
        {

            repeat = numericUpDown1.Value;
            amount = numericUpDown2.Value;
            skip = numericUpDown3.Value;

            randomizeAmount = checkBox1.Checked;

            filepath = textBox1.Text;
            file = File.ReadAllBytes(filepath);

            corrupt();
        }

        public void corrupt()
        {
            addval = file.Length / 100;
            byte[] newfile = new byte[file.Length];
            int i = 0;
            decimal thisrepeat = 0;
            while(i < file.Length)
            {
                if (randomizeAmount)
                {
                    Random r = new Random();
                    amount = r.Next(0, 255);
                }

                byte b = file[i];
                if (i > skip)
                {
                    thisrepeat++;
                    if (thisrepeat == repeat)
                    {
                        thisrepeat = 0;
                        decimal ii = 0;
                        while (ii < amount)
                        {
                            int thisval = Convert.ToInt32(amount);
                            b = Convert.ToByte(thisval);
                            ii++;
                        }
                    }
                }
                newfile[i] = b;
                i++;
            }
            string ext = filepath.Substring(filepath.LastIndexOf('.'));
            if(File.Exists(Path.Combine(Environment.CurrentDirectory, "corrupted" + ext)))
            {
                File.Delete(Path.Combine(Environment.CurrentDirectory, "corrupted" + ext));
            }
            File.Create(Path.Combine(Environment.CurrentDirectory, "corrupted"+ ext)).Close();
            File.WriteAllBytes(Path.Combine(Environment.CurrentDirectory, "corrupted" + ext), newfile);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string msg = "How to use:";
            msg += "\nPath: select a file path with the 'Browse' button.";
            msg += "\nRepeat every: Every *th byte will be changed";
            msg += "\nAmount: New value of these bytes";
            msg += "\nSkip bytes: Bytes at begin of file that will be skipped";

            MessageBox.Show(msg, "How to use");
        }
    }
}
