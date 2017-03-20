using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EulerovKon
{
    /// <summary>
    ///     Objekt obsahujúci informácie o aktuálnom rozloení šachovnice
    /// </summary>
    [DebuggerDisplay("X:{Stav.X}, Y:{Stav.Y}, C:{Cost}, R:{_remaining}")]
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
                var skok = @operator(Stav);
                if (skok == null) continue;
                Jumps.Add(skok);
            }
        }

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
        public bool Victory => _remaining == 0;

        /// <summary>
        ///     Vráti èi aktuálny uzol je slepá ulièka
        /// </summary>
        /// <returns>Vráti èi aktuálny uzol je slepá ulièka</returns>
        public bool Failed => _remaining != 0 && Cost == 0;

        #endregion

        #region Attributes

        /// <summary>
        ///     Poèet neobsadenıch políèok v šachovnici
        /// </summary>
        private readonly int _remaining;

        /// <summary>
        ///     Doèasne uloené dostupné políèka kam môe kôò skoèi
        /// </summary>
        public readonly List<Tuple<int, int>> Jumps = new List<Tuple<int, int>>(8);

        /// <summary>
        ///     Aktuálny stav šachovnice
        /// </summary>
        public readonly Stav Stav;

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
            Stav = new Stav(width, height, x, y);
            _remaining = Stav.Width * Stav.Height - 1;
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
            Stav = new Stav(uzol.Stav, horse);
            _remaining = uzol._remaining - 1;
            GenerateJumps();
        }

        #endregion
    }
}