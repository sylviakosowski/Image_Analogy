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
        public int[] shortestPathArray;

        public PatchGraph(int patchDimension, int patchIter)
        {
            this.patchDimension = patchDimension;
            this.patchIter = patchIter;
            graph = null;
            shortestPath = new Queue<Tuple<int, int>>();
            shortestPathArray = new int[patchDimension];
        }

        /* Construct a graph out of pixels where the value at each node is the difference 
         * between the pixel components of the existing patch from B2 and the new patch from
         * A2. Edges are weighted with the sum of squared differences between adjacent pixels. 
         *
         * imageB2: The second complex image
         * imageA2: The first complex image
         * patchB: The top left corner of the patch in image B2
         * patchA: The top left corner of the patch in image A2
         * horizontal: Whether we're finding a horizontal shortest path or a vertical shortest path
         */
        public void createGraph(Color[,] imageB2, Color[,] imageA2,
            Tuple<int,int> patchB, Tuple<int,int> patchA, bool horizontal)
        {
            horizontal = !horizontal;
            /* Calculate how wide and tall the overlap is. */
            int xOverlap;
            int yOverlap;

            if(horizontal)
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

            //Iterate through the graph, initializing the nodes
            for(int i = 0; i < xOverlap; i++)
            {
                for(int j = 0; j < yOverlap; j++)
                {
                    //Obtain color of pixel in the patch
                    Color a = imageA2[axStart+i,ayStart+j];
                    Color b = imageB2[bxStart+patchIter+i, byStart+patchIter+j];

                    //Compute difference between RGB components of each pixel in the overlap
                    int aVal = a.A - b.A > 0 ? a.A - b.A : 0;
                    int rVal = a.R - b.R > 0 ? a.R - b.R : 0;
                    int gVal = a.G - b.G > 0 ? a.G - b.G : 0;
                    int bVal = a.B - b.B > 0 ? a.B - b.B : 0;
                    Color diff = Color.FromArgb(aVal, rVal, gVal, bVal);

                    //Create a new node initialied with everything but the neighbors
                    Node newNode = new Node(i, j, diff);
                    graph[i, j] = newNode;
                }
            }
        }

        private bool outOfBounds(int index, int min, int max)
        {
            return index < min || index >= max;
        }

        /* Initialize the graph with the neighbors of each pixel. The edge between each
         * pixel and its neighbor is weighted according to the sum of squared differences
         * between the pixel and its neighbor. */
        public void initializeGraphNeighborsWeights(int xStart, int xEnd, int yStart, int yEnd, bool horizontal)
        {
            //Iterate through the graph, intializing the neighbors for each pixel
            //Weights of the edges between pixels are determined by the SSD between them
            int xBound = graph.GetLength(0);
            int yBound = graph.GetLength(1);

            //Console.WriteLine("xBound:" + xBound);
            //Console.WriteLine("yBound:" + yBound);

            for (int i = 0; i < xBound; i++ )
            {
                for (int j = 0; j < yBound; j++)
                {
                    Color currentDiff = graph[i,j].diff;

                    /*
                    if (i - 1 >= 0)
                    {
                        if (graph[i - 1,j] == null)
                        {
                            break;
                        }
                        //There is a neighbor to the left
                        Color neighborDiff = graph[i-1, j].diff;
                        int ssd = (int)( Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                        + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                        graph[i, j].addNeighbor(graph[i-1, j], ssd);
                    }
                    */

                    if(horizontal)
                    {
                        if (i + 1 < xBound)
                        {
                            if (graph[i + 1, j] == null)
                            {
                                break;
                            }
                            //There is a neighbor directly to the right
                            Color neighborDiff = graph[i + 1, j].diff;
                            int ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                            + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                            graph[i, j].addNeighbor(graph[i + 1, j], ssd);

                            //There is a neighbor to the top-right diagonal
                            if (j - 1 >= 0)
                            {
                                neighborDiff = graph[i + 1, j - 1].diff;
                                ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                                + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                                graph[i, j].addNeighbor(graph[i + 1, j - 1], ssd);
                            }

                            //There is a neighbor to the bottom-right diagonal
                            if (j + 1 < yBound)
                            {
                                neighborDiff = graph[i + 1, j + 1].diff;
                                ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                                + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                                graph[i, j].addNeighbor(graph[i + 1, j + 1], ssd);
                            }
                        }
                    }
                    else
                    {
                        /*
                        if (j - 1 >= 0)
                        {
                            if (graph[i, j - 1] == null)
                            {
                                break;
                            }
                            //There is a neighbor to the top
                            Color neighborDiff = graph[i, j - 1].diff;
                            int ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                            + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                            graph[i, j].addNeighbor(graph[i, j - 1], ssd);
                        }*/

                        if (j + 1 < yBound)
                        {
                            //There is a neighbor to the bottom
                            //Console.WriteLine(i);
                            //Console.WriteLine(j + 1);
                            if (graph[i, j + 1] == null)
                            {
                                break;
                            }
                            Color neighborDiff = graph[i, j + 1].diff;
                            int ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                            + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                            graph[i, j].addNeighbor(graph[i, j + 1], ssd);

                            //Neighbor to the bottom left
                            if(i - 1 >= 0)
                            {
                                neighborDiff = graph[i - 1, j + 1].diff;
                                ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                                + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                                graph[i, j].addNeighbor(graph[i - 1, j + 1], ssd);
                            }

                            //Neighbor to the bottom right
                            if (i + 1 < xBound)
                            {
                                neighborDiff = graph[i + 1, j + 1].diff;
                                ssd = (int)(Math.Pow((currentDiff.A - neighborDiff.A), 2) + Math.Pow((currentDiff.R - neighborDiff.R), 2)
                                + Math.Pow((currentDiff.G - neighborDiff.G), 2) + Math.Pow((currentDiff.B - neighborDiff.B), 2));

                                graph[i, j].addNeighbor(graph[i + 1, j + 1], ssd);
                            }

                        }
                    }

                    

                    //Console.WriteLine("NEIGHBORS COUNT:" + graph[i, j].neighbors.Count);
                }
            }

        }

        /* Helper function for findShortestPath, performs Dijkstra's algorithm */
        private void dijkstra(Node current, Node end, bool horizontal)
        {
            Tuple<int, int> pos = new Tuple<int, int>(current.x, current.y);
            shortestPath.Enqueue(pos);
            if (horizontal)
            {
                shortestPathArray[current.x] = current.y;
            } else
            {
                shortestPathArray[current.y] = current.x;
            }

            if (current.x == end.x && current.y == end.y)
            {
                //Destination node is visited, so return
                Console.WriteLine("CORRECT END REACHED!");
                current.visited = true;
                return;
            }

            Tuple<Node, int> smallestDistNode = null;

            foreach (Tuple<Node, int> n in current.neighbors)
            {
                //Only do the stuff if the node is unvisited and on the next level of pixels (dont backtrack)
                int nextLevel;
                int currentLevel;
                if(horizontal)
                {
                    //Horizontal dijkstra
                    nextLevel = n.Item1.x;
                    currentLevel = current.x;
                } else
                {
                    //Vertical dijkstra
                    nextLevel = n.Item1.y;
                    currentLevel = current.y;
                }

                
                if (n.Item1.visited == false && nextLevel > currentLevel)
                {
                    //Calculate tentative distance for this node
                    int tentativeDistance = current.cost + n.Item2;

                    if (n.Item1.cost > tentativeDistance)
                    {
                        n.Item1.cost = tentativeDistance;

                        //Check if this is now the node with the smallest distance and update accordingly
                        if (smallestDistNode == null)
                        {
                            smallestDistNode = n;
                        }
                        else
                        {
                            //Console.WriteLine("smallestDistNode.Item1.distance: " + smallestDistNode.Item1.distance);
                            //Console.WriteLine("tentativeDistance: " + tentativeDistance);
                            if (smallestDistNode.Item1.cost > tentativeDistance)
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
                Console.WriteLine("We should never get here because we're not doing a complete traversal");
                return;
            }
            dijkstra(smallestDistNode.Item1, end, horizontal);
        }

        private void dijkstra2(Node current, Node end, List<Node> tempNodes, bool horizontal)
        {
            //Console.WriteLine(tempNodes.Count);
            if(tempNodes.Count == 0 || graph[current.x, current.y].cost == Int32.MaxValue) {
                //If there are no temporary nodes or if the cost of current is infinity, stop
                //Console.WriteLine("END OF DIJKSTRA REACHED");
                return;
            }
            else
            {
                //Console.WriteLine("DIJKSTRA ITERATION");
                //Find the temporary node with the smallest temporary cost
                Node smallestNode = null;
                foreach(Node temp in tempNodes)
                {
                    if (smallestNode == null)
                    {
                        smallestNode = graph[temp.x, temp.y];
                    } else
                    {
                        if(graph[temp.x, temp.y].cost < smallestNode.cost)
                        {
                            smallestNode = graph[temp.x, temp.y];
                        }
                    }
                }
                //Label the smallest node as permanent
                graph[smallestNode.x, smallestNode.y].permanent = true;
                //Remove the smallest node from the temp nodes
                tempNodes.Remove(smallestNode);
                //Label the smallest node as the current node
                current = graph[smallestNode.x, smallestNode.y];

                foreach(Tuple<Node, int> neighbor in current.neighbors)
                {
                    //For each temporary node which is a neighbor of the current node
                    //if cost(current) + edgeWeight < cost(neighbor) assign cost(neighbor) to the sum
                    Node neighborNode = neighbor.Item1;
                    if(!graph[neighborNode.x, neighborNode.y].permanent)
                    {
                        int neighborCost = graph[neighborNode.x, neighborNode.y].cost;
                        int edgeWeight = neighbor.Item2;
                        int sum = current.cost + edgeWeight;
                        if (sum < neighborCost)
                        {
                            //Set the cost and the parent for this node
                            graph[neighborNode.x, neighborNode.y].cost = sum;
                            graph[neighborNode.x, neighborNode.y].parent = graph[current.x, current.y];
                        }
                        if(!graph[neighborNode.x,neighborNode.y].visited)
                        {
                            tempNodes.Add(graph[neighborNode.x, neighborNode.y]);
                            graph[neighborNode.x, neighborNode.y].visited = true;
                        }
                    }
                }

                dijkstra2(current, end, tempNodes, horizontal);
            }
        }

        private void collectPath(Node start, Node end, bool horizontal)
        {

            //Console.WriteLine("START PATH");
            //Backtrack from the end node and find the shortest path to the start node
            Node current = end;
            while (current != null)
            {
                if (horizontal)
                {
                    shortestPathArray[current.x] = current.y;
                }
                else
                {
                    shortestPathArray[current.y] = current.x;
                }

                current = graph[current.x, current.y].parent;
            }
            shortestPathArray.Reverse();
        }

        public void findShortestPath(Node start, Node end, bool horizontal)
        {
            //Console.WriteLine("start: " + start.x + " " + start.y);
            //Console.WriteLine("end: " + end.x + " " + end.y);
            //Assign a cost of 0 to the starting node. All other nodes stay with cost of maxint (infinity)
            start.cost = 0;
            start.visited = true;
            graph[start.x, start.y].cost = 0;
            graph[start.x, start.y].visited = true;
            List<Node> tempNodes = new List<Node>();
            tempNodes.Add(start);

            //dijkstra(start, end, horizontal);
            dijkstra2(start, end, tempNodes, horizontal);
            //Console.WriteLine("end NOW: " + end.x + " " + end.y);
            collectPath(start, end, horizontal);

            //Console.WriteLine("Array first item:" + shortestPathArray[0]);
            //Console.WriteLine("Array last item:" + shortestPathArray[19]);


        }

    }
}
