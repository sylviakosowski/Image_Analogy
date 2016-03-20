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

        public int cost; //The current cost of getting to this node, used in dijkstra's
        public bool visited; //Whether this node has been visited or not, used in dijkstra's
        public bool permanent; //Whether this node has a permanent cost, useed in dijkstra's
        public Node parent; //The node which came before this node in the shortest path

        //Adjacency list, stores the neighbor nodes of this pixel along with its edge weight
        public List<Tuple<Node,int>> neighbors; 

        public Node(int x, int y, Color diff)
        {
            this.x = x;
            this.y = y;
            this.diff = diff;
            cost = Int32.MaxValue;
            visited = false;
            permanent = false;
            parent = null;
            neighbors = new List<Tuple<Node,int>>();
        }

        public void addNeighbor(Node node, int weight)
        {
            neighbors.Add(new Tuple<Node,int>(node,weight));
        }
    }
}
