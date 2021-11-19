using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIPToolKit.Tools
{
    public static class MathExtensions
    {
        public static bool IsInRange<T>(this T testValue, T number1, T number2)
        {
            return Comparer<T>.Default.Compare(testValue, number1) != Comparer<T>.Default.Compare(testValue, number2);
        }

        public static bool IsBetween(this double testValue, double bound1, double bound2)
        {
            return (testValue >= Math.Min(bound1, bound2) && testValue <= Math.Max(bound1, bound2));
        }

        public static bool IsBetween<T>(this T item, T start, T end)
        {
            return Comparer<T>.Default.Compare(item, start) >= 0
                && Comparer<T>.Default.Compare(item, end) <= 0;
        }
    }
}
