// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-05-13
// ******************************

using System.Windows;

namespace Assignment7_MiniGolf
{
    /// <summary>
    /// Static class used for different calculations
    /// </summary>
    class Calculator
    {       

        /// <summary>
        /// Check if ball is colliding with obstacles
        /// and return a bounce vector
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="obstacle"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        public static Vector IsColliding(Ball ball, Obstacle obstacle, Course course)
        {
            
            Vector returnVector = new Vector(1.0,1.0);                      // If vector is 1,1 than no collision

            Collider[] colliders = CalculateRectColliders(obstacle, ball);  // Calculate the colliders
            foreach (Collider collider in colliders)                        // run trrough the collider array
            {
                if (IsInCollider(collider, ball))                           // check if ball is colliding
                {
                    returnVector = new Vector(collider.SpeedChange.X, collider.SpeedChange.Y);  // make the return vector
                    break;
                }
            }        
           
            return returnVector;    // return the vector to the caller
        }

        /// <summary>
        /// Check if ball is colliding with hole
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="hole"></param>
        /// <returns></returns>
        public static bool IsColliding (Ball ball, Hole hole)
        {
            bool isColliding = false;                                   // Set is Colliding to false and check below for collision
            double distance = (ball.Position - hole.Position).Length;   // calculate distance between hole and ball
            if (distance < (ball.Diameter / 2.0)) isColliding = true;     // if distance is less than hole radius then ball went in
            return isColliding;                                         // Return state to caller
        }      

        /// <summary>
        /// Calculate a collider that is bigger than the actual graphics
        /// </summary>
        /// <param name="obstacle"></param>
        /// <param name="ball"></param>
        /// <returns></returns>
        private static Collider[] CalculateRectColliders(Obstacle obstacle, Ball ball)
        {
            Collider[] returnColliders = new Collider[8]; // One collider for each corner and one for each side
            Vector size;
            Vector pos;
            Vector speedCh;
            double radie = ball.Diameter / 2.0;
            // 012
            // 7-3
            // 654
            // *****
            // 0 - Left Top Corner
            size = new Vector(radie * 0.7, radie * 0.7);
            pos = new Vector(obstacle.Position.X - radie * 0.7, obstacle.Position.Y + obstacle.Size.Y);
            speedCh = new Vector(-1.0, -1.0);
            returnColliders[0] = new Collider(size, pos, speedCh);

            // 1 - Top
            size = new Vector(obstacle.Size.X, radie);
            pos = new Vector(obstacle.Position.X, obstacle.Position.Y + obstacle.Size.Y);
            speedCh = new Vector(1.0, -1.0);
            returnColliders[1] = new Collider(size, pos, speedCh);

            // 2 - Right Top Corner
            size = new Vector(radie * 0.7, radie * 0.7);
            pos = new Vector(obstacle.Position.X + obstacle.Size.X, obstacle.Position.Y + obstacle.Size.Y);
            speedCh = new Vector(-1.0, -1.0);
            returnColliders[2] = new Collider(size, pos, speedCh);

            // 3 - Right
            size = new Vector(radie, obstacle.Size.Y);
            pos = new Vector(obstacle.Position.X + obstacle.Size.X, obstacle.Position.Y);
            speedCh = new Vector(-1.0, 1.0);
            returnColliders[3] = new Collider(size, pos, speedCh);

            // 4 - Right Bottom Corner
            size = new Vector(radie * 0.7, radie * 0.7);
            pos = new Vector(obstacle.Position.X + obstacle.Size.X, obstacle.Position.Y - radie * 0.7);
            speedCh = new Vector(-1.0, -1.0);
            returnColliders[4] = new Collider(size, pos, speedCh);

            // 5 - Bottom 
            size = new Vector(obstacle.Size.X, radie);
            pos = new Vector(obstacle.Position.X, obstacle.Position.Y - radie);
            speedCh = new Vector(1.0, -1.0);
            returnColliders[5] = new Collider(size, pos, speedCh);

            // 6 - Left Bottom Corner
            size = new Vector(radie * 0.7, radie * 0.7);
            pos = new Vector(obstacle.Position.X - radie * 0.7, obstacle.Position.Y - radie * 0.7);
            speedCh = new Vector(-1.0, -1.0);
            returnColliders[6] = new Collider(size, pos, speedCh);

            // 7 - Left 
            size = new Vector(radie, obstacle.Size.Y);
            pos = new Vector(obstacle.Position.X - radie, obstacle.Position.Y);
            speedCh = new Vector(-1.0, -1.0);
            returnColliders[7] = new Collider(size, pos, speedCh);

            return returnColliders;

        }

        /// <summary>
        /// A method for chacking collision between ball and collider
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="ball"></param>
        /// <returns></returns>
        private static bool IsInCollider(Collider collider, Ball ball)
        {
            bool isInCollider = false;  // Set to false and check if true   
            if ((ball.Position.X >= collider.Position.X) && (ball.Position.X <= (collider.Position.X + collider.Size.X)) &&            
               ((ball.Position.Y >= collider.Position.Y) && (ball.Position.Y <= (collider.Position.Y + collider.Size.Y) )) )    // Check if ball is in collider.
            {
                isInCollider = true;    // set to true
            }
            

            return isInCollider;    // return to caller

        }

        /// <summary>
        /// Calculate player score
        /// </summary>
        /// <param name="player"></param>
        /// <param name="gameManager"></param>
        /// <returns></returns>
        public static double CalculateScore(Player player, GameManager gameManager)
        {
            if (player.Strokes > 0)    // If player made some strokes
            {
                return (gameManager.Difficulty + 1.0) / (player.Strokes * 0.5)* 100.0 + gameManager.Bounces * 7.0; // Do a calculation with difficulty, strokes, and bounces as parameters
            }
            else return 0.0;            // return zero if no strokes made
        }

        /// <summary>
        /// Check if the ball is above the hole on the course
        /// </summary>
        /// <param name="ball"></param>
        /// <param name="hole"></param>
        /// <returns></returns>
        public static bool IsBallAboveHole(Ball ball, Hole hole)
        {
            bool isAbove = false;                                   // set to false and check if true
            if (ball.Position.Y > hole.Position.Y) isAbove = true;  // set to true if ball is above the hole
            return isAbove;                                         // return to caller
        }



    }
}
