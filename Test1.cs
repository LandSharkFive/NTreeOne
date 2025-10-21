using NTreeOne;

namespace UnitTest
{

    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestOne()
        {
            Tree t = new Tree();
            for (int i = 0; i < 1000; i++)
            {
                t.Add(i);
            }

            int data = t.GetData().Count;
            Assert.AreEqual(1000, data);

            t.Rebuild();

            data = t.GetData().Count;
            Assert.AreEqual(1000, data);


            for (int i = 0; i < 1000; i++)
            {
                t.Delete(i);
            }

            data = t.GetData().Count;
            Assert.AreEqual(0, data);
        }

        [TestMethod]
        public void TestTwo()
        {
            Tree t = new Tree();
            for (int i = 0; i < 1000; i++)
            {
                t.Add(i);
            }

            for (int i = 0; i < 1000; i++)
            {
                Assert.IsTrue(t.Exist(i));
            }

            t.Rebuild();
            for (int i = 0; i < 1000; i++)
            {
                Assert.IsTrue(t.Exist(i));
            }
        }

        [TestMethod]
        public void TestThree()
        {
            Tree t = new Tree();
            for (int i = 0; i < 1000; i++)
            {
                t.Add(i);
            }

            Assert.IsTrue(t.Count() > 0);
            Assert.IsTrue(t.Height() > 0);

            t.Rebuild();
            Assert.IsTrue(t.Count() > 0);
            Assert.IsTrue(t.Height() > 0);

            for (int i = 0; i < 1000; i++)
            {
                t.Delete(i);
            }
            Assert.IsTrue(t.Count() > 0);
            Assert.IsTrue(t.Height() > 0);
            Assert.AreEqual(0, t.GetData().Count);
        }

        [TestMethod]
        public void TestFour()
        {
            List<int> a = new List<int>();
            List<int> b = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                a.Add(i);
                b.Add(i);
            }

            Util.Shuffle(a);
            Util.Shuffle(b);

            Tree t = new Tree();
            for (int i = 0; i < a.Count; i++)
            {
                t.Add(a[i]);
            }

            Assert.IsTrue(t.Height() > 0);
            Assert.AreEqual(1000, t.GetData().Count);
            Assert.IsTrue(t.Count() > 0);

            for (int i = 0; i < b.Count; i++)
            {
                t.Delete(i);
            }

            Assert.IsTrue(t.Height() > 0);
            Assert.AreEqual(0, t.GetData().Count);
            Assert.IsTrue(t.Count() > 0);
        }

        [TestMethod]
        public void TestFive()
        {
            Random rnd = new Random();

            List<int> a = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                a.Add(rnd.Next(10000));
            }

            a = a.Distinct().ToList();
            List<int> b = new List<int>();  
            b.AddRange(a);
            Util.Shuffle(b);

            Tree t = new Tree();
            for (int i = 0; i < a.Count; i++)
            {
                t.Add(a[i]);
            }

            Assert.IsTrue(t.Count() > 0);
            Assert.IsTrue(t.Height() > 0);
            Assert.IsTrue(t.GetData().Count > 80);
            Assert.IsTrue(Util.IsSorted(t.GetData()));

            for (int i = 0; i < b.Count; i++)
            {
                t.Delete(b[i]);
            }

            Assert.IsTrue(t.Count() > 0);
            Assert.IsTrue(t.Height() > 0);
            Assert.AreEqual(0, t.GetData().Count);
            Assert.IsTrue(Util.IsSorted(t.GetData()));
        }

    }
}