using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cukraszda
{
    class Sutemeny
    {

        public string Nev { get; set; }
        public string Tipus { get; set; }
        public bool Dijazott { get; set; }
        public int Ar { get; set; }
        public string Egyseg { get; set; }

        public Sutemeny(string nev, string tipus, bool dijazott, int ar, string egyseg)
        {
            Nev = nev;
            Tipus = tipus;
            Dijazott = dijazott;
            Ar = ar;
            Egyseg = egyseg;
        }
    }
}
