// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-05-13
// ******************************

using System.Windows;
using System.Windows.Shapes;

namespace Assignment7_MiniGolf
{
    public class Hole
    {
        // Props
        private double diameter;
        private Vector position;
        private Ellipse graphic;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="holeDiam"></param>
        /// <param name="holePos"></param>
        public Hole(double holeDiam, Vector holePos)
        {
            Create(holeDiam, holePos); // Create the hole
        }

        /// <summary>
        /// Creating hole
        /// </summary>
        /// <param name="holeDiam"></param>
        /// <param name="holePos"></param>
        private void Create(double holeDiam, Vector holePos)
        {
            diameter = holeDiam;        // set class diameter            

            graphic = new Ellipse();                                // new ellipse
            graphic.Width = diameter;                               // Set width
            graphic.Height = diameter;                              // set height    
            graphic.Stroke = System.Windows.Media.Brushes.Black;    // Set all black
            graphic.Fill = System.Windows.Media.Brushes.Black;      // Set all black (it's a hole!)
            graphic.HorizontalAlignment = HorizontalAlignment.Left; // Aligment 
            graphic.VerticalAlignment = VerticalAlignment.Bottom;   // Aligment
            graphic.StrokeThickness = 2;                            // Line thickness, has no meaning unless different color than fill, but anyway.

            Position = holePos;         // Set class position
        }

        /// <summary>
        /// Set and get position
        /// </summary>
        public Vector Position
        {
            get { return position; }
            set
            {
                // set both class pos and graphic pos
                position = value;
                graphic.Margin = new Thickness(position.X - Graphic.Width / 2.0, 0.0, 0.0, position.Y - Graphic.Height / 2.0);
            }
        }

        /// <summary>
        /// Set and get diameter
        /// </summary>
        public double Diameter
        {
            get { return diameter; }
            set { diameter = value; }
        }

        /// <summary>
        /// Get the graphic
        /// </summary>
        public Ellipse Graphic
        {
            get { return graphic; }
        }

    }
}