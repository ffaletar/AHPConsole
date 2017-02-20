using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHPLib
{
    public class Executor
    {
        public List<Alternativa> listaAlternativa;
        public List<Kriterij> listaKriterija;
        private double[,] matricaKriterija;

        public Executor()
        {
            listaAlternativa = new List<Alternativa>();
            listaKriterija = new List<Kriterij>();

            Alternativa alternativa1 = new Alternativa("BMW X5");
            Alternativa alternativa2 = new Alternativa("Audi A7");
            Alternativa alternativa3 = new Alternativa("Opel Insignia");
            Alternativa alternativa4 = new Alternativa("Golf 7");
            Alternativa alternativa5 = new Alternativa("Mercedes");

            Kriterij kriterij1 = new Kriterij("Cijena",0, null);
            Kriterij kriterij2 = new Kriterij("Kvaliteta", 1, null);
            Kriterij kriterij3 = new Kriterij("Izgled", 2, null);
            Kriterij kriterij4 = new Kriterij("Boja", 3, kriterij3);
            Kriterij kriterij5 = new Kriterij("Oblik", 4, kriterij3);

            listaKriterija.Add(kriterij1);
            listaKriterija.Add(kriterij2);
            listaKriterija.Add(kriterij3);
            listaKriterija.Add(kriterij4);
            listaKriterija.Add(kriterij5);

            listaAlternativa.Add(alternativa1);
            listaAlternativa.Add(alternativa2);
            listaAlternativa.Add(alternativa3);
            listaAlternativa.Add(alternativa4);
            listaAlternativa.Add(alternativa5);
        }

        public bool DodajAlternativu(string nazivAlternative)
        {
            Alternativa alternativa = new Alternativa(nazivAlternative);
            string a = alternativa.Naziv;
            listaAlternativa.Add(alternativa);

            return true;
        }
        public bool DodajKriterij(string nazivKriterija, int idRoditelja)
        {
            int zadnjiId = 0;
            if(listaKriterija.Count != 0)
            {
                zadnjiId = listaKriterija[listaKriterija.Count - 1].Id + 1;
            }

            if (idRoditelja != -1)
            {
                Kriterij roditelj = listaKriterija.Find(r => r.Id == idRoditelja);
                Kriterij dijete = new Kriterij(nazivKriterija, zadnjiId, roditelj);

                listaKriterija.Add(dijete);
            }
            else
            {
                Kriterij roditelj = new Kriterij(nazivKriterija, zadnjiId);
                listaKriterija.Add(roditelj);
            }

            return true;
        }

        public List<Alternativa> DohvatiListuAlternativa()
        {
            return listaAlternativa;
        }
        
        public List<Kriterij> DohvatiListuKriterija()
        {
            return listaKriterija;
            
        }


        public Kriterij DohvatiKriterijPremaId(int id)
        {
            Kriterij kriterij = listaKriterija.Find(x => x.Id == id);

            return kriterij;
        }

        public double[,] KreirajNovuMatricuKriterija()
        {
            int brojKriterija = listaKriterija.Count();
            matricaKriterija = new double[brojKriterija, brojKriterija];

            return matricaKriterija;
        }


    }
}
