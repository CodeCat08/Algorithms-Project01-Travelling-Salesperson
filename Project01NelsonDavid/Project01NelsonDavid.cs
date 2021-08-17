// ------------------------------------------------------------------------------------------------------------------------
// File name: Project01NelsonDavid.cs
// Project name: Project01NelsonDavid
// Project description: This project calculates the fastest roundtrip route between 'N' input coordinates, always starting
//                      from the origin point (0,0).
// ------------------------------------------------------------------------------------------------------------------------
// Creation Date: 09/13/2020
// ------------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;

namespace Project01NelsonDavid
{
    /// <summary>
    ///     Class Name: Project01NelsonDavid
    ///     Class Purpose: This class calculates the shortest route to travel every
    ///     node from a list of coordinates.
    /// </summary>
    /// <remarks>
    ///     Creation Date: 09/13/2020
    /// </remarks>
    public static class Project01NelsonDavid
    {
        private static double shortestDist = double.MaxValue; //the shortest distance found
        private static Node[] bestRoute; //the best route found
        private static Node origin = new Node(0,0,0); //origin node
        private static double[,] nodeDistances; //city to city distance table

        /// <summary>
        ///     Method Name: Main
        ///     Method Purpose: Defines the entry point of the application.
        /// </summary>
        /// <remarks>
        ///     Date Created: 09/13/2020
        /// </remarks>
        public static void Main()
        {
            Node[] nodes = loadNodesFromConsole(); //load points as nodes from input

            var sw = Stopwatch.StartNew(); //start a stopwatch to time algorithm
            calculateRoutes(ref nodes); //calculate shortest route from nodes
            sw.Stop(); //stop the stopwatch once algorithm is finished

            Console.WriteLine($"Shortest Route: {shortestDist:F}"); //print shortest distance to console
            Console.Write("Optimal Route: 0 "); //print the shortest route toured
            foreach (Node node in bestRoute)
            {
                Console.Write($"{node} ");
            } //end foreach(Node node in bestRoute)

            Console.WriteLine();
            Console.WriteLine($"Time elapsed: {sw.Elapsed.TotalSeconds:F} sec"); //print the amount of time the algorithm took

            Console.Read(); //wait for user to press a key to end program
        } //end Main

        /// <summary>
        ///     Method Name: calculateRoutes
        ///     Method Purpose: Calculates the distance of each route permutation until finding
        ///     the most optimal route. The most optimal route is saved as the 'bestRoute'.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <remarks>
        ///     Date Created: 09/13/2020
        /// </remarks>
        private static void calculateRoutes(ref Node[] nodes)
        {
            //while a next permutation of nodes exists
            while (nextPermutation(ref nodes))
            {
                double currentDist = roundtripDistance(ref nodes); //set current distance to total distance of initial route
                if (currentDist <= shortestDist) //if new best route found
                {
                    shortestDist = currentDist; //update shortest distance
                    
                    //update best route
                    for (int i = nodes.Length - 1; i >= 0; i--)
                    {
                        bestRoute[i] = nodes[i];
                    }//end for(int i = nodes.Length - 1; i >= 0; i--)
                } //end if(currentDist < shortestDist)

            } //end while(nextPermutation(nodes))
        } //end calculateRoutes

        /// <summary>
        ///     Method Name: nextPermutation
        ///     Method Purpose: Calculates the next permutation of a specified array of Node objects.
        /// </summary>
        /// <param name="nodes">The nodes.</param>
        /// <remarks>
        ///     Date Created: 09/13/2020
        /// </remarks>
        private static bool nextPermutation(ref Node[] nodes)
        {
            /*
             * 01. Scan from right and find first item out of increasing order (7 < 11)
             *  {2 6 3 8 7* 11* 10 9 4 1}
             *
             * 02. Scan from right again until finding first item greater than pivot (7 < 9)
             *  {2 6 3 8 7* 11 10 9* 4 1}
             *
             * 03. Swap the two items (pivot and successor)
             *  {2 6 3 8 9* 11 10 7* 4 1}
             *
             * 04. Reverse items after pivot index
             *  {2 6 3 8 9* 1 4 7 10 11}
             */

            //scan array from right to left for pivot point
            int pivot = nodes.Length - 1;
            while (pivot > 0 && (nodes[pivot].Id < nodes[pivot - 1].Id))
            {
                pivot--;
            }

            //if no pivot value was found, no permutations remain
            if (pivot <= 0)
            {
                return false;
            }

            //scan from right to left again for value that is greater than item before pivot value
            int swapIndex = nodes.Length - 1;
            while (nodes[swapIndex].Id < nodes[pivot - 1].Id)
            {
                swapIndex--;
            }

            //swap item before pivot and item greater than it
            swap(ref nodes,pivot - 1,swapIndex);

            //reverse everything from pivot point to the array "tail"
            int tail = nodes.Length - 1;
            while (pivot < tail)
            {
                swap(ref nodes,pivot,tail);
                pivot++;
                tail--;
            }

            return true;

        } //end nextPermutation

