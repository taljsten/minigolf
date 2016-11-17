// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-05-13
// ******************************

using System;
using System.Windows;


namespace Assignment7_MiniGolf
{
    /// <summary>
    /// The course class
    /// </summary>
    public class Course
    {
        // Props
        private Obstacle[] obstacles;                       // The obstacles
        private const int maxNumberOfObstacles = 6;         // Max number of obstacles on a course
        private const double maxWidthOfObstacle = 0.5f;     // in percentage of the width of the course
        private Tee tee;                                    // the tee
        private Hole hole;                                  // the hole
        private double friction;                            // This is pseudofriction with a number between 0-1. how much the ball will reduce speed per tick (second?) 0= full friction, 1=no friction
        private Vector courseSize;                          // the size of this course        
        private Random random = new Random();               // Random used to place obstacle
        

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="courseSize"></param>
        /// <param name="teeSize"></param>
        /// <param name="holeDiam"></param>
        /// <param name="holePos"></param>
        /// <param name="numOfObstacles"></param>
        /// <param name="courseFriction"></param>
        /// <param name="courseWallDampening"></param>
        public Course(Vector newCourseSize, Vector teeSize, double holeDiam, Vector holePos, int numOfObstacles, double courseFriction)
        {
            courseSize = newCourseSize;                                                         // set the coursesize
            tee = new Tee(teeSize, new Vector( (CourseSize.X - teeSize.X)/2.0 ,3.0));           // create the tee
            hole = new Hole(holeDiam, holePos);                                                 // create the hole
            if (numOfObstacles < 0) numOfObstacles = 0;                                         // check num of obstacles
            if (numOfObstacles > maxNumberOfObstacles) numOfObstacles = maxNumberOfObstacles;   // -- " --
            obstacles = new Obstacle[numOfObstacles];                                           // create the obstacles array
            GenerateObstacles();                                                                // create the obstacles
            friction = courseFriction;                                                          // set friction
        }
        
        /// <summary>
        /// Generate placement of obstacles
        /// </summary>
        private void GenerateObstacles()
        {
            double divider = CourseSize.Y / (obstacles.Length + 2.0);                       // Set the distance between obstacles
            for (int i=0; i<obstacles.Length; i++)                                          // loop through the obstacles array
            {                
                double randomSizeX = random.NextDouble() * 0.4 * CourseSize.X + 50.0;       // get a random size for the width (x)
                Vector obstacleSize = new Vector(randomSizeX, 20.0);                        // Set the size
                double randomX = random.NextDouble() * (CourseSize.X - obstacleSize.X);     // get a random position in x
                Vector obstaclePos = new Vector(randomX, i * divider + divider);            // set the position
                obstacles[i] = new Obstacle(obstacleSize, obstaclePos, CourseSize);         // Create a new obstacle
            }
        }
               

        /// <summary>
        /// Return obstacles        
        /// </summary>
        public Obstacle[] Obstacles
        {
            get { return obstacles; }
        }

        /// <summary>
        /// Get and set course size
        /// </summary>
        public Vector CourseSize
        {
            private set { courseSize = value; }
            get { return courseSize; }
        }

        /// <summary>
        ///  Get the friction
        /// </summary>
        public double Friction
        {
            get { return friction; }
        }

        /// <summary>
        /// Get the hole
        /// </summary>
        public Hole Hole
        {
            get { return hole; }
        }

        /// <summary>
        /// Get the tee
        /// </summary>
        public Tee Tee
        {
            get { return tee; }
        }


    }
}