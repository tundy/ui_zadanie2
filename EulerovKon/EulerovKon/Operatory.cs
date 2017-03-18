using System;

namespace EulerovKon
{
    /// <summary>
    ///     Statická trieda obsahjúca bšetky dostupné operátory
    /// </summary>
    internal static class Operatory
    {
        /// <summary>
        ///     Metóda operátora
        /// </summary>
        /// <param name="uzol">Uzol obsahujúci stav na ktorom sa bude vykonáva operátor</param>
        /// <returns>Pozíciu kam skoèí kôò ak je to moné, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        public delegate Tuple<int, int> Operator(Uzol uzol);

        /// <summary>
        ///     Zoznam všetkıch operátorov
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
        ///     Skok nahor do¾ava
        /// </summary>
        /// <param name="uzol">Uzol obsahujúci stav na ktorom sa bude vykonáva operátor</param>
        /// <returns>Pozíciu kam skoèí kôò ak je to moné, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O1(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 1, uzol.Y - 2);
            return result.Item1 < 0 || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2] ? null : result;
        }

        /// <summary>
        ///     Skok nahor doprava
        /// </summary>
        /// <param name="uzol">Uzol obsahujúci stav na ktorom sa bude vykonáva operátor</param>
        /// <returns>Pozíciu kam skoèí kôò ak je to moné, inak null</returns>
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
        /// <param name="uzol">Uzol obsahujúci stav na ktorom sa bude vykonáva operátor</param>
        /// <returns>Pozíciu kam skoèí kôò ak je to moné, inak null</returns>
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
        /// <param name="uzol">Uzol obsahujúci stav na ktorom sa bude vykonáva operátor</param>
        /// <returns>Pozíciu kam skoèí kôò ak je to moné, inak null</returns>
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
        /// <param name="uzol">Uzol obsahujúci stav na ktorom sa bude vykonáva operátor</param>
        /// <returns>Pozíciu kam skoèí kôò ak je to moné, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O5(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X + 1, uzol.Y + 2);
            return result.Item1 >= uzol.Width || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok nadol do¾ava
        /// </summary>
        /// <param name="uzol">Uzol obsahujúci stav na ktorom sa bude vykonáva operátor</param>
        /// <returns>Pozíciu kam skoèí kôò ak je to moné, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O6(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 1, uzol.Y + 2);
            return result.Item1 < 0 || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok v¾avo dole
        /// </summary>
        /// <param name="uzol">Uzol obsahujúci stav na ktorom sa bude vykonáva operátor</param>
        /// <returns>Pozíciu kam skoèí kôò ak je to moné, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O7(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 2, uzol.Y + 1);
            return result.Item1 < 0 || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2]
                ? null
                : result;
        }

        /// <summary>
        ///     Skok v¾avo hore
        /// </summary>
        /// <param name="uzol">Uzol obsahujúci stav na ktorom sa bude vykonáva operátor</param>
        /// <returns>Pozíciu kam skoèí kôò ak je to moné, inak null</returns>
        /// <exception cref="IndexOutOfRangeException" />
        private static Tuple<int, int> O8(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 2, uzol.Y - 1);
            return result.Item1 < 0 || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2] ? null : result;
        }
    }
}