// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-04-22
// ******************************

using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace Assignment7_MiniGolf
{
    /// <summary>
    /// Class for creating obstacles
    /// </summary>
    public class Obstacle
    {
        // Props
        private Rectangle graphic;                              // the actual graphic
        private Vector size;                                    // Size of the obstacle
        private Vector position;                                // Pos of obs
        private Vector courseSize;                              // Sixe of course for ref               
        private Vector maxSize = new Vector(100.0, 30.0);       // the max size of an obst
               

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="obstacleSize"></param>
        /// <param name="obstaclePosition"></param>
        /// <param name="gameCourseSize"></param>
        public Obstacle(Vector obstacleSize, Vector obstaclePosition, Vector gameCourseSize)
        {
            size = obstacleSize;            // set size
            position = obstaclePosition;    // set pos
            courseSize = gameCourseSize;    // set course size for ref
            ValidatePos();                  // Validate position
            ValidateSize();                 // Validate the size
            CreateObstacle();               // Create
        }

        /// <summary>
        /// Validation of position
        /// </summary>
        private void ValidatePos()
        {
            // We do not want the obstacle outside the course
            if (Position.X < 0.0) Position = new Vector(0.0, Position.Y);
            if (Position.X > (courseSize.X - Size.X)) Position = new Vector((courseSize.X - Size.X), Position.Y);
            if (Position.Y < 0.0) Position = new Vector(Position.X, 0.0);
            if (Position.Y > (courseSize.Y - Size.Y)) Position = new Vector(Position.X, (courseSize.Y - Size.Y));
        }

        /// <summary>
        /// Validation of size
        /// </summary>
        private void ValidateSize()
        {
            // Just checking so that nothing is below 0 and larger than maxsize
            if (Size.X < 0.0) Size = new Vector(0.0, Size.Y);
            if (Size.X > maxSize.X) Size = new Vector(maxSize.X, Size.Y);
            if (Size.Y < 0.0) Size = new Vector(Size.X, 0.0);
            if (Size.Y > maxSize.Y) Size = new Vector(Size.X, maxSize.Y);
        }


        /// <summary>
        /// Create the actual shape
        /// </summary>
        private void CreateObstacle()
        {
            //if (placement == Placement.None) placement = Placement.Left;
            graphic = new Rectangle(); // Create the graphic

            // Figure out the placement
            graphic.RenderTransformOrigin = new Point(0.0, 0.0);
            graphic.HorizontalAlignment = HorizontalAlignment.Left;           

            // Set sizes, brushes and more
            graphic.VerticalAlignment = VerticalAlignment.Bottom;
            graphic.Margin = new Thickness(Position.X, 0, 0, Position.Y);
            graphic.Height = Size.Y;
            graphic.Width = Size.X;
            graphic.Stroke = Brushes.Black;
            graphic.Fill = Brushes.Gold;
            graphic.StrokeThickness = 1.5;            
        }

        /// <summary>
        /// Get the graphic
        /// </summary>
        public Rectangle Graphic
        {
            get { return graphic; }
        }

        /// <summary>
        /// Get and set size
        /// </summary>
        public Vector Size
        {
            get { return size; }
            set { size = value; }
        }

        /// <summary>
        /// get and set pos
        /// </summary>
        public Vector Position
        {
            get { return position; }
            set { position = value; }
        }


    }
}