// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-05-13
// ******************************

using System.Windows;

namespace Assignment7_MiniGolf
{
    /// <summary>
    /// Class providing a collider. It has no graphic and only basic props:
    /// Size, position and a speed change for colliding objects
    /// </summary>
    class Collider
    {
        private Vector size;        // The size of the collider
        private Vector position;    // the position of the collider
        private Vector speedChange; // The vector speed change to use for the ball

        /// <summary>
        /// Constructor.
        /// If shape is a circle then radie is set to colliderWidth
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="colliderWidth"></param>
        /// <param name="colliderHeight"></param>
        public Collider(Vector colliderSize, Vector colliderPosition, Vector colliderSpeedChanger)
        {
            size = colliderSize;                // set size
            position = colliderPosition;        // set pos
            speedChange = colliderSpeedChanger; // set speed change
        }

        /// <summary>
        /// Get size
        /// </summary>
        public Vector Size
        {
            get { return size; }            
        }

        /// <summary>
        /// Get pos
        /// </summary>
        public Vector Position
        {
            get { return position; }           
        }

        /// <summary>
        /// Get speed change
        /// </summary>
        public Vector SpeedChange
        {
            get { return speedChange; }
        }
    }
}
