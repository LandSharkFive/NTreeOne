namespace NTreeOne
{
    public class Node
    {
        private const int MaxSize = 60;

        public int Key;
        public List<Node> Child;
        public List<int> Data;

        public Node()
        {
            Key = 0;
            Child = new List<Node>();
            Data = new List<int>();
        }


        public Node(int a)
        {
            Child = new List<Node>();
            Data = new List<int>();
            Data.Add(a);
        }

        bool IsLeaf()
        {
            return Child.Count == 0;
        }

        /// <summary>
        /// Count nodes.
        /// </summary>
        /// <param name="a">int</param>
        /// <returns>int</returns>
        public int Count(Node a)
        {
            if (a == null)
            {
                return 0;
            }
            int count = 0;
            foreach(Node c in a.Child)
            {
                count += Count(c);
            }
            return 1 + count;
        }

        /// <summary>
        /// Get Height of node.
        /// </summary>
        /// <returns>int</returns>
        public int Height(Node n)
        {
            if (n == null)
            {
                return 0;
            }
            int max = 0;
            foreach(Node c in n.Child)
            {
                int h = Height(c);
                if (h > max)
                {
                    max = h;
                }
            }
            return 1 + max;
        }


        public void Add(Node n, int a)
        {
            if (n.IsLeaf())
            {
                AddToLeaf(n, a);
                return;
            }

            Node b = GetLeaf(n, a);
            if (b != null)
            {
                AddToLeaf(b, a);
                return;
            }
        }

        private void AddToLeaf(Node n, int a)
        {
            n.Data.Add(a);
            n.Data.Sort();
            if (n.Data.Count > MaxSize)
            {
                SplitLeaf(n);
            }
        }

        public Node GetLeaf(Node n, int a)
        {
            if (n == null)
            {
                return null;
            }
            if (n.IsLeaf())
            {
                return n;
            }
            for (int i = 0; i < n.Child.Count - 1; i++)
            {
                if (a < n.Child[i].Key)
                {
                    return n.GetLeaf(n.Child[i], a);
                }
            }
            return n.GetLeaf(n.Child[n.Child.Count - 1], a);
        }


        public void SplitLeaf(Node n)
        {
            if (n.IsLeaf())
            {
                Node a = new Node();
                Node b = new Node();
                Node c = new Node(); 
                int mid = n.Data.Count / 2;
                for (int i = 0; i < mid; i++)
                {
                    a.Data.Add(n.Data[i]);
                }
                for (int j = mid; j < n.Data.Count - 1; j++) 
                {
                    b.Data.Add(n.Data[j]);
                }
                c.Data.Add(n.Data[n.Data.Count - 1]);
                n.Data.Clear();
                a.Key = b.Data[0];
                b.Key = c.Data[0]; 
                c.Key = 0; 
                n.Child.Add(a);
                n.Child.Add(b);
                n.Child.Add(c);
            }
        }

        /// <summary>
        /// Get keys from index nodes.
        /// </summary>
        /// <returns>list</returns>
        public List<int> GetKey(Node n)
        {
            if (n == null)
            {
                return new List<int>();
            }
            List<int> result = new List<int>();
            if (!IsLeaf())
            {
                result.Add(Key);
            }
            foreach (Node a in n.Child)
            {
                result.AddRange(n.GetKey(a));
            }
            return result;
        }

        public List<int> GetData(Node n)
        {
            if (n == null)
            {
                return new List<int>();
            }
            List<int> result = new List<int>();
            if (n.IsLeaf())
            {
                result.AddRange(n.Data);
                return result;
            }
            foreach (var c in n.Child)
            {
                result.AddRange(GetData(c));
            }
            return result;
        }

        public bool Exist(Node n, int a)
        {
            Node b = GetLeaf(n, a);
            if (b == null)
            {
                return false;
            }
            return b.Data.Contains(a);
        }


        public void Delete(Node n, int a)
        {
            Node b = GetLeaf(n, a);
            if (b != null)
            {
                RemoveFromLeaf(b, a);
            }
        }

        public void RemoveFromLeaf(Node n, int a)
        {
            if (n.IsLeaf())
            {
                n.Data.Remove(a);
            }
        }

        /// <summary>
        /// Write Data to Stream.
        /// </summary>
        /// <param name="sw">StreamWriter</param>
        public void WriteToStream(StreamWriter sw)
        {
            if (IsLeaf())
            {
                foreach (int x in Data)
                {
                    sw.WriteLine(x);
                }
            }
            foreach (Node a in Child)
            {
                a.WriteToStream(sw);
            }    
        }

    }
}
