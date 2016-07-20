using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLogicQueue;
using NUnit.Framework;

namespace ClassLibraryCustomQueueNUnit
{
    [TestFixture]
    public class CustomQueueTests
    {
        #region InputData

        private IEnumerable<TestCaseData> InputData
        {
            get
            {
                yield return new TestCaseData(new CustomQueue<int>(), 0).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData(new CustomQueue<int>(new[] {5, 7, 7, 9, 6}), 5);
            }
        }

        private IEnumerable<TestCaseData> InputDataEnqueue
        {
            get
            {
                yield return new TestCaseData(new CustomQueue<int>(), 10, new[] {10});
                yield return
                    new TestCaseData(new CustomQueue<int>(new[] {5, 7, 7, 9, 6, 10}), 10, new[] {5, 7, 7, 9, 6, 10, 10})
                    ;
            }
        }

        private IEnumerable<TestCaseData> InputDataString
        {
            get
            {
                yield return new TestCaseData(new CustomQueue<string>(), 0).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData(new CustomQueue<string>(new[] {"one", "two"}), "one");
            }
        }

        private IEnumerable<TestCaseData> InputDataEnqueueString
        {
            get
            {
                yield return
                    new TestCaseData(new CustomQueue<string>(new[] {"one", "two"}), "three",
                        new[] {"one", "two", "three"});
            }
        }

        #endregion

        #region Tests

        [Test, TestCaseSource("InputData")]
        public void TestCustomQueue_QueuePeek_ReturnFirstElement(CustomQueue<int> queue, int expected)
        {
            int actual = queue.Peek();
            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("InputDataString")]
        public void TestCustomQueue_StringQueuePeek_ReturnFirstElement(CustomQueue<string> queue, string expected)
        {
            string actual = queue.Peek();
            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("InputDataEnqueue")]
        public void TestCustomQueue_EnqueueForeachQueue_ReturnArray(CustomQueue<int> queue, int element, int[] expected)
        {
            queue.Enqueue(element);

            int[] actual = new int[queue.Count];
            int i = 0;

            foreach (var item in queue)
            {
                actual[i] = item;
                i++;
            }

            Assert.AreEqual(expected, actual);
        }

        [Test, TestCaseSource("InputDataEnqueueString")]
        public void TestCustomQueue_StringEnqueueForeachQueue_ReturnArray(CustomQueue<string> queue, string element,
            string[] expected)
        {
            queue.Enqueue(element);

            string[] actual = new string[queue.Count];
            int i = 0;

            foreach (var item in queue)
            {
                actual[i] = item;
                i++;
            }

            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}