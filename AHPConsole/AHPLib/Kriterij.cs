using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHPLib
{
    public class Kriterij
    {
        private int id;
        private string naziv;
        private Kriterij roditelj;

        public Kriterij()
        {

        }
        public Kriterij(string naziv, int id)
        {
            this.naziv = naziv;
            this.id = id;
        }

        public Kriterij(string naziv, int id, Kriterij roditelj)
        {
            this.naziv = naziv;
            this.roditelj = roditelj;
            this.id = id;
        }
        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Kriterij Roditelj
        {
            get { return roditelj; }
            set { roditelj = value; }
        }

    }
}
