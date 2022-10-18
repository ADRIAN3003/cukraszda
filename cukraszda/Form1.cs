using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace cukraszda
{
    public partial class Form1 : Form
    {
        List<Sutemeny> sutemenies = new List<Sutemeny>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("cuki.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] tmp = sr.ReadLine().Split(';');
                    string nev = tmp[0];
                    string tipus = tmp[1];
                    bool dijazott = Convert.ToBoolean(tmp[2]);
                    int ar = Convert.ToInt32(tmp[3]);
                    string egyseg = tmp[4];
                    sutemenies.Add(new Sutemeny(nev, tipus, dijazott, ar, egyseg));
                }
            }

            Random rnd = new Random();

        }
    }
}
