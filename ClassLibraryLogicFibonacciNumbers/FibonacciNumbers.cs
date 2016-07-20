using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibraryLogicFibonacciNumbers
{
    public sealed class FibonacciNumbers
    {
        #region Method GetFibonacciNumbers

        /// <summary>
        /// Method GetFibonacciNumbers return sequence of fibonacci numbers.
        /// </summary>
        /// <param name="count">Count of fibonacci numbers.</param>
        /// <returns>Fibonacci numbers.</returns>
        public static IEnumerable<int> GetFibonacciNumbers(int count)
        {
            if (count == 0)
                yield return 0;

            int prev = 0;
            int next = 1;

            for (int i = 0; i < count; i++)
            {
                int temp = prev;
                prev = next;
                next = temp + next;
                yield return prev;
            }
        }

        #endregion
    }
}
