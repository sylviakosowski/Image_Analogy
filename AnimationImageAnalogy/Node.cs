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

        //Adjacency list, stores the neighbor nodes of this pixel along with its edge weight
        List<Tuple<Node,int>> neighbors; 

        public Node(int x, int y, Color diff)
        {
            this.x = x;
            this.y = y;
            this.diff = diff;
            neighbors = new List<Tuple<Node,int>>();
        }

        public void addNeighbor(Node node, int weight)
        {
            neighbors.Add(new Tuple<Node,int>(node,weight));
        }
    }
}
