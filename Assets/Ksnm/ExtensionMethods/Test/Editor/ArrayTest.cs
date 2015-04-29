using NUnit.Framework;

namespace Ksnm.ExtensionMethods.System
{
    [TestFixture]
    public class ArrayTest
    {
        /// <summary>
        /// 等しいと判定されれば正解
        /// </summary>
        [Test]
        [TestCase(new[] { 0 }, new[] { 0 })]
        [TestCase(new[] { -1 }, new[] { -1 })]
        [TestCase(new[] { 0, 1 }, new[] { 0, 1 })]
        public void CompareEqual(int[] arrayA, int[] arrayB)
        {
            var result = arrayA.Compare(arrayB);
            Assert.AreEqual(result, 0);
        }
        /// <summary>
        /// arrayAが大きいと判定されれば正解
        /// </summary>
        [Test]
        [TestCase(new[] { 1 }, new[] { 0 })]
        [TestCase(new[] { 0 }, new[] { -1 })]
        [TestCase(new[] { 0, 1 }, new[] { 0 })]
        public void CompareLarge(int[] arrayA, int[] arrayB)
        {
            var result = arrayA.Compare(arrayB);
            Assert.AreEqual(result, +1);
        }
        /// <summary>
        /// arrayAが小さいと判定されれば正解
        /// </summary>
        [Test]
        [TestCase(new[] { 0 }, new[] { 1 })]
        [TestCase(new[] { -1 }, new[] { 0 })]
        [TestCase(new[] { 0 }, new[] { 0, 1 })]
        public void CompareSmall(int[] arrayA, int[] arrayB)
        {
            var result = arrayA.Compare(arrayB);
            Assert.AreEqual(result, -1);
        }
    }
}
