// ------------------------------------------------------------------------------------------------------------------------
// File name: Node.cs
// Project name: Project01NelsonDavid
// Project description: This project calculates the fastest roundtrip route between 'N' input coordinates, always starting
//                      from the origin point (0,0).
// ------------------------------------------------------------------------------------------------------------------------
// Creation Date: 09/13/2020
// ------------------------------------------------------------------------------------------------------------------------

namespace Project01NelsonDavid
{
    /// <summary>
    /// Class Name: Node
    /// Class Purpose: This class represents a node with X & Y coordinates provided
    /// from an input file.
    /// </summary>
    /// <remarks>
    /// Creation Date: 09/13/2020
    /// </remarks>
    public class Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="id">The node name.</param>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Node(int id, int x, int y)
        {
            this.Id = id;
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{this.Id}";
        }

        /// <summary>
        /// Gets or sets the x-coordinate.
        /// </summary>
        /// <value>
        /// The x value.
        /// </value>
        public int X { get; }

        /// <summary>
        /// Gets or sets the y-coordinate.
        /// </summary>
        /// <value>
        /// The y value.
        /// </value>
        public int Y { get; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; }
    }
}
