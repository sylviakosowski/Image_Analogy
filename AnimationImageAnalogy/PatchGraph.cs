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

        Node[,] graph;

        public Queue<Tuple<int,int>> shortestPath;

        public PatchGraph(int patchDimension, int patchIter)
        {
            this.patchDimension = patchDimension;
            this.patchIter = patchIter;

            graph = null;
            shortestPath = new Queue<Tuple<int, int>>();
        }

        /* TODO: CLEAN UP THIS FUNCTION BETTER */
        public void createGraph(Color[,] imageExisting, Color[,] imageNew,
            Tuple<int,int> patchExisting, Tuple<int,int> patchNew, int overlapDimension)
        {
            int xStart;
            int xEnd;

            int yStart;
            int yEnd;

            if(overlapDimension == 0)
            {
                xStart = patchNew.Item1;
                xEnd = patchExisting.Item1 + patchDimension;
                yStart = patchNew.Item2;
                yEnd = patchNew.Item2 + patchDimension;
            } 
            else
            {
                xStart = patchNew.Item1;
                xEnd = patchNew.Item1 + patchDimension;
                yStart = patchNew.Item2;
                yEnd = patchExisting.Item2 + patchDimension;
            }

            graph = new Node[(xEnd-xStart), (yEnd-yStart)];

            for(int i = xStart; i < xEnd; i++) 
            {
                for(int j = yStart; j < yEnd; j++)
                {
                    //Compute difference between each pixel in overlap
                    Color a = imageExisting[i,j];
                    Color b = imageNew[i,j];
                    
                    Color diff = Color.FromArgb(a.A - b.A, a.R - b.R, a.G - b.G, a.B - b.B);

                    /* Create a new node initialized with everything but the neighbors */
                    Node newNode = new Node(i,j,diff);
                    //graph.Add(new Tuple<int,int>(i,j), newNode);
                    graph[i,j] = newNode;
                }
            }
        }


        /* TODO: CLEAN UP THIS FUNCTION BETTER */
        public void initilazeGraphNeighborsWeights(int xStart, int xEnd, int yStart, int yEnd)
        {
            for (int i = xStart; i < xEnd; i++ )
            {
                for (int j = yStart; j < yEnd; j++)
                {
                   // Node n = graph.TryGetValue( new Tuple<int,int>(i,j) );
                    //Node currentNode = graph[i, j];
                    Color currentDiff = graph[i,j].diff;
                    if (i - 1 >= xStart)
                    {
                        //There is a neighbor to the left
                        Color neighborDiff = graph[i-1, j].diff;
                        int ssd = (int)( Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                        + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                        graph[i, j].addNeighbor(graph[i-1, j], ssd);
                    }

                    if (i + 1 < xEnd)
                    {
                        //There is a neighbor to the right
                        Color neighborDiff = graph[i + 1, j].diff;
                        int ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                        + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                        graph[i, j].addNeighbor(graph[i + 1, j], ssd);
                    }

                    if (j - 1 >= yStart)
                    {
                        //There is a neighbor to the top
                        Color neighborDiff = graph[i, j-1].diff;
                        int ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                        + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                        graph[i, j].addNeighbor(graph[i, j-1], ssd);
                    }

                    if (j + 1 < yEnd)
                    {
                        //There is a neighbor to the bottom
                        Color neighborDiff = graph[i, j+1].diff;
                        int ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                        + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                        graph[i, j].addNeighbor(graph[i, j+1], ssd);
                    }
                }
            }

        }

        private void dijkstra(Node current, Node end)
        {
            Tuple<int, int> pos = new Tuple<int, int>(current.x, current.y);
            shortestPath.Enqueue(pos);

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
