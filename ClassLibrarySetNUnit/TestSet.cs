using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibraryLogicSet;
using NUnit.Framework.Internal;
using NUnit.Framework;

namespace ClassLibrarySetNUnit
{
    [TestFixture]
    public class TestSet
    {
        private readonly Set<string> firstSet = new Set<string>(new[] { "One", "Two" });
        private readonly Set<string> secondSet = new Set<string>(new[] { "Two", "Three", "One", "Five" });

        [Test]
        public void TestSet_CountString_ReturnCountSet()
        {
            int expected = 2;

            int actual = firstSet.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSet_UnionString_ReturnUnionSet()
        {
            Set<string> expected = new Set<string>(new[] { "One", "Two", "Three", "Five" });

            Set<string> actual = firstSet.Union(secondSet);
            Set<string> actual2 = firstSet + secondSet;

            Assert.AreEqual(expected.ToEnumerable(), actual.ToEnumerable());
            Assert.AreEqual(expected.ToEnumerable(), actual2.ToEnumerable());
        }

        [Test]
        public void TestSet_ExceptString_ReturnExceptSet()
        {
            Set<string> expected = new Set<string>(new[] { "Three", "Five" });

            Set<string> actual = firstSet.Except(secondSet);
            Set<string> actual2 = firstSet / secondSet;

            Assert.AreEqual(expected.ToEnumerable(), actual.ToEnumerable());
            Assert.AreEqual(expected.ToEnumerable(), actual2.ToEnumerable());
        }

        [Test]
        public void TestSet_IntersectString_ReturnIntersectSet()
        {
            Set<string> expected = new Set<string>(new[] { "Two", "One" });

            Set<string> actual = firstSet.Intersect(secondSet);

            Assert.AreEqual(expected.ToEnumerable(), actual.ToEnumerable());
        }

        [Test]
        public void TestSet_EqualsSet_ReturnFalseTrue()
        {
            bool expected = false;

            bool actual = (firstSet == secondSet);
            
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestSet_EqualsSet_ReturnTrue()
        {
            Set<string> thirdSet = new Set<string>(new[] { "One", "Two" });

            bool expected = true;

            bool actual = (firstSet == thirdSet);

            Assert.AreEqual(expected, actual);
        }
    }
}
