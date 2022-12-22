using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTNT
{
    class Edge
    {
        public string u;
        public string v;
        public Edge(string _u, string _v)
        {
            u = _u;
            v = _v;
        }
    }

    class Graph
    {
        public List<Edge> edges = new List<Edge>();

        public Graph(string path)
        {
            string[] data = System.IO.File.ReadAllLines(path);
            foreach (var item in data)
            {
                string[] s = item.Split(' ');
                edges.Add(new Edge(s[0], s[1]));
                edges.Add(new Edge(s[1], s[0]));
            }
        }

        public void BFS()
        {
            Console.Write("Nhap dinh bat dau: ");
            string s = Console.ReadLine();
            Console.Write("Nhap dinh ket thuc: ");
            string f = Console.ReadLine();
            Dictionary<string, string> parents = new Dictionary<string, string>();
            foreach (var item in edges)
            {
                if (!parents.ContainsKey(item.u))
                {
                    parents.Add(item.u, null);
                }
                if (!parents.ContainsKey(item.v))
                {
                    parents.Add(item.v, null);
                }
                List<string> visited = new List<string>();
                Queue<string> q = new Queue<string>();
                q.Enqueue(s);
                while (q.Count != 0)
                {
                    string t = q.Peek();
                    visited.Add(t);
                    q.Dequeue();
                    List<string> dsKe = edges.Where(p => p.u == t).Select(p => p.v).ToList();
                    foreach (var d in dsKe)
                    {
                        if (!visited.Contains(d))
                        {
                            q.Enqueue(d);
                            parents[d] = t;
                        }
                    }
                }
            }

            PrintPath(s, f, parents);
        }

        public void DFS()
        {
            Console.Write("Nhap dinh bat dau: ");
            string s = Console.ReadLine();
            Console.Write("Nhap dinh ket thuc: ");
            string f = Console.ReadLine();
            Dictionary<string, string> parents = new Dictionary<string, string>();
            foreach (var item in edges)
            {
                if (!parents.ContainsKey(item.u))
                {
                    parents.Add(item.u, null);
                }
                if (!parents.ContainsKey(item.v))
                {
                    parents.Add(item.v, null);
                }
                List<string> visited = new List<string>();
                Stack<string> q = new Stack<string>();
                q.Push(s);
                while (q.Count != 0)
                {
                    string t = q.Peek();
                    visited.Add(t);
                    q.Pop();
                    List<string> dsKe = edges.Where(p => p.u == t).Select(p => p.v).ToList();
                    foreach (var d in dsKe)
                    {
                        if (!visited.Contains(d))
                        {
                            q.Push(d);
                            parents[d] = t;
                        }
                    }
                }
            }

            PrintPath(s, f, parents);
        }

        public void PrintPath(string s, string f, Dictionary<string, string> parents)
        {
            if (s == f)
                Console.Write(f + " ");
            else if (parents[f] == null)
                Console.WriteLine("Khong tim thay duong di!");
            else
            {
                PrintPath(s, parents[f], parents);
                Console.Write(f + " ");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Duyet do thi BFS");
            Graph g1 = new Graph("graph.txt");
            g1.BFS();

            Console.WriteLine("\n\nDuyet do thi DFS");
            Graph g2 = new Graph("graph.txt");
            g2.DFS();
            Console.ReadKey();
        }
    }
}
