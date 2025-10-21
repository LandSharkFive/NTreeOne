namespace NTreeOne
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TestMe();
        }

        private static void TestMe()
        {
            Tree t = new Tree();
            for (int i = 0; i < 1000; i++)
            {
                t.Add(i);
            }

            Console.WriteLine("nodes " + t.Count());
            Console.WriteLine("height " + t.Height());

            t.Rebuild();

            Console.WriteLine("nodes " + t.Count());
            Console.WriteLine("height " + t.Height());

            for (int i = 0; i < 100; i++)
            {
                t.Delete(i);
            }

            Console.WriteLine("nodes " + t.Count());
            Console.WriteLine("height " + t.Height());

            List<int> list = t.GetData();
            Console.WriteLine(list.Count);
            Util.PrintList(list);
        }
    }
}
