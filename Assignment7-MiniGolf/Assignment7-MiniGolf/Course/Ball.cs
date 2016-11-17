// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-05-13
// ******************************

using System;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Assignment7_MiniGolf
{
    /// <summary>
    /// The ball class
    /// </summary>
    public class Ball
    {
        #region Props
        // Props       
        private Grid ballGrid;
        private double diameter;                            // Ball diameter
        private Vector position;                            // ball position
        private Vector speed;                               // ball speed
        private Image arrow;                                // the direction arrow
        private Ellipse ballCircle;                         // ball graphic
        
       
        #endregion

        #region Constructor & Creations
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="newDiam"></param>
        /// <param name="newPos"></param>
        public Ball(double newDiam, Vector newPos)
        {
            
            
            CreateArrow();                      // create the arrow
            CreateballGrid(Arrow.Height * 2.1); // Make the grid twice as large as the arrow + some margin
            Create(newDiam, newPos);            // create the ball
            AddToGrid();                        // Add ball and arrow to ball grid
        }

        /// <summary>
        /// Create a ball grid to hold both arrow and ball
        /// </summary>
        /// <param name="size"></param>
        private void CreateballGrid(double size)
        {
            ballGrid = new Grid();
            ballGrid.Width = size;
            ballGrid.Height = size;
            ballGrid.HorizontalAlignment = HorizontalAlignment.Left;
            ballGrid.VerticalAlignment = VerticalAlignment.Bottom;
            ballGrid.RenderTransformOrigin = new Point(0.5, 0.5);
            //ballGrid.Background = Brushes.Lime; // For Debug only
            
        }

        /// <summary>
        /// Method for adding bal and arrow to the ball grid
        /// </summary>
        public void AddToGrid()
        {
            ballGrid.Children.Add(BallCircle);  // add ball
            ballGrid.Children.Add(Arrow);       // add arrow
        }       

        /// <summary>
        /// Create the arrow
        /// </summary>
        private void CreateArrow()
        {
            arrow = new Image();                                                // New inmage
            BitmapImage bmIm = new BitmapImage();                               // new bitmap
            bmIm.BeginInit();                                                   // Begin init of bitmap
            bmIm.UriSource = new Uri("Pictures/arrow.png", UriKind.Relative);   // read source image
            bmIm.EndInit();                                                     // end init of bitmap
            arrow.Stretch = Stretch.Fill;                                       // Let the control stretch to image
            arrow.Source = bmIm;                                                // set the image source to the bitmap
            arrow.Width = 10.0;                                                 // width
            arrow.Height = 64.0;                                                // Height
            arrow.HorizontalAlignment = HorizontalAlignment.Center;             // Alignment
            arrow.VerticalAlignment = VerticalAlignment.Center;                 // Alignment
            arrow.RenderTransformOrigin = new Point(0.5, 1.0);                  // Set the correct origin so that arrow rotates correct
            arrow.Margin = new Thickness(0.0, 0.0, 0.0, arrow.Height);          // Set position to center of ball grid
        }

        /// <summary>
        /// Create the Ball
        /// </summary>
        /// <param name="newDiam"></param>
        /// <param name="newPos"></param>        
        private void Create(double newDiam, Vector newPos)
        {
            diameter = newDiam;                                             // set the class diameter
            ballCircle = new Ellipse();                                     // Create a new ellipse
            ballCircle.Width = diameter;                                    // Set the ellipse width
            ballCircle.Height = diameter;                                   // Set the ellipse height
            Position = newPos;                                              // Set the class Position
            ballCircle.Stroke = Brushes.Black;                              // Set line color
            ballCircle.Fill = Brushes.AntiqueWhite;                         // Set fill color
            ballCircle.HorizontalAlignment = HorizontalAlignment.Center;    // set aligment
            ballCircle.VerticalAlignment = VerticalAlignment.Center;        // Alignment
            ballCircle.RenderTransformOrigin = new Point(0.5, 0.5);         // set origin
            ballCircle.Margin = new Thickness(0.0);                         // Set margins to zero
            ballCircle.StrokeThickness = 2;                                 // Line thickness
        }
        #endregion

        #region Getters and Setters
        /// <summary>
        /// Ball shape
        /// </summary>
        public Ellipse BallCircle
        {
            get { return ballCircle; }
        }

        /// <summary>
        /// Arrow visibility
        /// </summary>
        public Visibility ArrowVisibility
        {
            set { arrow.Visibility = value; }
        }

        /// <summary>
        /// Ball position
        /// </summary>
        public Vector Position
        {
            set
            {
                position = value;
                ballGrid.Margin = new Thickness(position.X - ballGrid.Width / 2.0, 0.0, 0.0, position.Y - ballGrid.Height / 2.0);               
            }           
            get { return position; }
        }
        
        /// <summary>
        /// Ball diameter
        /// </summary>
        public double Diameter
        {
            get { return diameter; }
        }

        /// <summary>
        /// Ball speed
        /// </summary>
        public Vector Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        /// <summary>
        /// Get Arrow graphic
        /// </summary>
        public Image Arrow
        {
            get { return arrow; }
        }       

        /// <summary>
        /// Get ball grid
        /// </summary>
        public Grid Graphic
        {
            get { return ballGrid; }
        }

        #endregion

        #region Update
        /// <summary>
        /// Update ball movement
        /// </summary>
        public void UpdateMovement()
        {
            Position = new Vector(Position.X + Speed.X, Position.Y + Speed.Y);  // Update the position            
        }
      
        #endregion
        
    }
}