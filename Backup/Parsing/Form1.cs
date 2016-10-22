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
        static string ifade;
        static byte oper;
        static int sayibul(string gelen)
        {
            string a = gelen;
            for (int i = 0; i < gelen.Length; i++)
            {
                switch (gelen[i])
                {
                    case '+':
                        oper = 1;
                        ifade = ifade.Substring(i + 1, ifade.Length - (i + 1));
                        return Convert.ToInt32(a.Substring(0, i));
                    case '-':
                        oper = 2;
                        ifade = ifade.Substring(i + 1, ifade.Length - (i + 1));
                        return Convert.ToInt32(a.Substring(0, i));
                    case '*':
                        oper = 3;
                        ifade = ifade.Substring(i + 1, ifade.Length - (i + 1));
                        return Convert.ToInt32(a.Substring(0, i));
                    case '/':
                        oper = 4;
                        ifade = ifade.Substring(i + 1, ifade.Length - (i + 1));
                        return Convert.ToInt32(a.Substring(0, i));
                    case '=':
                        ifade = ifade.Substring(i + 1, ifade.Length - (i + 1));
                        return Convert.ToInt32(a.Substring(0, i));
                    
                        
                }
            }
            return 0;
        }
        static double islem(int sayi1, int sayi2)
        {
            switch (oper)
            {
                case 1:
                    return sayi1 + sayi2;
                case 2:
                    return sayi1 - sayi2;
                case 3:
                    return sayi1 * sayi2;
                case 4:
                    return (double)sayi1 / sayi2;
            }
            return 0;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ifade = textBox1.Text;
            oper = 1;
            double sonuc=islem(sayibul(ifade), sayibul(ifade));
            MessageBox.Show(sonuc.ToString());
        }
    }
}
