using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CA230926
{
    class Program
    {
        static void Main()
        {
            var hivasok = new List<Hivas>();
            using var sr = new StreamReader(
                path: @"..\..\..\src\cb.txt",
                encoding: Encoding.UTF8);
            _ = sr.ReadLine();
            while (!sr.EndOfStream) hivasok.Add(new(sr.ReadLine()));

            Console.WriteLine($"3.f.: bejegyzesek szama: {hivasok.Count} db");

            bool f4 = hivasok.Any(h => h.AdasDb == 4);
            Console.WriteLine($"4.f.: {(f4 ? null : "nem ")} volt 4 adast indito sofor");

            Console.Write("5.f.: kerek egy nevet: ");
            string knev = Console.ReadLine();
            int f5 = hivasok
                .Where(h => h.Nev.ToLower() == knev.ToLower())
                .Sum(h => h.AdasDb);
            if (f5 != 0) Console.WriteLine($"\t{knev} {f5}x hasznalta a CB-radiot");
            else Console.WriteLine("\tnincs ilyen nevu sofor");

            using var sw = new StreamWriter(
                path: @"..\..\..\src\cb2.txt",
                append: false,
                encoding: Encoding.UTF8);
            sw.WriteLine("Kezdes;Nev;AdasDb");
            foreach (var h in hivasok)
                sw.WriteLine($"{h.IdoPercben};{h.Nev};{h.AdasDb}");

            var f8dic = hivasok
                .GroupBy(h => h.Nev)
                .ToDictionary(n => n.Key, hsz => hsz.Sum(h => h.AdasDb));
            Console.WriteLine($"8.f: soforok szama: {f8dic.Count} fo");

            var f9 = f8dic.OrderBy(kvp => kvp.Value).Last();
            Console.WriteLine("9.f.: legtobb adast indito sofor:");
            Console.WriteLine($"\tnev: {f9.Key}");
            Console.WriteLine($"\tadasok szama: {f9.Value} alkalom");
        }
    }
}
