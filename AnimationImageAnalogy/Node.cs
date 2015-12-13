using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnimationImageAnalogy
{
    /* Class to represent the nodes in the PatchGraph */
    class Node
    {
        public int x; //X coordinate of the pixel which this node represents
        public int y; //y coordinate of the pixel which this node represents

        public Color diff; //The color difference at this node location

        public int distance; //used in dijkstra's
        public bool visited;

        //Adjacency list, stores the neighbor nodes of this pixel along with its edge weight
        public List<Tuple<Node,int>> neighbors; 

        public Node(int x, int y, Color diff)
        {
            this.x = x;
            this.y = y;
            this.diff = diff;
            distance = Int32.MaxValue;
            visited = false;
            neighbors = new List<Tuple<Node,int>>();
        }

        public void addNeighbor(Node node, int weight)
        {
            neighbors.Add(new Tuple<Node,int>(node,weight));
        }
    }
}
