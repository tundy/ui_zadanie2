using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EulerovKon
{
    /// <summary>
    ///     Objekt obsahuj�ci inform�cie o aktu�lnom rozlo�en� �achovnice
    /// </summary>
    [DebuggerDisplay("X:{Stav.X}, Y:{Stav.Y}, C:{Cost}, R:{_remaining}")]
    internal class Uzol
    {
        #region Private Methdos

        /// <summary>
        ///     Vygeneruje nov� dostupn� poz�cie kam m��e k�� sko�i�, na z�klade oper�torov
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
        ///     Vr�ti �i aktu�lny uzol je kone�n� uzol
        /// </summary>
        /// <returns>Vr�ti �i aktu�lny uzol je kone�n� uzol</returns>
        public bool Victory => _remaining == 0;

        /// <summary>
        ///     Vr�ti �i aktu�lny uzol je slep� uli�ka
        /// </summary>
        /// <returns>Vr�ti �i aktu�lny uzol je slep� uli�ka</returns>
        public bool Failed => _remaining != 0 && Cost == 0;

        #endregion

        #region Attributes

        /// <summary>
        ///     Po�et neobsaden�ch pol��ok v �achovnici
        /// </summary>
        private readonly int _remaining;

        /// <summary>
        ///     Do�asne ulo�en� dostupn� pol��ka kam m��e k�� sko�i�
        /// </summary>
        public readonly List<Tuple<int, int>> Jumps = new List<Tuple<int, int>>(8);

        /// <summary>
        ///     Aktu�lny stav �achovnice
        /// </summary>
        public readonly Stav Stav;

        #endregion

        #region Constructors

        /// <summary>
        ///     Vytvor nov� uzol
        /// </summary>
        /// <param name="width">��rka stavu</param>
        /// <param name="height">V��ka stavu</param>
        /// <param name="x">Horizont�lna poz�cia ko�a</param>
        /// <param name="y">Vertik�lna poz�cia ko�a</param>
        /// <exception cref="IndexOutOfRangeException" />
        /// <exception cref="ArgumentException" />
        public Uzol(int width, int height, int x, int y)
        {
            Stav = new Stav(width, height, x, y);
            _remaining = Stav.Width * Stav.Height - 1;
            GenerateJumps();
        }

        /// <summary>
        ///     Vytvor� nov� uzol na z�klade druh�ho uzla
        /// </summary>
        /// <param name="uzol">Uzol, z ktor�ho ma skop�rova� rozlo�enie</param>
        /// <param name="horse">Nov� poz�cia ko�a</param>
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