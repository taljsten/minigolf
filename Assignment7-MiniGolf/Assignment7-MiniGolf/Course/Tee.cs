// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-05-13
// ******************************

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Assignment7_MiniGolf
{
    /// <summary>
    /// Tee class for the ball to be "put at"
    /// </summary>
    public class Tee
    {
        private Vector size;        // Size of tee
        private Vector position;    // position of tee
        private Rectangle graphic;  // Graphic representation

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="teeSize"></param>
        /// <param name="teePos"></param>
        public Tee(Vector teeSize, Vector teePos)
        {           
            Create(teeSize, teePos); // Create the tee
        }

        /// <summary>
        /// Create the graphics
        /// </summary>
        /// <param name="teeSize"></param>
        /// <param name="teePos"></param>
        private void Create(Vector teeSize, Vector teePos)
        {        
                
            graphic = new Rectangle();                              // Create a rectangle
            graphic.Width = teeSize.X;                              // set width
            graphic.Height = teeSize.Y;                             // set height
            graphic.Stroke = Brushes.Black;                         // set line coler
            graphic.Fill = Brushes.LightGray;                       // and fill color
            graphic.HorizontalAlignment = HorizontalAlignment.Left; // and the origin
            graphic.VerticalAlignment = VerticalAlignment.Bottom;   // -- " --
            graphic.StrokeThickness = 1.0;                          // Line thickness
            Position = teePos;                                      // And update tha class position
        }

        /// <summary>
        /// Get and set the position variable of the class
        /// </summary>
        public Vector Position
        {
            get { return position; }
            set
            {
                position = value;   // set the class variable
                Graphic.Margin = new Thickness(Position.X, 0.0, 0.0, position.Y);   // Set the graphic represenation position
            }
        }

        /// <summary>
        ///  Set and get class size
        /// </summary>
        public Vector Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// This is the actual graphic representation
        /// </summary>
        public Rectangle Graphic
        {
            get { return graphic; }
            //set { graphic = value; }
        }


    }
}