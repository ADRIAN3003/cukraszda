using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cukraszda
{
    class Sutemeny
    {
        private string nev;

        public string Nev
        {
            get { return nev; }
            private set { nev = value; }
        }

        private string tipus;

        public string Tipus
        {
            get { return tipus; }
            private set { tipus = value; }
        }

        private bool dijazott;

        public bool Dijazott
        {
            get { return dijazott; }
            private set { dijazott = value; }
        }

        private int ar;

        public int Ar
        {
            get { return ar; }
            private set { ar = value; }
        }

        private string egyseg;

        public string Egyseg
        {
            get { return egyseg; }
            private set { egyseg = value; }
        }

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
