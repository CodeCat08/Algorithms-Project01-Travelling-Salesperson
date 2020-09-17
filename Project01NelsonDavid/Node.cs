// ------------------------------------------------------------------------------------------------------------------------
// File name: Node.cs
// Project name: Project01NelsonDavid
// Project description: 
// ------------------------------------------------------------------------------------------------------------------------
// Creator's name and email: David Nelson (nelsondk@etsu.edu)
// Course Name: CSCI-3230 Algorithms
// Course Section: 901
// Creation Date: 09/13/2020
// ------------------------------------------------------------------------------------------------------------------------

using System;

namespace Project01NelsonDavid
{
    /// <summary>
    /// Class Name: Node
    /// Class Purpose: This class represents a node with X & Y coordinates provided
    /// from an input file.
    /// </summary>
    /// <author>
    /// David Nelson
    /// </author>
    /// <remarks>
    /// Creation Date: 09/13/2020
    /// </remarks>
    public class Node
    {
        /// <summary>
        /// The node identifier
        /// </summary>
        private int _id;

        /// <summary>
        /// The node x-coordinate
        /// </summary>
        private int _x;

        /// <summary>
        /// The node y-coordinate
        /// </summary>
        private int _y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Node(int id, int x, int y)
        {
            Id = id;
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"{_id}";
        }

        /// <summary>
        /// Gets or sets the x-coordinate.
        /// </summary>
        /// <value>
        /// The x value.
        /// </value>
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }

        /// <summary>
        /// Gets or sets the y-coordinate.
        /// </summary>
        /// <value>
        /// The y value.
        /// </value>
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
    }
}