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
        public Executor()
        {
            listaAlternativa = new List<Alternativa>();
            listaKriterija = new List<Kriterij>();


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

    }
}
