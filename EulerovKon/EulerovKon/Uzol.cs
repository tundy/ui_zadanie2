using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EulerovKon
{
    /// <summary>
    ///     Objekt obsahujúci informácie o aktuálnom rozloení šachovnice
    /// </summary>
    [DebuggerDisplay("X:{X}, Y:{Y}, C:{Cost}, R:{Remaining}")]
    internal class Uzol
    {
        #region Private Methdos

        /// <summary>
        ///     Vygeneruje nové dostupné pozície kam môe kôò skoèi, na základe operátorov
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

        #region Constants

        /// <summary>
        ///     Maximálna Šírka šachovnice
        /// </summary>
        public const int MaxWidth = 20;

        /// <summary>
        ///     Maximálna vıška šachovnice
        /// </summary>
        public const int MaxHeight = 20;

        /// <summary>
        ///     Minimálna Šírka šachovnice
        /// </summary>
        public const int MinWidth = 5;

        /// <summary>
        ///     Minimálna vıška šachovnice
        /// </summary>
        public const int MinHeight = 5;

        #endregion

        #region Properties

        /// <summary>
        ///     Cena uzlu
        /// </summary>
        public int Cost => Jumps.Count;

        /// <summary>
        ///     Vráti èi aktuálny uzol je koneènı uzol
        /// </summary>
        /// <returns>Vráti èi aktuálny uzol je koneènı uzol</returns>
        public bool Victory => Remaining == 0;

        /// <summary>
        ///     Vráti èi aktuálny uzol je slepá ulièka
        /// </summary>
        /// <returns>Vráti èi aktuálny uzol je slepá ulièka</returns>
        public bool Failed => Remaining != 0 && Cost == 0;

        #endregion

        #region Attributes

        /// <summary>
        ///     Urèuje ktoré políèka na šachovnici u boli navštívené
        /// </summary>
        public readonly bool[,] Used;

        /// <summary>
        ///     Šírka šachovnice
        /// </summary>
        public readonly int Width;

        /// <summary>
        ///     Vıška šachovnice
        /// </summary>
        public readonly int Height;

        /// <summary>
        ///     Horizontálna súradnica kde sa práve nachádza kôò
        /// </summary>
        public readonly int X;

        /// <summary>
        ///     Vertikálna súradnica kde sa práve nachádza kôò
        /// </summary>
        public readonly int Y;

        /// <summary>
        ///     Poèet neobsadenıch políèok v šachovnici
        /// </summary>
        public readonly int Remaining;

        /// <summary>
        ///     Doèasne uloené dostupné políèka kam môe kôò skoèi
        /// </summary>
        public readonly List<Tuple<int, int>> Jumps = new List<Tuple<int, int>>(8);

        /// <summary>
        ///     Cesta, ktorou som sa dostal do tohto uzlu
        /// </summary>
        public readonly Tuple<int, int>[] Path;

        /// <summary>
        ///     Pozícia v poli Cesta
        /// </summary>
        private readonly int _pathIndex;

        #endregion

        #region Constructors

        /// <summary>
        ///     Vytvor novı uzol
        /// </summary>
        /// <param name="width">Šírka stavu</param>
        /// <param name="height">Vıška stavu</param>
        /// <param name="x">Horizontálna pozícia koòa</param>
        /// <param name="y">Vertikálna pozícia koòa</param>
        /// <exception cref="IndexOutOfRangeException" />
        /// <exception cref="ArgumentException" />
        public Uzol(int width, int height, int x, int y)
        {
            if (width > MaxWidth || width < MinWidth)
                throw new ArgumentException("Wrong Width", nameof(width));
            if (height > MaxHeight || height < MinHeight)
                throw new ArgumentException("Wrong Height", nameof(height));

            Used = new bool[width, height];
            Width = width;
            Height = height;

            X = x;
            Y = y;

            Used[X, Y] = true;
            Path = new Tuple<int, int>[Width * Height];
            Remaining = Width * Height - 1;
            Path[_pathIndex++] = new Tuple<int, int>(x, y);

            GenerateJumps();
        }

        /// <summary>
        ///     Vytvorí novı uzol na základe druhého uzla
        /// </summary>
        /// <param name="uzol">Uzol, z ktorého ma skopírova rozloenie</param>
        /// <param name="horse">Nová pozícia koòa</param>
        /// <exception cref="IndexOutOfRangeException" />
        public Uzol(Uzol uzol, Tuple<int, int> horse)
        {
            Width = uzol.Width;
            Height = uzol.Height;
            Used = new bool[Width, Height];

            for (var x = 0; x < Width; x++)
            for (var y = 0; y < Height; y++)
                Used[x, y] = uzol.Used[x, y];

            X = horse.Item1;
            Y = horse.Item2;

            Used[horse.Item1, horse.Item2] = true;
            Path = new Tuple<int, int>[uzol.Path.Length];
            Remaining = uzol.Remaining - 1;

            _pathIndex = uzol._pathIndex;
            for (var i = 0; i < _pathIndex; i++)
                Path[i] = uzol.Path[i];
            Path[_pathIndex++] = horse;

            GenerateJumps();
        }

        #endregion
    }
}