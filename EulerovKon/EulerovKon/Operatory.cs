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
        /// <param name="uzol">Uzol obsahuj�ci stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        public delegate Tuple<int, int> Operator(Uzol uzol);

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
        /// <param name="uzol">Uzol obsahuj�ci stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O1(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 1, uzol.Y - 2);
            return result.Item1 < 0 || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2] ? null : result;
        }

        /// <summary>
        ///     Skok nahor doprava
        /// </summary>
        /// <param name="uzol">Uzol obsahuj�ci stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O2(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X + 1, uzol.Y - 2);
            return result.Item1 >= uzol.Width || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok vpravo hore
        /// </summary>
        /// <param name="uzol">Uzol obsahuj�ci stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O3(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X + 2, uzol.Y - 1);
            return result.Item1 >= uzol.Width || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok vpravo dole
        /// </summary>
        /// <param name="uzol">Uzol obsahuj�ci stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O4(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X + 2, uzol.Y + 1);
            return result.Item1 >= uzol.Width || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok nadol doprava
        /// </summary>
        /// <param name="uzol">Uzol obsahuj�ci stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O5(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X + 1, uzol.Y + 2);
            return result.Item1 >= uzol.Width || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok nadol do�ava
        /// </summary>
        /// <param name="uzol">Uzol obsahuj�ci stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O6(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 1, uzol.Y + 2);
            return result.Item1 < 0 || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok v�avo dole
        /// </summary>
        /// <param name="uzol">Uzol obsahuj�ci stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O7(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 2, uzol.Y + 1);
            return result.Item1 < 0 || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok v�avo hore
        /// </summary>
        /// <param name="uzol">Uzol obsahuj�ci stav na ktorom sa bude vykon�va� oper�tor</param>
        /// <returns>Poz�ciu kam sko�� k�� ak je to mo�n�, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O8(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 2, uzol.Y - 1);
            return result.Item1 < 0 || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2] ? null : result;
        }
    }
}