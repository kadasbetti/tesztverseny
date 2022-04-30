using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace tesztverseny
{

    internal class Valasz
    {
        public Char aktualisValasz; //A,B,C,D,X
        public Char helyesseg; // + szokoz
        public int pontszam; //3,4,5,6
    }
    internal class Versenyzo
    {
        public String kod; //max 5 char
        public List<Valasz> valaszok = new List<Valasz>();
        public int osszPont;
    }
  
    internal class Program
    {

        static int pontszamlalo(int index)
        {
            if(index<5)
            {
                return 3; 
            }
            if(index>=5 && index<10)
            {
                return 4;
            }
            if(index>=10 && index<13)
            {
                return 5;
            }
            else
            {
                return 6;
            }
        }

        static void Main(string[] args)
        {

           

Console.WriteLine("1. feladat: Az adatok beolvasása");

            StreamReader sr = new StreamReader("valaszok.txt");
            List<Char> helyesValaszok = new List<Char>();
            List<Versenyzo> versenyzok = new List<Versenyzo>();

            int index = 0;

            while (!sr.EndOfStream)
            {
                String sor = sr.ReadLine();

                if (index != 0)
                {   
                    Versenyzo versenyzo = new Versenyzo();
                    String kod = sor.Split(' ')[0];
                    versenyzo.kod = sor.Split(' ')[0];

                    foreach(Char valaszFajlbol in sor.Split(' ')[1])
                    {
                        Valasz valasz = new Valasz();
                        valasz.aktualisValasz = valaszFajlbol;
                        versenyzo.valaszok.Add(valasz);
                    }
                    versenyzok.Add(versenyzo);
                }
                else
                {
                   foreach(Char valasz in sor)
                    { 
                         helyesValaszok.Add(valasz);
                    }
                }
                index++;

            }
            Console.WriteLine("");

Console.WriteLine("2.feladat");
            Console.WriteLine("A vetélkedőn " + versenyzok.Count + " versenyző indult.");
            Console.WriteLine("");

Console.WriteLine("3. feladat: Adja meg a versenyző azonosítóját!");
            String versenyzokod = Console.ReadLine();
            Console.WriteLine("A versenyzo azonositoja: "+ versenyzokod);
            foreach(Versenyzo v in versenyzok)
            {
                if (versenyzokod.Equals(v.kod))
                {
                    String osszesvalasz = "";
                    foreach(Valasz valasz in v.valaszok)
                    {
                        osszesvalasz = osszesvalasz + valasz.aktualisValasz;
                    }
                    Console.WriteLine(osszesvalasz + " (a versenyző válasza)");
                }
            }
    Console.WriteLine();
Console.WriteLine("4. feladat");
            foreach(Versenyzo v in versenyzok)
            {
                for(int i = 0; i < v.valaszok.Count; i++)
                {
                    if (v.valaszok[i].aktualisValasz.Equals(helyesValaszok[i]))
                        {
                        v.valaszok[i].helyesseg = '+';
                        }
                    else
                    {
                        v.valaszok[i].helyesseg = ' ';
                    }
                }

            }
            
            foreach(Char c in helyesValaszok)
            {
                Console.Write(c);   
            }
            Console.Write(" (a helyes megoldás)");
            Console.WriteLine();

            foreach (Versenyzo v in versenyzok)
            {
                if (versenyzokod.Equals(v.kod))
                {
                    String helyesseg = " ";
                    foreach(Valasz valasz in v.valaszok)
                    {
                        helyesseg += valasz.helyesseg;
                    }
                    Console.WriteLine(helyesseg + " (a versenyző helyes válaszai)");
                }
            }

    Console.WriteLine();
Console.WriteLine("5. feladat: Adja meg egy feladat sorszámát!");
            int sorszam = int.Parse(Console.ReadLine());
            Console.WriteLine("A feladat sorszáma = " + sorszam);

            int szamlalo = 0;
            foreach(Versenyzo v in versenyzok)
            {
                if(v.valaszok[sorszam-1].helyesseg.Equals('+'))
                {
                    szamlalo++;
                }
            }


            Console.WriteLine("A feladatra {0} fő, a versenyzők {1:00.00}%-a adott helyes választ.", szamlalo, ((double)szamlalo/(double)versenyzok.Count)*100);
                    
     Console.WriteLine();
Console.WriteLine("6. feladat");

            foreach(Versenyzo v in versenyzok)
            {
                for (int i = 0; i < v.valaszok.Count; i++)
                {
                    v.valaszok[i].pontszam = pontszamlalo(i);
                    if (v.valaszok[i].helyesseg.Equals('+'))
                    {
                        v.osszPont += v.valaszok[i].pontszam;
                    }
                }
            }

            StreamWriter sw = new StreamWriter("pontok.txt");

            foreach(Versenyzo v in versenyzok)
            {
                sw.WriteLine(v.kod + " " + v.osszPont);
            }
            sw.Close();





            Console.ReadKey();
        }
    }
}
