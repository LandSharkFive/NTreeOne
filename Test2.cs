using NTreeOne;
using System.Diagnostics;

namespace UnitTest
{

    [TestClass]
    public sealed class Test2
    {
        [TestMethod]
        public void TestOne()
        {
            Random rnd = new Random();

            List<int> a = new List<int>();
            for (int i = 0; i < 1000; i++)
            {
                a.Add(rnd.Next(10000));
            }

            // Remove duplicates.
            a = a.Distinct().ToList();

            var timer = new Stopwatch();
            timer.Start();

            Tree t = new Tree();
            for (int i = 0; i < a.Count; i++)
            {
                t.Add(a[i]);
            }

            timer.Stop();
            TimeSpan myTime = timer.Elapsed;
            Console.WriteLine("add {0} ms", myTime.TotalMilliseconds);
            Console.WriteLine("memory {0} mb", Util.GetMemory());

            Assert.IsTrue(t.Height() > 0);
            Assert.IsTrue(t.Count() > 0);
            Assert.IsTrue(Util.IsSorted(t.GetData()));
            Console.WriteLine("height {0}", t.Height());

            timer.Reset();
            timer.Start();

            t.Rebuild();

            timer.Stop();
            myTime = timer.Elapsed;
            Console.WriteLine("build {0} ms", myTime.TotalMilliseconds);
            Console.WriteLine("memory {0} mb", Util.GetMemory());

            Assert.IsTrue(t.Height() > 0);
            Assert.IsTrue(t.Count() > 0);
            Assert.IsTrue(Util.IsSorted(t.GetData()));
            Console.WriteLine("height {0}", t.Height());
        }

    }
}