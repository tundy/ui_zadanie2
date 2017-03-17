using System;
using System.Collections.Generic;

namespace EulerovKon
{
    public static class Operacie
    {
        public delegate Tuple<int, int> Operacia(Stav stav);

        public static readonly Operacia[] AllOperations = {
            O1,
            O2,
            O3,
            O4,
            O5,
            O6,
            O7,
            O8
        };

        private static Tuple<int, int> O1(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X - 1, stav.Y - 2);
            return result.Item1 < 0 || result.Item2 < 0 || stav.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O2(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X + 1, stav.Y - 2);
            return result.Item1 >= stav.Width || result.Item2 < 0 || stav.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O3(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X + 2, stav.Y - 1);
            return result.Item1 >= stav.Width || result.Item2 < 0 || stav.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O4(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X + 2, stav.Y + 1);
            return result.Item1 >= stav.Width || result.Item2 >= stav.Height || stav.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O5(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X + 1, stav.Y + 2);
            return result.Item1 >= stav.Width || result.Item2 >= stav.Height || stav.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O6(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X - 1, stav.Y + 2);
            return result.Item1 < 0 || result.Item2 >= stav.Height || stav.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O7(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X - 2, stav.Y + 1);
            return result.Item1 < 0 || result.Item2 >= stav.Height || stav.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O8(Stav stav)
        {
            var result = new Tuple<int, int>(stav.X - 2, stav.Y - 1);
            return result.Item1 < 0 || result.Item2 < 0 || stav.Used[result.Item1, result.Item2] ? null : result;
        }
    }
}