        /// <summary>
        ///     Swaps elements in a specified array at the specified indices.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <param name="valOne">An element index.</param>
        /// <param name="valTwo">A second element index.</param>
        private static void swap(ref Node[] array, int valOne, int valTwo)
        {
            Node temp = array[valOne];
            array[valOne] = array[valTwo];
            array[valTwo] = temp;
        } //end swap

        /// <summary>
        ///     Method Name: roundtripDistance
        ///     Method Purpose: Calculates the total distance of a trip/route.
        /// </summary>
        /// <param name="array">The array holding a trip route.</param>
        /// <returns>The total distance from the specified route.</returns>
        /// <remarks>
        ///     Date Created: 09/13/2020
        /// </remarks>
        private static double roundtripDistance(ref Node[] array)
        {

            double totalDistance = calculateDistance(ref array[array.Length - 1], ref origin); //calculate distance from last node to origin
            totalDistance += calculateDistance(ref origin,ref array[0]); //calculate distance from origin to first node

            //use distance matrix to look up distance between each node
            for (var i = 1; i < array.Length; i++)
            {
                totalDistance += nodeDistances[array[i-1].Id - 1,array[i].Id - 1];
                if (totalDistance >= shortestDist)
                {
                    return totalDistance;
                }
            }

            return totalDistance;
        } //end roundtripDistance

        /// <summary>
        ///     Method Name: calculateDistance
        ///     Method Purpose: Calculates the distance between two specified nodes.
        /// </summary>
        /// <param name="nodeOne">A node to get the distance from.</param>
        /// <param name="nodeTwo">A node to get the distance tp.</param>
        /// <returns>The distance between the specified nodes.</returns>
        /// <remarks>
        ///     Date Created: 09/13/2020
        /// </remarks>
        private static double calculateDistance(ref Node nodeOne,ref Node nodeTwo)
        {
            //calculate the distance between two Node's coordinates
            int x = Math.Abs(nodeOne.X - nodeTwo.X);
            int y = Math.Abs(nodeOne.Y - nodeTwo.Y);
            double tot = (x * x) + (y * y);
            return Math.Sqrt(tot);
        } //end calculateDistance

        /// <summary>
        ///     Method Name: loadNodesFromConsole
        ///     Method Purpose: Reads input containing a specified capacity and list of points and stores them in each Node
        ///     in an array of node objects.
        /// </summary>
        /// <returns>An array of Node objects of a specified size and with specified coordinates.</returns>
        /// <exception c="ArgumentNullException">
        ///     No capacity in input.
        ///     or
        ///     No coordinates in input.
        /// </exception>
        /// <exception c="Exception">
        ///     Please validate that input value is an integer for number of desired points.
        ///     or
        ///     No coordinates in input.
        /// </exception>
        /// <remarks>
        ///     Date Created: 09/13/2020
        /// </remarks>
        private static Node[] loadNodesFromConsole()
        {
            bool isValidInteger = int.TryParse(Console.ReadLine(), out int capacity); //parse 0th line as an integer.
            if (!isValidInteger) //if capacity line is invalid
            {
                throw new Exception("Please validate that input value is an integer for number of desired points.");
            } //end if(!isValidInteger)

            var nodes = new Node[capacity]; //initialize an array of Nodes with a capacity specified in input
            bestRoute = new Node[capacity]; //initialize the bestRoute Node array
            
            //input points into each node in the node array
            for (var i = 0; i < capacity; i++)
            {
                //read each point string line by line, validate that it's not null
                string pointString = Console.ReadLine() ?? throw new Exception("No coordinates in input.");
                string[] values = pointString.Split(' '); //split each line into X and Y components
                int.TryParse(values[0], out int tempX); //parse the X component as an integer
                int.TryParse(values[1], out int tempY); //parse the Y component as an integer
                nodes[i] = new Node(i + 1, tempX, tempY); //add a new node to the node array from the provided X & Y coordinates
            } //end for(var i = 0; i < capacity + 1; i++)

            //initialize a distance matrix for lookup
            nodeDistances = new double[capacity,capacity];
            for (var i = 0; i < capacity; i++)
            {
                for (var j = 0; j < capacity; j++)
                {
                    nodeDistances[i, j] = calculateDistance(ref nodes[i], ref nodes[j]);
                }
            }

            //deep copy nodes into bestRoute
            for (var i = 0; i < nodes.Length; i++)
            {
                bestRoute[i] = nodes[i];
            }
            shortestDist = roundtripDistance(ref nodes); //initialize shortest distance as current roundtrip distance

            return nodes; //return an array of nodes populated with points from input
        } //end loadNodesFromConsole
    } //end class
} //end namespace
