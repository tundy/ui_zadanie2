using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EulerovKon
{
    public class Stav
    {
        #region Attributes

        /// <summary>
        ///     Určuje ktoré políčka na šachovnici už boli navštívené
        /// </summary>
        public readonly bool[,] Used;

        /// <summary>
        ///     Šírka šachovnice
        /// </summary>
        public readonly int Width;

        /// <summary>
        ///     Výška šachovnice
        /// </summary>
        public readonly int Height;

        /// <summary>
        ///     Horizontálna súradnica kde sa práve nachádza kôň
        /// </summary>
        public readonly int X;

        /// <summary>
        ///     Vertikálna súradnica kde sa práve nachádza kôň
        /// </summary>
        public readonly int Y;

        /// <summary>
        ///     Stav, z ktorého som sa sem dostal
        /// </summary>
        public Stav From;

        #endregion

        #region Constructors

        /// <summary>
        ///     Vytvor nový stav
        /// </summary>
        /// <param name="width">Šírka stavu</param>
        /// <param name="height">Výška stavu</param>
        /// <param name="x">Horizontálna pozícia koňa</param>
        /// <param name="y">Vertikálna pozícia koňa</param>
        /// <exception cref="IndexOutOfRangeException" />
        /// <exception cref="ArgumentException" />
        public Stav(int width, int height, int x, int y)
        {
            if (width > Search.MaxWidth || width < Search.MinWidth)
                throw new ArgumentException("Wrong Width", nameof(width));
            if (height > Search.MaxHeight || height < Search.MinHeight)
                throw new ArgumentException("Wrong Height", nameof(height));

            Used = new bool[width, height];
            Width = width;
            Height = height;

            X = x;
            Y = y;

            Used[X, Y] = true;

            From = null;
        }

        /// <summary>
        ///     Vytvorí nový stav na základe predchadzajúceho stavu
        /// </summary>
        /// <param name="stav">Stav, z ktorého ma skopírovať rozloženie</param>
        /// <param name="horse">Nová pozícia koňa</param>
        /// <exception cref="IndexOutOfRangeException" />
        public Stav(Stav stav, Tuple<int, int> horse)
        {
            Width = stav.Width;
            Height = stav.Height;
            Used = new bool[Width, Height];

            for (var x = 0; x < Width; x++)
                for (var y = 0; y < Height; y++)
                    Used[x, y] = stav.Used[x, y];

            X = horse.Item1;
            Y = horse.Item2;

            Used[horse.Item1, horse.Item2] = true;

            From = stav;
        }

        #endregion
    }
}
