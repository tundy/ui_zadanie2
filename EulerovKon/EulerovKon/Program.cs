using System;

namespace EulerovKon
{
    internal class Program
    {
        private static readonly Search Search = new Search();

        private static void DoStuff(int width, int height, int x, int y)
        {
            var path = Search.Start(width, height, x-1, y-1, 15);
            Console.WriteLine();
            Console.WriteLine($"Dlzka prehladavania: {Search.TimeElapsed}.");
            Console.WriteLine($"Pocet prehladanych uzlov: {Search.Steps}.");
            Console.WriteLine($"Pocet Vygenerovanych uzlov: {Search.Generated}.");
            Console.WriteLine($"\"Maximalne\" vyuzitie pamate pocas hladania: {Search.MaxMemory >>10} KiloBajtov.");
            if (path != null)
            {
                Console.WriteLine($"Najdena cesta({path.Length}) pre sachovnicu {width}x{height} z pozicie {x}x{y}:");
                foreach (var tuple in path)
                    Console.Write($"{tuple.Item1 + 1}x{tuple.Item2 + 1} ");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine(Search.TimedOut ? "Nepodarilo sa najst cestu v casovom limite" : "Cesta neexistuje");
            }
        }

        private static void Main()
        {

            DoStuff(5, 5, 1, 1);
            Console.ReadKey(true);
            DoStuff(20, 5, 12, 4);
            /* DoStuff(6, 5, 1, 1);
            DoStuff(5, 5, 2, 2);
             DoStuff(6, 5, 2, 2);
             DoStuff(6, 6, 1, 1);
             DoStuff(6, 6, 2, 2);*/
            Console.ReadKey(true);
            DoStuff(7, 8, 1, 1);
            /*DoStuff(8, 8, 2, 2);
            DoStuff(20, 20, 1, 1);
            DoStuff(20, 20, 2, 2);*/
            Console.ReadKey(true);
            DoStuff(20, 20, 10, 10);
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
