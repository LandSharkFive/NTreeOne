namespace NTreeOne
{
    public class Tree
    {
        private const int MaxSize = 60;

        public Node Root;

        public Tree()
        {
            Root = null;
        }

        public Tree(int a)
        {
            Root = new Node(a);
        }

        public void Clear()
        {
            Root = null;
        }

        /// <summary>
        /// Count nodes in tree.
        /// </summary>
        /// <returns>int</returns>
        public int Count()
        {
            if (Root == null)
                return 0;
            else
                return Root.Count(Root);
        }

        /// <summary>
        /// Get tree height.
        /// </summary>
        /// <returns>int</returns>
        public int Height()
        {
            if (Root == null)
            {
                return 0;
            }

            return Root.Height(Root);
        }

        public void Add(int a)
        {
            if (Root == null)
            {
                Root = new Node();
            }

            Root.Add(Root, a);
        }

        public void Delete(int a)
        {
            if (Root == null)
                return;

            Root.Delete(Root, a);
        }

        public void AddRange(List<int> a)
        {
            if (Root == null)
            {
                Root = new Node();
            }

            foreach (int x in a)
            {
                Add(x);
            }
        }

        public List<int> GetData()
        {
            if (Root == null)
            {
                return new List<int>();
            }

            return Root.GetData(Root);
        }
        
        public bool Exist(int a)
        {
            if (Root == null)
            {
                return false;
            }

            return Root.Exist(Root, a);
        }


        public void Rebuild()
        {
            List<int> list = Root.GetData(Root);
            list.Sort();
            Root = BuildTwo(list, 0, list.Count - 1, MaxSize);
        }

        public Node Build(List<int> list, int start, int end, int size)
        {
            Node a = new Node();
            if (start + size > end)
            {
                // leaf
                a.Key = 0;
                a.Data = list.GetRange(start, end - start + 1);
                return a;
            }

            // index
            int mid = (start + end) / 2;
            a.Child.Add(Build(list, start, mid, size));
            a.Child.Add(Build(list, mid + 1, end, size));
            a.Child[0].Key = list[mid + 1];
            return a;
        }

        public Node BuildTwo(List<int> list, int start, int end, int size)
        {
            Node a = new Node();
            if (start + size > end)
            {
                // leaf
                a.Key = 0;
                a.Data = list.GetRange(start, end - start + 1);
                return a;
            }

            // index
            int mid = (start + end) / 2;
            int m1 = (start + mid) / 2;
            int m2 = (mid + end) / 2;
            a.Child.Add(BuildTwo(list, start, m1, size));
            a.Child.Add(BuildTwo(list, m1 + 1, mid, size));
            a.Child.Add(BuildTwo(list, mid + 1, m2, size));
            a.Child.Add(BuildTwo(list, m2 + 1, end - 1, size));
            a.Child.Add(BuildTwo(list, end, end, size));
            a.Child[0].Key = list[m1 + 1];
            a.Child[1].Key = list[mid + 1];
            a.Child[2].Key = list[m2 + 1];
            a.Child[3].Key = list[end];
            a.Child[4].Key = 0;
            return a;
        }


        /// <summary>
        /// Write to file.
        /// </summary>
        /// <param name="fileName">string</param>
        public void WriteToFile(string fileName)
        {
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                Root.WriteToStream(sw);
            }
        }

        /// <summary>
        /// Read file
        /// </summary>
        /// <param name="fileName">string</param>
        public void ReadFile(string fileName)
        {
            List<int> list = new List<int>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    int a = 0;
                    if (int.TryParse(line, out a))
                    {
                        list.Add(a);
                    }
                }
            }

            Root = BuildTwo(list, 0, list.Count - 1, MaxSize);
        }



    }
}
