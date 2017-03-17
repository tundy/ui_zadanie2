using System;
using System.Collections.Generic;

namespace EulerovKon
{
    /// <summary>
    /// Objekt obsahuj�ci inform�cie o aktu�lnom rozlo�en� �achovnice
    /// </summary>
    public class Stav
    {
        #region Attributes
        /// <summary>
        /// Ur�uje ktor� pol��ka na �achovnici u� boli nav�t�ven�
        /// </summary>
        public readonly bool[,] Used;
        /// <summary>
        /// ��rka �achovnice
        /// </summary>
        public readonly int Width;
        /// <summary>
        /// V��ka �achovnice
        /// </summary>
        public readonly int Height;
        /// <summary>
        /// Horizont�lna s�radnica kde sa pr�ve nach�dza k��
        /// </summary>
        public readonly int X;
        /// <summary>
        /// Vertik�lna s�radnica kde sa pr�ve nach�dza k��
        /// </summary>
        public readonly int Y;
        /// <summary>
        /// Po�et neobsaden�ch pol��ok v �achovnici
        /// </summary>
        public readonly int Remaining;
        /// <summary>
        /// Cena stavu
        /// </summary>
        public int Cost => Jumps.Count;
        /// <summary>
        /// Dostupn� pol��ka kam m��e k�� sko�i�
        /// </summary>
        public readonly List<Tuple<int,int>> Jumps = new List<Tuple<int, int>>(8);
        /// <summary>
        /// Cesta, ktorou som sa dostal do tohto stavu
        /// </summary>
        public readonly Tuple<int, int>[] Path;
        /// <summary>
        /// Poz�cia v poli Cesta
        /// </summary>
        private readonly int _pathIndex;
        #endregion

        #region Constructors
        /// <summary>
        /// Vytvor nov� stav
        /// </summary>
        /// <param name="width">��rka stavu</param>
        /// <param name="height">V��ka stavu</param>
        /// <param name="x">Horizont�lna poz�cia ko�a</param>
        /// <param name="y">Vertik�lna poz�cia ko�a</param>
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
        /// Vytvor� nov� stav na z�klade druh�ho stavu
        /// </summary>
        /// <param name="stav">Stav, z ktor�ho ma skop�rova� rozlo�enie</param>
        /// <param name="horse">Nov� poz�cia ko�a</param>
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
        /// Vr�ti �i aktu�lny stav je kone�n� stav
        /// </summary>
        /// <returns>Vr�ti �i aktu�lny stav je kone�n� stav</returns>
        public bool Victory() => Remaining == 0;

        /// <summary>
        /// Vr�ti �i aktu�lny stav je slep� uli�ka
        /// </summary>
        /// <returns>Vr�ti �i aktu�lny stav je slep� uli�ka</returns>
        public bool Failed() => Remaining != 0 && Cost == 0;

        /// <summary>
        /// Vr�ti �i pol��ko v �achovnici je obsaden�
        /// </summary>
        /// <param name="index">Tuple obsahuj�ci horizont�lnu poz�ciu na prvom mieste a vertik�lnu na druhom</param>
        /// <returns>Obsadenos� pol��ka v �achovnici</returns>
        public bool GetUsed(Tuple<int, int> index) => Used[index.Item1, index.Item2];
        #endregion

        #region Private Methdos
        /// <summary>
        /// Vygeneruje nov� dostupn� poz�cie kam m��e k�� sko�i�, na z�klade oper�torov
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