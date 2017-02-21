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
        List<Kriterij>[] skupineKriterija;

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
            Kriterij kriterij6 = new Kriterij("Gume", 5, null);

            listaKriterija.Add(kriterij1);
            listaKriterija.Add(kriterij2);
            listaKriterija.Add(kriterij3);
            listaKriterija.Add(kriterij4);
            listaKriterija.Add(kriterij5);
            listaKriterija.Add(kriterij6);

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

        public double[,] KreirajNovuMatricuKriterija(List<Kriterij> primljenaListaKriterija)
        {
            int brojKriterija = primljenaListaKriterija.Count();
            matricaKriterija = new double[brojKriterija, brojKriterija];
           
            return matricaKriterija;
        }

        //public List<Kriterij>[] DohvatiSkupineKriterija()
        //{
        //    skupineKriterija = new List<Kriterij>[100];

        //    for(int i = 0; i<listaKriterija.Count; i++)
        //    {



        //    }
        //}

        public List<Kriterij>[] KreirajSkupineKriterija()
        {

            
            IEnumerable<Kriterij> enumerable = Enumerable.Empty<Kriterij>();
            enumerable = listaKriterija.GroupBy(x => x.Roditelj).Select(x => x.First());
            List<Kriterij> listaKriterijaNumerable = enumerable.ToList();

            List<Kriterij>[] poljeListaPoRazinama = new List<Kriterij>[listaKriterijaNumerable.Count];

            Dictionary<string, List<Kriterij>> tbl = new Dictionary<string, List<Kriterij>>();
            string ime = "";


            for (int k=0; k<listaKriterijaNumerable.Count; k++)
            {
                Kriterij kriterij = new Kriterij();
                kriterij = listaKriterijaNumerable[k];

                if(kriterij.Roditelj != null)
                {
                    ime = "lista" + kriterij.Roditelj.Id;
                }else
                {
                    ime = "lista00";
                }
                

                List<Kriterij> lista = new List<Kriterij>();
                tbl.Add(ime, lista);

              
            }


            for (int i = 0; i < listaKriterija.Count; i++)
            {
                Kriterij kriterij = new Kriterij();
                kriterij = listaKriterija[i];
                if(kriterij.Roditelj != null)
                {
                    if (tbl.ContainsKey("lista" + kriterij.Roditelj.Id))
                    {
                        tbl["lista" + kriterij.Roditelj.Id].Add(kriterij);
                    }
                }
                else
                {
                    tbl["lista00"].Add(kriterij);
                }
                
                //else
                //{
                //    testList.Add(key[index], new List<long> { val[index] });
                //}    
                

                //if (kriterij.Roditelj == null)
                //{
                //    listaPrveRazine.Add(kriterij);
                //}
            }
            int a = 0;
            foreach (KeyValuePair<string, List<Kriterij>> lista in tbl)
            {
                int velicinaPolja = poljeListaPoRazinama.Length;
                poljeListaPoRazinama[a] = lista.Value;
                a++;
            }

            //poljeListaPoRazinama[0] = listaPrveRazine;
            return poljeListaPoRazinama;
        }

    }
}
