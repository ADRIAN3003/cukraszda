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
            ElsoFeladat();
            MasodikFeladat();
            HarmadikFeladat();
            NegyedikFeladat();
            OtodikFeladat();
            HatodikFeladat();
        }

        private void HatodikFeladat()
        {
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

        private void OtodikFeladat()
        {
            List<string> kiiras = new List<string>();
            foreach (var suti in sutemenies)
            {
                if (!kiiras.Contains(suti.Nev + " " + suti.Tipus))
                {
                    kiiras.Add(suti.Nev + " " + suti.Tipus);
                }
            }

            using (StreamWriter sw = new StreamWriter("lista.txt"))
            {
                foreach (var suti in kiiras)
                {
                    sw.WriteLine(suti);
                }
            }
        }

        private void NegyedikFeladat()
        {
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
        }

        private void HarmadikFeladat()
        {
            Sutemeny draga = sutemenies.OrderBy(x => x.Ar).Last();
            tbDraga.Text = draga.Nev;
            tbDragaAr.Text = draga.Ar + " Ft/" + draga.Egyseg;

            Sutemeny olcso = sutemenies.OrderBy(x => x.Ar).First();
            tbOlcso.Text = olcso.Nev;
            tbOlcsoAr.Text = olcso.Ar + " Ft/" + olcso.Egyseg;
        }

        private void MasodikFeladat()
        {
            Random rnd = new Random();
            tbAjanlat.Text = "Mai ajánlatunk: " + sutemenies[rnd.Next(0, sutemenies.Count)].Nev;
        }

        private void ElsoFeladat()
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

            foreach (var suti in sutemenies)
            {
                if (!cbTipus.Items.Contains(suti.Tipus))
                {
                    cbTipus.Items.Add(suti.Tipus);
                }
            }
            cbTipus.SelectedIndex = 0;
        }

        private void btnMentes_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTipus.SelectedItem.ToString() == "")
                {
                    throw new Exception("Nem írtál be süteménynevet!");
                }
                else if (sutemenies.Count(x => x.Tipus.ToLower() == cbTipus.SelectedItem.ToString().ToLower()) == 0)
                {
                    throw new Exception("Nincs megfelő sütink. Kérjük Válassz mást!");
                }
                else
                {
                    double kiirt = 0;
                    double osszeg = 0;
                    using (StreamWriter sw = new StreamWriter("ajanlat.txt"))
                    {
                        foreach (var ajanlat in sutemenies)
                        {
                            if (ajanlat.Tipus.ToLower() == cbTipus.SelectedItem.ToString().ToLower())
                            {
                                kiirt++;
                                osszeg += ajanlat.Ar;
                                sw.WriteLine(ajanlat.Nev + " " + ajanlat.Ar + " Ft/" + ajanlat.Egyseg);
                            }
                        }
                    }
                    MessageBox.Show(kiirt + " db sütit írtam ki az ajanlat.txt-be\nátlagár: " + osszeg / kiirt + " Ft");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUjSuti_Click(object sender, EventArgs e)
        {
            try
            {
                int ujSutiAr = 0;
                if (tbSutiNeve.Text == "" || tbSutiTipusa.Text == "" || tbSutiEgysege.Text == "" || tbSutiAr.Text == "")
                {
                    throw new Exception("Nem adtál meg minden adatot!");
                }
                else if (!int.TryParse(tbSutiAr.Text, out ujSutiAr))
                {
                    throw new Exception("Az új sütemény ára nem szám!");
                }
                else
                {
                    sutemenies.Add(new Sutemeny(tbSutiNeve.Text, tbSutiTipusa.Text, cbSutiDijazott.Checked, ujSutiAr, tbSutiEgysege.Text));

                    using (StreamWriter sw = File.AppendText("cuki.txt"))
                    {
                        sw.WriteLine(tbSutiNeve.Text + ";" + tbSutiTipusa.Text + ";" + cbSutiDijazott.Checked + ";" + tbSutiAr.Text + ";" + tbSutiEgysege.Text);
                    }

                    MessageBox.Show("Az állomány bővítése sikeres volt!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
