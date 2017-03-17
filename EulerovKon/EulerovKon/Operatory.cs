using System;
using System.Collections.Generic;

namespace EulerovKon
{
    public static class Operatory
    {
        public delegate Tuple<int, int> Operator(Uzol uzol);

        public static readonly Operator[] AllOperations = {
            O1,
            O2,
            O3,
            O4,
            O5,
            O6,
            O7,
            O8
        };

        private static Tuple<int, int> O1(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 1, uzol.Y - 2);
            return result.Item1 < 0 || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O2(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X + 1, uzol.Y - 2);
            return result.Item1 >= uzol.Width || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O3(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X + 2, uzol.Y - 1);
            return result.Item1 >= uzol.Width || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O4(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X + 2, uzol.Y + 1);
            return result.Item1 >= uzol.Width || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O5(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X + 1, uzol.Y + 2);
            return result.Item1 >= uzol.Width || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O6(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 1, uzol.Y + 2);
            return result.Item1 < 0 || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O7(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 2, uzol.Y + 1);
            return result.Item1 < 0 || result.Item2 >= uzol.Height || uzol.Used[result.Item1, result.Item2] ? null : result;
        }

        private static Tuple<int, int> O8(Uzol uzol)
        {
            var result = new Tuple<int, int>(uzol.X - 2, uzol.Y - 1);
            return result.Item1 < 0 || result.Item2 < 0 || uzol.Used[result.Item1, result.Item2] ? null : result;
        }
    }
}