using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHPLib;

namespace AHPConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool kraj = false;
            bool nastaviti = true;

            Executor executor = new Executor();
            List<Kriterij> listaKriterija = new List<Kriterij>();
            List<Kriterij>[] poljeListaKriterija = new List<Kriterij>[5];
            Dictionary<string, double> vrijednostKriterija = new Dictionary<string, double>();
            double[,] matricaKriterija;
            Dictionary<string, Dictionary<string, double>> popisVrijednostiKriterija = new Dictionary<string, Dictionary<string, double>>();



            //while (kraj == false){
            //    Console.WriteLine("1. Unos alternative, 2. Unos kriterija, 3. Ispis kriterija, 9. Izlaz");

            //    switch (Console.ReadLine())
            //    {
            //        case "1":
            //            Console.WriteLine("---Odabrali ste unos alternative---");
            //            Console.WriteLine("Naziv alternative: ");
            //            string alternativa = Console.ReadLine();
            //            executor.DodajAlternativu(alternativa);

            //            break;
            //        case "2":
            //            Console.WriteLine("---Odabrali ste unos kriterija---");
            //            Console.WriteLine("Naziv kriterija: ");
            //            executor.DodajKriterij(Console.ReadLine(), -1);

            //            break;
            //        case "3":
            //            Console.WriteLine("---Odabrali ste ispis kriterija---");
            //            listaKriterija = executor.DohvatiListuKriterija();

            //            for (int i = 0; i < listaKriterija.Count; i++)
            //            {
            //                Kriterij kriterij = new Kriterij();
            //                kriterij = listaKriterija[i];
            //                if(kriterij.Roditelj != null)
            //                {
            //                    Console.WriteLine(kriterij.Naziv + " -->  " + kriterij.Id + "  ===  " + executor.DohvatiKriterijPremaId(kriterij.Roditelj.Id).Naziv);
            //                }else
            //                {
            //                    Console.WriteLine(kriterij.Naziv + " -->  " + kriterij.Id + "  ===  " + "'bez roditelja'");
            //                }

            //            }

            //            while(nastaviti == true)
            //            {
            //                Console.WriteLine("1 ---> Dodati podkriterij");
            //                Console.WriteLine("2 ---> Nastaviti bez dodavanja podkriterija");
            //                if (Console.ReadLine() == "1")
            //                {
            //                    Console.WriteLine("Unesi naziv podkriterija ");
            //                    string nazivPodkriterija = Console.ReadLine();
            //                    Console.WriteLine("Unesi id roditelja ");
            //                    int idRoditelja = Convert.ToInt32(Console.ReadLine());
            //                    executor.DohvatiKriterijPremaId(idRoditelja);
            //                    executor.DodajKriterij(nazivPodkriterija, idRoditelja);
            //                    nastaviti = true;
            //                }else
            //                {
            //                    nastaviti = false;
            //                }
            //            }



            //            break;
            //        case "9":
            //            kraj = true;
            //            break;
            //        default:
            //            Console.WriteLine("---Pogrešan unos---");
            //            break;

            //    }
            //}

            poljeListaKriterija = executor.KreirajSkupineKriterija();

            for(int a = 0; a<poljeListaKriterija.Length; a++)
            {
                listaKriterija = poljeListaKriterija[a];

                matricaKriterija = executor.KreirajNovuMatricuKriterija(listaKriterija);
                
                //listaKriterija = executor.DohvatiListuKriterija();

                for (int i = 0; i < listaKriterija.Count; i++)
                {
                    Kriterij kriterij1 = new Kriterij();
                    kriterij1 = listaKriterija[i];
                    for (int j = 0; j < listaKriterija.Count; j++)
                    {
                        Kriterij kriterij2 = new Kriterij();
                        kriterij2 = listaKriterija[j];
                        double vrijednost = 1;

                        if (i < j)
                        {
                            Console.WriteLine(kriterij1.Naziv + " - " + kriterij2.Naziv);
                            vrijednost = Convert.ToDouble(Console.ReadLine());
                            matricaKriterija[i, j] = vrijednost;
                            matricaKriterija[j, i] = 1 / vrijednost;
                        }
                        else if (i == j)
                        {
                            matricaKriterija[i, j] = 1;
                        }
                    }
                }

                List<Kriterij> lisKrit = new List<Kriterij>();
                string kriterijRoditeljNaziv = "";
                lisKrit = poljeListaKriterija[a];
                if(lisKrit[0].Roditelj != null)
                {
                    kriterijRoditeljNaziv = lisKrit[0].Roditelj.Naziv;
                }
                else
                {
                    kriterijRoditeljNaziv = "null";
                }


                vrijednostKriterija = executor.IzracunajVrijednostKriterija(matricaKriterija, listaKriterija);


                popisVrijednostiKriterija.Add(kriterijRoditeljNaziv, vrijednostKriterija);

            }


            
            

        Console.ReadLine();

        }
    }
}
