using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Parsing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int formcount = 0;
        private string CalculateTheProcessPriority0(string txtprnt)
        {
            string txt1 = "", txt2 = "";
            string oprtr="";
            int begini=0, endi=0;
            for (int i = 0; i < txtprnt.Length; i++)
            {
                if (i==0&&txtprnt[i]=='-')
                {
                    txt1 = "-";
                    i++;
                }
                if (txtprnt[i] == '/'||txtprnt[i] == '*')
                {
                    if (oprtr=="")
                    {
                        oprtr = Convert.ToString(txtprnt[i]);
                        if (txtprnt[++i] == '-')
                        {
                            txt2 = "-";
                            i++;
                        }
                    }
                    else
                    {
                        txtprnt = txtprnt.Remove(begini, endi-begini + 1).Insert(begini, calculator(Convert.ToDouble(txt1), oprtr, Convert.ToDouble(txt2)));
                        txt1 = "";
                        txt2 = "";
                        begini = 0;
                        i = -1;
                    }
                }
                if (i!=-1&&( txtprnt[i] == '+'||txtprnt[i] == '-'))
                {
                    if (oprtr=="")
                    {
                        txt1 = "";
                        if (txtprnt[i+1] == '-')
                        {
                            txt1 = "-";
                            i++;
                        }
                        begini = i++;
                    }
                    else
                    {
                        txtprnt = txtprnt.Remove(begini, endi-begini + 1).Insert(begini, calculator(Convert.ToDouble(txt1), oprtr, Convert.ToDouble(txt2)));
                        txt1 = "";
                        txt2 = "";
                        begini = 0;
                        i = -1;
                    }
                }
                if (i!=-1)
                {
                    if (oprtr == "")//sayıları ata
                    {
                        txt1 += txtprnt[i];
                    }
                    else if (txtprnt[i] != '+' && txtprnt[i] != '-' && txtprnt[i] != '*' && txtprnt[i] != '/')
                    {
                        txt2 += txtprnt[i];
                        endi = i;
                    }
                }
                else
                {
                    oprtr = "";
                }
                
            }
            if (oprtr!="")
            {
                txtprnt = txtprnt.Remove(begini, endi-begini + 1).Insert(begini, calculator(Convert.ToDouble(txt1), oprtr, Convert.ToDouble(txt2)));
            }
            return CalculateTheProcessPriority1(txtprnt);
        }
        private string CalculateTheProcessPriority1(string txtprnt)
        {
            string txt1 = "", txt2 = "";
            string oprtr="";
            int begini=0, endi=0;
            for (int i = 0; i < txtprnt.Length; i++)
            {
                if (i == 0 && txtprnt[i] == '-')
                {
                    txt1 = "-";
                    i++;
                }
                if (txtprnt[i] == '+' || txtprnt[i] == '-')
                {
                    if (oprtr=="")
                    {
                        oprtr = Convert.ToString(txtprnt[i]);
                        if (txtprnt[++i] == '-')
                        {
                            txt2 = "-";
                            i++;
                        }
                    }
                    else
                    {
                        txtprnt = txtprnt.Remove(begini, endi-begini + 1).Insert(begini, calculator(Convert.ToDouble(txt1), oprtr, Convert.ToDouble(txt2)));
                        txt1 = "";
                        txt2 = "";
                        begini = 0;
                        i = -1;
                    }
                }
                if (i!=-1)
                {
                    if (oprtr == "")
                    {
                        txt1 += txtprnt[i];
                    }
                    else if (txtprnt[i] != '+' && txtprnt[i] != '-')
                    {
                        txt2 += txtprnt[i];
                        endi = i;
                    }
                }
                else
                {
                    oprtr = "";
                }
            }
            if (oprtr!="")
            {
                txtprnt = txtprnt.Remove(begini, endi-begini + 1).Insert(begini, calculator(Convert.ToDouble(txt1), oprtr, Convert.ToDouble(txt2)));
            }
            return txtprnt;
        }
        private string calculator(double n1, string oprtr, double n2)
        {
            switch (oprtr)
            {
                case "+":
                    return Convert.ToString(n1 + n2);
                case "-":
                    return Convert.ToString(n1 - n2);
                case "*":
                    return Convert.ToString(n1 * n2);
                case "/":
                    return Convert.ToString(n1 / n2);
            }
            return "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            string txt = textBox1.Text;
            string txt0 = txt;
            int prntopn = 0, prntcls = 0;
            bool fopn = false, fcls = false;
            listBox1.Items.Clear();
            label1.Text = "=";
            toolStripProgressBar1.Maximum = txt.Length;
            try
            {
                for (int i = 0; i < txt.Length; i++)
                {
                    if (txt[i] == '(' && fcls == false)
                    {
                        prntopn = i;
                        fopn = true;
                    }
                    if (txt[i] == ')' && fopn)
                    {
                        prntcls = i - (prntopn + 1);
                        fcls = true;
                    }
                    if (fopn && fcls)
                    {
                        txt = txt.Remove(prntopn, prntcls + 2).Insert(prntopn, CalculateTheProcessPriority0(txt.Substring(prntopn + 1, prntcls)));
                        listBox1.Items.Add(txt);
                        prntopn = 0;
                        prntcls = 0;
                        fopn = false;
                        fcls = false;
                        i = -1;
                    }
                    toolStripProgressBar1.Value = i+1;
                }
                if (!fopn && !fcls)
                {
                    txt = txt.Remove(0, txt.Length).Insert(0, CalculateTheProcessPriority0(txt.Substring(0, txt.Length)));
                }
                label1.Text += txt;
                toolStripProgressBar1.Value = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("Error! The formula is incorrect.");
            }
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label1.Text = "=";
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.gursuasik.byethost15.com/");
            linkLabel1.LinkVisited = true;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.facebook.com/pages/GÃŒrsu-AÅÄ±k/411125005601913");
            linkLabel1.LinkVisited = true;
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitter.com/gursuasik");
            linkLabel1.LinkVisited = true;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://gursuasik.blogspot.com/ ");
            linkLabel1.LinkVisited = true;
        }
    }
}
