using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Lab15
{




    public class Graph
    {
        public class Node
        {
            public int id;
            public int active;
            public int x;
            public int y;
            public string stringId;
            public int mark = 0;
            public List<int> edges;
            public SolidBrush nodeColor = new SolidBrush(Color.Black);

            public void addEdge(int id)
            {
                if (!edges.Contains(id)) edges.Add(id);
            }

            public void removeEdge(int id)
            {
                edges.Remove(id);
            }
        };

        public List<Node> nodes = new List<Node>();
        public List<bool> used = new List<bool>();
        private int maxId = 0;
        public int x = 0;
        public int y = 0;
        public bool inverted = false;
        public List<List<int>> matrix = new List<List<int>>();
        public void addNode()
        {
            bool find = false;
            int id = 0;
            for (int i = 0; i < maxId; i++)
            {
                bool exist = false;
                foreach (Node nd in nodes)
                {
                    if (nd.id == i)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    id = i;
                    find = true;
                    break;
                }
            }
            if (!find)
            {
                id = maxId;
                maxId++;
            }
            Node n = new Node();
            n.id = id;
            n.active = 0;
            n.x = x;
            n.y = y;
            n.stringId = id.ToString();
            n.mark = 0;
            n.edges = new List<int>();
            nodes.Add(n);

            used.Add(false);
            nodes.Sort((x, y) => x.id.CompareTo(y.id));

            matrix.Clear();
            CreateMatrix();
        }

        public void RemoveNode(int id)
        {
            Node n = null;
            foreach (Node nd in nodes)
            {
                nd.edges.Remove(id);
                if (nd.id == id)
                {
                    n = nd;
                }
            }
            nodes.Remove(n);

            matrix.Clear();
            CreateMatrix();
        }

        public void LoadNode(int id, int x, int y, string name, List<int> e)
        {
            Node n = new Node();
            if (maxId <= id)
                maxId = id + 1;
            n.id = id;
            n.active = 0;
            n.x = x;
            n.y = y;
            n.stringId = id.ToString();
            n.edges = e;
            nodes.Add(n);
            used.Add(false);
            n.mark = 0;

        }

        public void CreateMatrix()
        {
            matrix.Clear();
            for (int i = 0; i < nodes.Count; i++)
            {
                List<int> vershina = new List<int>();
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (nodes[i].edges.Contains(j)) vershina.Add(1);
                    else vershina.Add(0);
                }
                matrix.Add(vershina);
            }
        }

        public void PoiskStokovIstokov(ref List<int> Stoki, ref List<int> Istoki)
        {

            for (int i = 0; i < matrix.Count; i++)
            {
                bool stok = true, istok = true;
                for (int j = 0; j < matrix[i].Count; j++)
                {
                    if (matrix[i][j] == 1) stok = false;
                    if (matrix[j][i] == 1) istok = false;
                }
                if (stok && istok) continue;
                else if (stok) Stoki.Add(i);
                else if (istok) Istoki.Add(i);
            }
        }

        public Node findUnusedVertex(List<Node> list)
        {
            foreach (Node n in list)
            {
                if (!used[list.IndexOf(n)])
                    return n;
            }
            return null;
        }


    }

    public static class additionFunctions
    {
        public static double degToRad(double deg)
        {
            return deg * Math.PI / 180;
        }

        public static double radToDeg(double rad)
        {
            return rad / Math.PI * 180;
        }

        public static double lengthdir_x(double len, double dir)
        {
            return len * Math.Cos(degToRad(dir));
        }

        public static double lengthdir_y(double len, double dir)
        {
            return len * Math.Sin(degToRad(dir)) * (-1);
        }

        public static double point_direction(int x1, int y1, int x2, int y2)
        {
            return 180 - radToDeg(Math.Atan2(y1 - y2, x1 - x2));
        }

        public static double point_distance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }
    }

}
