using System;

namespace EulerovKon
{
    /// <summary>
    ///     Statick� trieda obsahj�ca b�etky dostupn� oper�tory
    /// </summary>
    internal static class Operatory
    {
        /// <summary>
        ///     Met�da oper�tora
        /// </summary>
        /// <param name="stav">Stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        public delegate Tuple<int, int> Operator(Stav stav);

        /// <summary>
        ///     Zoznam v�etk�ch oper�torov
        /// </summary>
        public static readonly Operator[] AllOperations =
        {
            O1,
            O2,
            O3,
            O4,
            O5,
            O6,
            O7,
            O8
        };

        /// <summary>
        ///     Skok nahor do�ava
        /// </summary>
        /// <param name="stav">Stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O1(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X - 1, stav.Y - 2);
            return result.Item1 < 0 || result.Item2 < 0 || stav.Used[result.Item1, result.Item2] ? null : result;
        }

        /// <summary>
        ///     Skok nahor doprava
        /// </summary>
        /// <param name="stav">Stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O2(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X + 1, stav.Y - 2);
            return result.Item1 >= stav.Width || result.Item2 < 0 || stav.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok vpravo hore
        /// </summary>
        /// <param name="stav">Stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O3(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X + 2, stav.Y - 1);
            return result.Item1 >= stav.Width || result.Item2 < 0 || stav.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok vpravo dole
        /// </summary>
        /// <param name="stav">Stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O4(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X + 2, stav.Y + 1);
            return result.Item1 >= stav.Width || result.Item2 >= stav.Height || stav.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok nadol doprava
        /// </summary>
        /// <param name="stav">Stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O5(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X + 1, stav.Y + 2);
            return result.Item1 >= stav.Width || result.Item2 >= stav.Height || stav.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok nadol do�ava
        /// </summary>
        /// <param name="stav">Stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O6(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X - 1, stav.Y + 2);
            return result.Item1 < 0 || result.Item2 >= stav.Height || stav.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok v�avo dole
        /// </summary>
        /// <param name="stav">Stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O7(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X - 2, stav.Y + 1);
            return result.Item1 < 0 || result.Item2 >= stav.Height || stav.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok v�avo hore
        /// </summary>
        /// <param name="stav">Stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O8(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X - 2, stav.Y - 1);
            return result.Item1 < 0 || result.Item2 < 0 || stav.Used[result.Item1, result.Item2] ? null : result;
        }
    }
}