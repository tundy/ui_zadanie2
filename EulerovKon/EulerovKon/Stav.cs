using System;
using System.Collections.Generic;

namespace EulerovKon
{
    /// <summary>
    /// Objekt obsahujúci informácie o aktuálnom rozloení šachovnice
    /// </summary>
    public class Stav
    {
        #region Attributes
        /// <summary>
        /// Urèuje ktoré políèka na šachovnici u boli navštívené
        /// </summary>
        public readonly bool[,] Used;
        /// <summary>
        /// Šírka šachovnice
        /// </summary>
        public readonly int Width;
        /// <summary>
        /// Vıška šachovnice
        /// </summary>
        public readonly int Height;
        /// <summary>
        /// Horizontálna súradnica kde sa práve nachádza kôò
        /// </summary>
        public readonly int X;
        /// <summary>
        /// Vertikálna súradnica kde sa práve nachádza kôò
        /// </summary>
        public readonly int Y;
        /// <summary>
        /// Poèet neobsadenıch políèok v šachovnici
        /// </summary>
        public readonly int Remaining;
        /// <summary>
        /// Cena stavu
        /// </summary>
        public int Cost => Jumps.Count;
        /// <summary>
        /// Dostupné políèka kam môe kôò skoèi
        /// </summary>
        public readonly List<Tuple<int,int>> Jumps = new List<Tuple<int, int>>(8);
        /// <summary>
        /// Cesta, ktorou som sa dostal do tohto stavu
        /// </summary>
        public readonly Tuple<int, int>[] Path;
        /// <summary>
        /// Pozícia v poli Cesta
        /// </summary>
        private readonly int _pathIndex;
        #endregion

        #region Constructors
        /// <summary>
        /// Vytvor novı stav
        /// </summary>
        /// <param name="width">Šírka stavu</param>
        /// <param name="height">Vıška stavu</param>
        /// <param name="x">Horizontálna pozícia koòa</param>
        /// <param name="y">Vertikálna pozícia koòa</param>
        public Stav(int width, int height, int x, int y)
        {
            Used = new bool[width, height];
            Width = width;
            Height = height;

            X = x;
            Y = y;

            Used[X, Y] = true;
            Path = new Tuple<int, int>[Width * Height];
            Remaining = Width * Height - 1;
            Path[_pathIndex++] = new Tuple<int, int>(x,y);

            GenerateJumps();
        }

        /// <summary>
        /// Vytvorí novı stav na základe druhého stavu
        /// </summary>
        /// <param name="stav">Stav, z ktorého ma skopírova rozloenie</param>
        /// <param name="horse">Nová pozícia koòa</param>
        public Stav(Stav stav, Tuple<int, int> horse)
        {
            Width = stav.Width;
            Height = stav.Height;
            Used = new bool[Width, Height];

            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                    Used[x,y] = stav.Used[x, y];

            X = horse.Item1;
            Y = horse.Item2;

            Used[horse.Item1, horse.Item2] = true;
            Path = new Tuple<int, int>[stav.Path.Length];
            Remaining = stav.Remaining - 1;

            _pathIndex = stav._pathIndex;
            for (var i = 0; i < _pathIndex; i++)
                Path[i] = stav.Path[i];
            Path[_pathIndex++] = horse;

            GenerateJumps();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Vráti èi aktuálny stav je koneènı stav
        /// </summary>
        /// <returns>Vráti èi aktuálny stav je koneènı stav</returns>
        public bool Victory() => Remaining == 0;

        /// <summary>
        /// Vráti èi aktuálny stav je slepá ulièka
        /// </summary>
        /// <returns>Vráti èi aktuálny stav je slepá ulièka</returns>
        public bool Failed() => Remaining != 0 && Cost == 0;

        /// <summary>
        /// Vráti èi políèko v šachovnici je obsadené
        /// </summary>
        /// <param name="index">Tuple obsahujúci horizontálnu pozíciu na prvom mieste a vertikálnu na druhom</param>
        /// <returns>Obsadenos políèka v šachovnici</returns>
        public bool GetUsed(Tuple<int, int> index) => Used[index.Item1, index.Item2];
        #endregion

        #region Private Methdos
        /// <summary>
        /// Vygeneruje nové dostupné pozície kam môe kôò skoèi, na základe operátorov
        /// </summary>
        private void GenerateJumps()
        {
            foreach (var @operator in Operatory.AllOperations)
            {
                var skok = @operator(this);
                if (skok == null) continue;
                Jumps.Add(skok);
            }
        }
        #endregion
    }
}