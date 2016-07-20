using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLogicFibonacciNumbers;
using NUnit.Framework;

namespace ClassLibraryFibonacciNumbersNunit
{
    [TestFixture]
    public class FibonacciNumbersTests
    {
        #region InputData

        private IEnumerable<TestCaseData> InputData
        {
            get
            {
                yield return new TestCaseData(5, new[] {1, 1, 2, 3, 5});
            }
        }

        #endregion

        #region Tests

        [Test, TestCaseSource("InputData")]
        public void TestFibonacchi_FibonacchiNumbers_ReturnedFibonacchi(int count, int[] expected)
        {
            int[] actual = new int[count];
            int i = 0;

            foreach (var number in FibonacciNumbers.GetFibonacciNumbers(count))
            {
                actual[i] = number;
                i++;
            }

            Assert.AreEqual(expected, actual, "!=", expected, actual);
        }

        [Test]
        public void TestFibonacchi_FibonacchiNumbers0_ReturnedFibonacchi0()
        {
            int expected = 0;
            int actual = 0;

            foreach (var number in FibonacciNumbers.GetFibonacciNumbers(0))
                actual = number;

            Assert.AreEqual(expected, actual, "!=", expected, actual);
        }

        #endregion
    }
}
