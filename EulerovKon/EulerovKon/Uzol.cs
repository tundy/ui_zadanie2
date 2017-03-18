using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EulerovKon
{
    /// <summary>
    ///     Objekt obsahuj�ci inform�cie o aktu�lnom rozlo�en� �achovnice
    /// </summary>
    [DebuggerDisplay("X:{X}, Y:{Y}, C:{Cost}, R:{Remaining}")]
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
                var skok = @operator(this);
                if (skok == null) continue;
                Jumps.Add(skok);
            }
        }

        #endregion

        #region Constants

        /// <summary>
        ///     Maxim�lna ��rka �achovnice
        /// </summary>
        public const int MaxWidth = 20;

        /// <summary>
        ///     Maxim�lna v��ka �achovnice
        /// </summary>
        public const int MaxHeight = 20;

        /// <summary>
        ///     Minim�lna ��rka �achovnice
        /// </summary>
        public const int MinWidth = 5;

        /// <summary>
        ///     Minim�lna v��ka �achovnice
        /// </summary>
        public const int MinHeight = 5;

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
        public bool Victory => Remaining == 0;

        /// <summary>
        ///     Vr�ti �i aktu�lny uzol je slep� uli�ka
        /// </summary>
        /// <returns>Vr�ti �i aktu�lny uzol je slep� uli�ka</returns>
        public bool Failed => Remaining != 0 && Cost == 0;

        #endregion

        #region Attributes

        /// <summary>
        ///     Ur�uje ktor� pol��ka na �achovnici u� boli nav�t�ven�
        /// </summary>
        public readonly bool[,] Used;

        /// <summary>
        ///     ��rka �achovnice
        /// </summary>
        public readonly int Width;

        /// <summary>
        ///     V��ka �achovnice
        /// </summary>
        public readonly int Height;

        /// <summary>
        ///     Horizont�lna s�radnica kde sa pr�ve nach�dza k��
        /// </summary>
        public readonly int X;

        /// <summary>
        ///     Vertik�lna s�radnica kde sa pr�ve nach�dza k��
        /// </summary>
        public readonly int Y;

        /// <summary>
        ///     Po�et neobsaden�ch pol��ok v �achovnici
        /// </summary>
        public readonly int Remaining;

        /// <summary>
        ///     Do�asne ulo�en� dostupn� pol��ka kam m��e k�� sko�i�
        /// </summary>
        public readonly List<Tuple<int, int>> Jumps = new List<Tuple<int, int>>(8);

        /// <summary>
        ///     Cesta, ktorou som sa dostal do tohto uzlu
        /// </summary>
        public readonly Tuple<int, int>[] Path;

        /// <summary>
        ///     Poz�cia v poli Cesta
        /// </summary>
        private readonly int _pathIndex;

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
        ///     Vytvor� nov� uzol na z�klade druh�ho uzla
        /// </summary>
        /// <param name="uzol">Uzol, z ktor�ho ma skop�rova� rozlo�enie</param>
        /// <param name="horse">Nov� poz�cia ko�a</param>
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