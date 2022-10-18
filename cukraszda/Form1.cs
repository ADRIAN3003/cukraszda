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
            tbAjanlat.Text = "Mai ajánlatunk: " + sutemenies[rnd.Next(0, sutemenies.Count)].Nev;

            Sutemeny draga = sutemenies.OrderBy(x => x.Ar).Last();
            tbDraga.Text = draga.Nev;
            tbDragaAr.Text = draga.Ar + " Ft/" + draga.Egyseg;

            Sutemeny olcso = sutemenies.OrderBy(x => x.Ar).First();
            tbOlcso.Text = olcso.Nev;
            tbOlcsoAr.Text = olcso.Ar + " Ft/" + olcso.Egyseg;

            List<string> dijnyertes = new List<string>();
            foreach (var suti in sutemenies)
            {
                if (suti.Dijazott)
                {
                    if (!dijnyertes.Contains(suti.Nev + "-" + suti.Egyseg))
                    {
                        dijnyertes.Add(suti.Nev + "-" + suti.Egyseg);
                    }
                }
            }

            tbDijnyertes.Text = dijnyertes.Count + " féle díjnyertes édességből választhat.";

            Dictionary<string, string> kiiras = new Dictionary<string, string>();
            foreach (var suti in sutemenies)
            {
                if (!kiiras.ContainsKey(suti.Nev))
                {
                    kiiras.Add(suti.Nev, suti.Tipus);
                }
            }

            using (StreamWriter sw = new StreamWriter("lista.txt"))
            {
                foreach (var suti in kiiras)
                {
                    sw.WriteLine(suti.Key + " " + suti.Value);
                }
            }


            Dictionary<string, int> stats = new Dictionary<string, int>();
            foreach (var suti in sutemenies)
            {
                if (stats.ContainsKey(suti.Tipus))
                {
                    stats[suti.Tipus]++;
                }
                else
                {
                    stats.Add(suti.Tipus, 1);
                }
            }


            using (StreamWriter sw = new StreamWriter("stat.csv"))
            {
                foreach (var stat in stats)
                {
                    sw.WriteLine(stat.Key + ";" + stat.Value);
                }
            }
        }
    }
}
