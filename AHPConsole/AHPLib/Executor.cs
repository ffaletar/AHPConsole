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

            Kriterij kriterij1 = new Kriterij("Cijena", 0, null);
            Kriterij kriterij2 = new Kriterij("Trajanje", 1, null);
            Kriterij kriterij3 = new Kriterij("Destinacija", 2, null);
            Kriterij kriterij4 = new Kriterij("Boja", 3, kriterij3);
            Kriterij kriterij5 = new Kriterij("Oblik", 4, kriterij3);
            Kriterij kriterij6 = new Kriterij("Udaljenost", 5, null);

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

        //samo za kreiranje matrice prema primljenoj listi kriterija, odnosno prema razinama
        public double[,] KreirajNovuMatricuKriterija(List<Kriterij> primljenaListaKriterija)
        {
            int brojKriterija = primljenaListaKriterija.Count();
            matricaKriterija = new double[brojKriterija, brojKriterija];

            return matricaKriterija;
        }

        public Dictionary<string, double> IzracunajVrijednostKriterija(double[,] kriteriji, List<Kriterij> kriterijiLista)
        {
            Dictionary<string, double> dic = new Dictionary<string, double>();
            List<double> zbrojStupaca = new List<double>();
            List<double> vrijednostKriterija = new List<double>();

            int brojKriterija = (int)Math.Sqrt(kriteriji.Length);

            double[,] prepravljenoPolje = new double[brojKriterija, brojKriterija];

            for (int j = 0; j < Math.Sqrt(kriteriji.Length); j++)
            {
                double zbroj = 0;

                for (int i = 0; i < Math.Sqrt(kriteriji.Length); i++)
                {
                    zbroj = zbroj + kriteriji[i, j];
                }

                zbrojStupaca.Add(zbroj);
            }

            for (int i = 0; i < Math.Sqrt(kriteriji.Length); i++)
            {
                double zbrojRedaka = 0;
                string ime;
                for (int j = 0; j < Math.Sqrt(kriteriji.Length); j++)
                {
                    prepravljenoPolje[i,j] = kriteriji[i, j] / zbrojStupaca[j];
                    zbrojRedaka = zbrojRedaka + prepravljenoPolje[i, j];
                }
                Kriterij kriterij = new Kriterij();
                kriterij = kriterijiLista[i];

                dic.Add(kriterij.Naziv, zbrojRedaka / Math.Sqrt(kriteriji.Length));
            }

            return dic;
        }

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

        public bool ProvjeriKonzistentnost(double[,] matrKriterija)
        {
            List<double> zbrojStupaca = new List<double>();
            List<double> prosjekRetka = new List<double>();
            List<double> vrijednostKriterija = new List<double>();
            List<double> konzistentnostRetci = new List<double>();
            bool konzistentno = false;

            double CI = 0;
            double CR = 0;
            double lambdaZbroj = 0;
            double lambda = 0;
            int brojKriterija = (int)Math.Sqrt(matrKriterija.Length);

            double[,] prepravljenoPolje = new double[brojKriterija, brojKriterija];
            //ovaj dio koda se ponavlja, možda bi se moglo spojiti u jedno, zbog efikasnosti, odnosno zbog redundantnog izvršavanja for petlji

            //zbroj stupaca matrice
            for (int j = 0; j < Math.Sqrt(matrKriterija.Length); j++)
            {
                double zbroj = 0;

                for (int i = 0; i < Math.Sqrt(matrKriterija.Length); i++)
                {
                    zbroj = zbroj + matrKriterija[i, j];
                }

                zbrojStupaca.Add(zbroj);
            }
            //zbroj redaka matrice
            for (int i = 0; i < Math.Sqrt(matrKriterija.Length); i++)
            {
                double zbroj = 0;

                for (int j = 0; j < Math.Sqrt(matrKriterija.Length); j++)
                {
                    zbroj = zbroj + matrKriterija[i, j];
                }

                prosjekRetka.Add(zbroj / Math.Sqrt(matrKriterija.Length));
            }

            for (int i = 0; i < Math.Sqrt(matrKriterija.Length); i++)
            {
                //double zbrojRedaka = 0;
                string ime;
                for (int j = 0; j < Math.Sqrt(matrKriterija.Length); j++)
                {
                    prepravljenoPolje[i, j] = matrKriterija[i, j] / zbrojStupaca[j];
                    //zbrojRedaka = zbrojRedaka + prepravljenoPolje[i, j];
                }
            }
            //Math.Sqrt(matrKriterija.Length)
            for (int n=0; n< Math.Sqrt(matrKriterija.Length); n++)
            {


                konzistentnostRetci.Add(prosjekRetka[n] / prepravljenoPolje[n, n]);
            }

            for(int z=0; z<konzistentnostRetci.Count; z++)
            {
                lambdaZbroj = lambdaZbroj + konzistentnostRetci[z];
            }

            lambda = lambdaZbroj / Math.Sqrt(matrKriterija.Length);

            CI = (lambda - Math.Sqrt(matrKriterija.Length)) / ((Math.Sqrt(matrKriterija.Length) - 1));
            
            CR = CI / DohvatiRI((int)Math.Sqrt(matrKriterija.Length));

            if(CR > 0.1)
            {
                konzistentno = false;
            }
            else
            {
                konzistentno = true;
            }

            return konzistentno;
        }

        public double DohvatiRI(int brojKriterija)
        {
            double RI = 0;
            switch (brojKriterija)
            {
                case 1:
                    RI = 0;
                    break;
                case 2:
                    RI = 0;
                    break;
                case 3:
                    RI = 0.58;
                    break;
                case 4:
                    RI = 0.9;
                    break;
                case 5:
                    RI = 1.12;
                    break;
                case 6:
                    RI = 1.24;
                    break;
                case 7:
                    RI = 1.32;
                    break;
                case 8:
                    RI = 1.41;
                    break;
                case 9:
                    RI = 1.45;
                    break;
                case 10:
                    RI = 1.49;
                    break;
                default:
                    RI = 0;
                    break;
            }

            return RI;
        }
    }
}
