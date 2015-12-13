using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace AnimationImageAnalogy
{
    class PatchGraph
    {
        private int patchDimension;
        private int patchIter;

        public Node[,] graph;

        public Queue<Tuple<int,int>> shortestPath;

        public PatchGraph(int patchDimension, int patchIter)
        {
            this.patchDimension = patchDimension;
            this.patchIter = patchIter;

            graph = null;
            shortestPath = new Queue<Tuple<int, int>>();
        }

        /* Construct a graph out of pixels where the value at each node is the difference 
         * between the pixel components of the existing patch from B2 and the new patch from
         * A2. Edges are weighted with the sum of squared differences between adjacent pixels. */
        public void createGraph(Color[,] imageB2, Color[,] imageA2,
            Tuple<int,int> patchB, Tuple<int,int> patchA, int overlapDimension)
        {
            /* Calculate how wide and tall the overlap is. */
            int xOverlap;
            int yOverlap;

            if(overlapDimension == 0)
            {
                //We are performing dijkstra's for horizontal overlap.
                xOverlap = patchDimension - patchIter;
                yOverlap = patchDimension;
            } else
            {
                //We are performing dijkstra's for vertical overlap.
                xOverlap = patchDimension;
                yOverlap = patchDimension - patchIter;
            }

            graph = new Node[xOverlap, yOverlap];

            int axStart = patchA.Item1;
            int ayStart = patchA.Item2;

            int bxStart = patchB.Item1;
            int byStart = patchB.Item2;

            Console.WriteLine(axStart);
            Console.WriteLine(ayStart);

            //Iterate through the graph, initializing the nodes
            for(int i = 0; i < xOverlap; i++)
            {
                for(int j = 0; j < yOverlap; j++)
                {
                    //Compute difference between each pixel in the overlap
                    Color a = imageA2[axStart+i,ayStart+j];
                    Color b = imageB2[bxStart+patchIter+i, byStart+patchIter+j];

                    int aVal = a.A - b.A > 0 ? a.A - b.A : 0;
                    int rVal = a.R - b.R > 0 ? a.R - b.R : 0;
                    int gVal = a.G - b.G > 0 ? a.G - b.G : 0;
                    int bVal = a.B - b.B > 0 ? a.B - b.B : 0;
                    Color diff = Color.FromArgb(aVal, rVal, gVal, bVal);
                    //Color diff = Color.FromArgb(a.A - b.A, a.R - b.R, a.G - b.G, a.B - b.B);

                    //Create a new node initialied with everything but the neighbors
                    Node newNode = new Node(i, j, diff);
                    graph[i, j] = newNode;
                }
            }
        }


        /* Initialize the graph with the neighbors of each pixel. The edge between each
         * pixel and its neighbor is weighted according to the sum of squared differences
         * between the pixel and its neighbor. */
        public void initializeGraphNeighborsWeights(int xStart, int xEnd, int yStart, int yEnd)
        {
            //Iterate through the graph, intializing the neighbors for each pixel
            //Weights of the edges between pixels are determined by the SSD between them
            int xBound = graph.GetLength(0);
            int yBound = graph.GetLength(1);

            Console.WriteLine("xBound:" + xBound);
            Console.WriteLine("yBound:" + yBound);

            for (int i = 0; i < xBound; i++ )
            {
                for (int j = 0; j < yBound; j++)
                {
                    Color currentDiff = graph[i,j].diff;
                    if (i - 1 >= 0)
                    {
                        //There is a neighbor to the left
                        Color neighborDiff = graph[i-1, j].diff;
                        int ssd = (int)( Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                        + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                        graph[i, j].addNeighbor(graph[i-1, j], ssd);
                    }

                    if (i + 1 < xBound)
                    {
                        //There is a neighbor to the right
                        Color neighborDiff = graph[i + 1, j].diff;
                        int ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                        + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                        graph[i, j].addNeighbor(graph[i + 1, j], ssd);
                    }

                    if (j - 1 >= 0)
                    {
                        //There is a neighbor to the top
                        Color neighborDiff = graph[i, j-1].diff;
                        int ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                        + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                        graph[i, j].addNeighbor(graph[i, j-1], ssd);
                    }

                    if (j + 1 < yBound)
                    {
                        //There is a neighbor to the bottom
                        Color neighborDiff = graph[i, j+1].diff;
                        int ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                        + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                        graph[i, j].addNeighbor(graph[i, j+1], ssd);
                    }

                    Console.WriteLine("NEIGHBORS COUNT:" + graph[i, j].neighbors.Count);
                }
            }

        }

        /* Helper function for findShortestPath, performs Dijkstra's algorithm */
        private void dijkstra(Node current, Node end)
        {
            Tuple<int, int> pos = new Tuple<int, int>(current.x, current.y);
            shortestPath.Enqueue(pos);
            Console.WriteLine(pos);

            if (current.x == end.x && current.y == end.y)
            {
                //Destination node is visited
                current.visited = true;
                return;
            }

            Tuple<Node, int> smallestDistNode = null;

            foreach (Tuple<Node, int> n in current.neighbors)
            {
                //Only do the stuff if the node is unvisited
                if (n.Item1.visited == false)
                {
                    //Calculate tentative distance for this node
                    int tentativeDistance = current.distance + n.Item2;

                    if (n.Item1.distance > tentativeDistance)
                    {
                        n.Item1.distance = tentativeDistance;

                        //Check if this is now the node with the smallest distance an update accordingly
                        if (smallestDistNode == null)
                        {
                            smallestDistNode = n;
                        }
                        else
                        {
                            if (smallestDistNode.Item1.distance > tentativeDistance)
                            {
                                smallestDistNode = n;
                            }
                        }
                    }
                }
            }

            //we're done calculating tentative distances for all neighbors of this node, so mark as visited
            current.visited = true;

            if(smallestDistNode == null){
                //we should never get here because we're not doing a complete traversal
                //but just in case
                return;
            }
            dijkstra(smallestDistNode.Item1, end);
        }

        public void findShortestPath(Node start, Node end)
        {
            start.distance = 0;
            start.visited = true;

            dijkstra(start,end);
        }

    }
}
