using System;
using System.IO;
using System.Collections.Generic;

namespace Rejstrik
{
    class Program
    {
        public static Dictionary<string, List<int>> SeznamRejstrik { get; set; }
        static void Main(string[] args)
        {
            if (File.Exists("ThreeMenInABoatEnglish.txt"))
            {
                VytvorRejstrik();
                foreach (var item in SeznamRejstrik)
                {
                    Stream stream = new FileStream("rejstrik.txt", FileMode.Append);
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.Write(item.Key + " - ");
                        foreach (var itemy in item.Value)
                        {
                            sw.Write(itemy + ", ");
                        }
                        sw.WriteLine();
                    }
                    Console.Write(item.Key + " - ");
                    foreach (var itemy in item.Value)
                    {
                        Console.Write(itemy + ", ");
                    }
                    Console.WriteLine("Dokončeno, najdete v souboru rejstrik.txt");
                }
            }
            else
                Console.WriteLine("Missing file to read.");
            Console.ReadKey();
        }

        public static void VytvorRejstrik() 
        {
            Console.WriteLine("Finding words.......");
            SeznamRejstrik = new Dictionary<string, List<int>>();
            using (StreamReader sr = new StreamReader("ThreeMenInABoatEnglish.txt"))
            {
                int radek = 0;
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    string[] radekSlova = line.Split(' ');
                    foreach (var item in radekSlova)
                    {
                        if (!SeznamRejstrik.ContainsKey(item))
                        {
                            List<int> radky = new List<int>();
                            radky.Add(radek);
                            SeznamRejstrik.Add(item, radky);
                        }
                        else if (SeznamRejstrik.ContainsKey(item) && !SeznamRejstrik[item].Contains(radek))
                            SeznamRejstrik[item].Add(radek);
                    }
                    radek++;
                }
            }
        }
    }
}
