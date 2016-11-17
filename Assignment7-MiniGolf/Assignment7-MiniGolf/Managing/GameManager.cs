// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-05-13
// ******************************


using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;



namespace Assignment7_MiniGolf
{
    /// <summary>
    /// The brain for this game
    /// </summary>
    public class GameManager
    {

        #region Props
        private Course course;                                      // Course that holds all except ball and player
        private Ball ball;                                          // Here's the ball
        private Player player;                                      // and the player
        private Grid gameGrid;                                      // the game grid. All grpahic is going to be children of this
        private double bestScore;                                   // Best score       
        private string bestPlayer;                                  // Player with best score    
        private double lastScore;                                   // players last score
        private int lastStrokes;                                    // players last amount of strokes
        private int bounces;                                        // number of ball bounces         
        private int postBouncesCoolDownInFrames = 5;                // The amount of frames for the ball to "cool down" after a bounce so that it can bounce again
        private int frameCount;                                     // Keep track of frames elapsed after an event
        private int difficulty;                                     // How hard life is
        private GameState gameState = GameState.NotStarted;         // the state of the game. Set to not started
        private GameState prevState = GameState.None;               // the previous state. Used to handle robust user input
        private const int ticksPerSecond = 120;                     // Max ticks per second is 1000, but thats really high. Between 60 and 120 is good
        private PlayerInput playerInputButton = PlayerInput.None;   // The player input action
        private Vector teeSize = new Vector(100, 50);               // Set the Tee size
        private bool shouldDirectionArrowBeVisible = false;         // a bool used for visibility of the arrow
        private double ballDirection = 0.0;                         // the ball direction
        private double arrowRotSpeed = 45;                          // rotation speed of the arrow
        private double ballForce = 0.0;                             // ball force
        private DispatcherTimer forceTimer;                         // Timer used when inputting force
        private double maxSpeedForBallInHole = 3.0;                 // Max speed for the ball to go in hole 
        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="startPlayer"></param>
        /// <param name="gameDifficulty"></param>
        public GameManager(Grid grid, Player startPlayer, int gameDifficulty)
        {
            gameGrid = grid;                // a ref to the game grid
            player = startPlayer;           // the player
            difficulty = gameDifficulty;    // game difficulty
            bestScore = 0.0;                // Reset bestScore
            lastScore = 0.0;                // reset last score
            bestPlayer = String.Empty;      // reset best player
            bounces = 0;                    // reset bounces

            FirstStartup();                 // Do first startup

        } 
        #endregion

        #region Setters and Getters
        /// <summary>
        /// Set and get Ball
        /// </summary>
        public Ball Ball
        {
            get { return ball; }
        }

        /// <summary>
        /// Set and get course
        /// </summary>
        public Course Course
        {
            get { return course; }
        }

        /// <summary>
        /// Set and get gamestate
        /// </summary>
        public GameState GameState
        {
            get { return gameState; }
            set { gameState = value; } // TODO: can only be set from this class???
        }

        /// <summary>
        /// Set and get ball direction
        /// </summary>
        public double BallDirection
        {
            get { return ballDirection; }
            set { ballDirection = value; }
        }

        /// <summary>
        /// Set and get ball force
        /// </summary>
        public double BallForce
        {
            get { return ballForce; }
        }

        /// <summary>
        /// Get arrow rotation
        /// </summary>
        public double ArrowRotSpeed
        {
            get { return arrowRotSpeed; }
        }

        /// <summary>
        /// Set and get difficulty
        /// </summary>
        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }

        /// <summary>
        /// Set and get best score
        /// </summary>
        public double BestScore
        {
            get { return bestScore; }

        }

        /// <summary>
        /// Set and get best player
        /// </summary>
        public string BestPlayer
        {
            get { return bestPlayer; }
        }

        /// <summary>
        /// Set and get last score
        /// </summary>
        public double LastScore
        {
            get { return lastScore; }
        }

        /// <summary>
        /// Set and get bounces
        /// </summary>
        public int Bounces
        {
            get { return bounces; }
        }

        /// <summary>
        /// Set and get last strokes
        /// </summary>
        public int LastStrokes
        {
            get { return lastStrokes; }
            set { lastStrokes = value; }
        }

        /// <summary>
        /// Return a copy of the player
        /// </summary>
        public Player Player
        {
            get
            {
                Player playerCopy = new Player();
                playerCopy.Name = player.Name;
                playerCopy.BestScore = player.BestScore;
                //playerCopy.Score = player.Score;
                playerCopy.Strokes = player.Strokes;
                return playerCopy;
            }

            set
            {
                player = value;
            }
        }

        /// <summary>
        /// Set the speed and direction for the ball
        /// </summary>
        /// <param name="ballSpeed"></param>
        /// <param name="ballDirection"></param>
        public void SetBallSpeed(double ballSpeed, Vector ballDirection)
        {
            Ball.Speed = new Vector(ballDirection.X, ballDirection.Y) * ballSpeed; // Set it
        }

        /// <summary>
        /// Return game ticks per second (FPS)
        /// </summary>
        public int TicksPerSecond
        {
            get { return ticksPerSecond; }
        }


        /// <summary>
        /// Return arrow visibility
        /// </summary>
        public bool ShouldDirectionArrowBeVisible
        {
            get { return shouldDirectionArrowBeVisible; }
        }



        /// <summary>
        /// Return player score
        /// </summary>
        public double PlayerScore
        {
            get
            {
                return Calculator.CalculateScore(player, this);
            }
        }

        #endregion

        #region Start and Reset
        /// <summary>
        /// When everything starts in the GameTimer 
        /// </summary>
        private void FirstStartup()
        {
            // Create a new course
            course = new Course(new Vector(gameGrid.Width, gameGrid.Height), teeSize, 20.0f, new Vector(gameGrid.Width / 2.2f, gameGrid.Height - 70.0f), Difficulty, 0.995);
            for (int i = 0; i < course.Obstacles.Length; i++)        // loop trough obstacles and...
            {
                gameGrid.Children.Add(course.Obstacles[i].Graphic); // ...add them to the game grid
            }
            gameGrid.Children.Add(course.Hole.Graphic);             // add the hole to the game grid
            gameGrid.Children.Add(course.Tee.Graphic);              // add the tee to the game grid
            ball = new Ball(15, new Vector(100, 20));               // create the ball and...
            gameGrid.Children.Add(ball.Graphic);                    // ...add to the grid           
            ResetGame();                                            // Set some intitials
        }

        /// <summary>
        /// Reset the game with new difficulty
        /// </summary>
        /// <param name="gameDifficulty"></param>
        public void Reset(int gameDifficulty)
        {
            gameGrid.Children.Clear();          // Clear the game grid from objects
            Difficulty = gameDifficulty;        // Set Difficulty
            FirstStartup();                     // Start

        }
        #endregion

        #region Game Update
        /// <summary>
        /// Update is called every game tick from Main window
        /// </summary>
        public void Update()
        {
            CheckGameState();       // let the game tick only check for game state
        } 
        #endregion

        #region State Machine
        /// <summary>
        /// Checking game State
        /// </summary>
        private void CheckGameState()
        {
            frameCount++;
            if (frameCount > TicksPerSecond) frameCount = 0;
            switch (GameState)
            {
                case GameState.Started:                     // On tee
                    GameInStartedState();                   // Do stuff
                    break;
                case GameState.WaitingForButtonRelease:     // Waiting for a button release, so that we do'nt get multiple inputs following ticks
                    GameWaitingForButtonRelease();
                    break;
                case GameState.WaitingForDirection:         // Waiting for a ball direction
                    GameWaitingForDirection();
                    break;
                case GameState.WaitingForForce:             // Waiting for a ball force
                    GameWaitingForForce();
                    break;
                case GameState.BallRolling:                 // Ball is moving                    
                    UpdateBallPosition();                   // Update the ball position and...
                    CollisionDetections();                  // ...check for collisions with obstacles, walls and hole
                    break;                
                case GameState.BallStopped:                 // Ball is not moving = stopped
                    GameInBallStoppedState();
                    break;
                case GameState.BallInHole:                  // Ball in hole
                    GameInBallInHoleState();
                    break;
                case GameState.PlayerChanged:               // Changed player. Restart!!
                    GameInChangedPlayerState();
                    break;
            }
        }

        /// <summary>
        /// Actions in game state "Started"
        /// </summary>
        private void GameInStartedState()
        {
            frameCount = 0;                                             // Set frame counter to 0;
            shouldDirectionArrowBeVisible = false;                      // Only show arrow when player wants to
            ballForce = 0.0;                                            // No force needed yet

            if (playerInputButton == PlayerInput.LeftActionButton)      // If player clicked LeftAction = Mouse left
            {
                ballForce = 0.0;                                        // Ball force still 0
                prevState = GameState;                                  // Keep track of previous state (in this case started)
                GameState = GameState.WaitingForButtonRelease;          // Set new state = waiting for a button release, so we do not get a premature ball direction
                shouldDirectionArrowBeVisible = true;                   // Now show the arrow
            }
        }

        /// <summary>
        /// Wait for a action button to release before doing anything more
        /// </summary>
        private void GameWaitingForButtonRelease()
        {
            if (playerInputButton == PlayerInput.None)              // if we released the button then...
            {
                switch (prevState)                                  // check the previous state to decide next action
                {
                    case GameState.Started:                         // if it was started...
                        GameState = GameState.WaitingForDirection;  // ...go to waiting for a direction
                        break;
                    case GameState.WaitingForDirection:             // if it was waiting for a direction...
                        GameState = GameState.WaitingForForce;      // ...go to waiting for a force
                        break;
                    case GameState.WaitingForForce:                 // if it was waiting for a force...
                        GameState = GameState.BallRolling;          // ... start rolling the ball
                        break;

                }
            }
        }

        /// <summary>
        /// Waiting for a direction
        /// </summary>
        private void GameWaitingForDirection()
        {
            shouldDirectionArrowBeVisible = true;   // now we want the arrow to be visible

            // if left mouse button is clicked
            if (playerInputButton == PlayerInput.LeftActionButton)
            {
                prevState = GameState;                                                          // Let's set the previous state before doing anything
                GameState = GameState.WaitingForButtonRelease;                                  // Go to waiting for a button release
                forceTimer = new DispatcherTimer();                                             // Start the force timer
                forceTimer.Tick += new EventHandler(ForceTimer_Tick);                           // Connect the timer event
                forceTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)(1000 / TicksPerSecond));   // Set the tick interval
                // start timer in next game state, we need to create the timer here so that next state knows about it
            }

            // if right mouse button is clicked, change rotation direction
            if (playerInputButton == PlayerInput.RightActionButton)
            {
                arrowRotSpeed = -arrowRotSpeed;                 // Change Direction
                prevState = GameState.Started;                  // Trick my own state system a little and set previous state to started, so we can trigger a waiting for button release
                GameState = GameState.WaitingForButtonRelease;  // Set waiting for button to release
            }

        }

        /// <summary>
        /// This runs every force timer tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForceTimer_Tick(object sender, EventArgs e)
        {
            ballForce += 10.0 / TicksPerSecond;         // calculate the ballforce
            if (ballForce >= 10.0) ballForce = 10.0;    // but no more than 10
        }

        /// <summary>
        /// Wait for a force
        /// </summary>
        private void GameWaitingForForce()
        {
            shouldDirectionArrowBeVisible = true;                                               // arrow still visible, but not rotating
            if (playerInputButton == PlayerInput.LeftActionButton && !forceTimer.IsEnabled)     // if action button left is clicked and force timer was created but not started
            {
                forceTimer.Start();                                                             // Start force timer and run as long as the button is pressed                
            }

            if (playerInputButton != PlayerInput.LeftActionButton && forceTimer.IsEnabled)      // When button is released and force timer is running
            {
                forceTimer.Stop();                                                              // stop the timer
                Ball.Speed = new Vector(ballForce * Math.Sin((BallDirection) * Math.PI / 180.0), ballForce * Math.Cos((BallDirection) * Math.PI / 180.0));  // Calculate the ball speed
                prevState = GameState;                                                          // set previous state as usual
                player.Strokes = Player.Strokes + 1;                                            // Increase player strokes now that he/she has done one :-)
                GameState = GameState.WaitingForButtonRelease;                                  // and go to waiting state again (a lot of waiting)                              
            }

            // Jump back to direction choice (option for the right action button, but a little cheating)
            if (playerInputButton == PlayerInput.RightActionButton && !forceTimer.IsEnabled)    // if action button right is clicked and force timer was created but not started
            {
                prevState = GameState.Started;                                                  // set previous to started
                GameState = GameState.WaitingForButtonRelease;                                  // And wait for button to release, then go the waiting for direction
            }
        }

        /// <summary>
        /// State for when ball went in hole
        /// </summary>
        private void GameInBallInHoleState()
        {
            double sc = Calculator.CalculateScore(player, this);    // Calculate player score
            if (sc > BestScore)                                     // if it is the best score...
            {
                bestScore = sc;                                     // ...replace best score and...
                bestPlayer = player.Name;                           // ...best player
            }
            lastScore = sc;                                         // ...Set last score
            ResetGame();                                            // Reset game, This goes a little fast, maybe change
        }

        /// <summary>
        /// When player is changed reset game
        /// </summary>
        private void GameInChangedPlayerState()
        {
            ResetGame();
        }

        /// <summary>
        /// When a game reset is needed
        /// </summary>
        private void ResetGame()
        {
            Ball.Position = new Vector(100, 20);    // Place ball on tee
            GameState = GameState.Started;          // Set game in started state
            LastStrokes = player.Strokes;           // set last strokes, but no records
            player.Strokes = 0;                     // reset strokes
            bounces = 0;                            // and bounces
        }

        /// <summary>
        /// Update the ball pos
        /// </summary>
        public void UpdateBallPosition()
        {
            shouldDirectionArrowBeVisible = false;          // now we do not want to see the arrow            
            ball.Speed = new Vector(ball.Speed.X * Course.Friction, ball.Speed.Y * course.Friction);    //update ball speed with course friction
            if (ball.Speed.Length < 0.25)                   // if ball speed is less than 0.25...
            {
                GameState = GameState.BallStopped;          // ...change state
            }
            ball.UpdateMovement();                          // Call the update movement method in the ball class

        }

        /// <summary>
        /// If ball is stopped
        /// </summary>
        private void GameInBallStoppedState()
        {
            ball.Speed = new Vector(0.0, 0.0);  // Stop the ball
            ballForce = 0.0;                    // Set force to zero
            GameState = GameState.Started;      // Change state to started

        }        

        /// <summary>
        /// Set and get for playerinput
        /// set it from the gui
        /// </summary>
        public PlayerInput PlayerInputButton
        {
            get { return playerInputButton; }
            set { playerInputButton = value; }
        }
        #endregion

        #region Collison Detections
        /// <summary>
        /// Check collisions for the ball
        /// </summary>
        private void CollisionDetections()
        {
            // Obstacles
            CollisionDetectionWithObstacles();

            // Walls
            CollisionDetectionWithWalls();

            // Hole
            CollisionDetectionWithHole();

        }

        /// <summary>
        /// Collision detection for the ball vs hole
        /// </summary>
        private void CollisionDetectionWithHole()
        {
            if ((Calculator.IsColliding(Ball, course.Hole)) && (Ball.Speed.Length < maxSpeedForBallInHole)) GameState = GameState.BallInHole; // if colliding change state
        }

        /// <summary>
        /// Collision detection for ball vs obstacles
        /// </summary>
        private void CollisionDetectionWithObstacles()
        {
            // Obstacles
            Vector compareVector = new Vector(1.0, 1.0);                                                    // set a simple compare vector
            foreach (Obstacle obstacle in course.Obstacles)                                                 // loop through all obstacles
            {
                Vector speedChange = Calculator.IsColliding(ball, obstacle, course);                        // if colliding create a speedchange (if not colliding a Vector(1,1) is returned
                if (speedChange != compareVector && frameCount >=0 )                                        // if the return vector is not equal to the compare vector...
                {
                    ball.Speed = new Vector(ball.Speed.X * speedChange.X, ball.Speed.Y * speedChange.Y);    // ..."reverse" ball speed and
                    bounces++;                                                                              // increase bounces
                    frameCount = - postBouncesCoolDownInFrames;                                             // Set frame count below zero and when it is zero again this method can run
                                                                                                            // This should prevent the ball from getting stuck
                }
            }
        }

        /// <summary>
        /// Collision detection for ball vs walls
        /// </summary>
        private void CollisionDetectionWithWalls()
        {
            // Walls
            double ballX = ball.Position.X;                                         // Store ball x location
            double ballY = ball.Position.Y;                                         // Store ball y location
            double radie = ball.Diameter / 2.0;                                     // Calculate ball radius
            if (((ballX - radie) <= 0 || (ballX + radie) >= Course.CourseSize.X) && frameCount >= 0)     // if ball x pos is out of course limits...
            {
                ball.Speed = new Vector(-ball.Speed.X, ball.Speed.Y);               // Reverse x direction
                bounces++;                                                          // increase bounces
                frameCount = -postBouncesCoolDownInFrames;                          // Set frame count below zero and when it is zero again this method can run
                                                                                    // This should prevent the ball from getting stuck
            }
            if (((ballY - radie) <= 0 || (ballY + radie) >= Course.CourseSize.Y) && frameCount >= 0)     // if ball y pos is out of course limits...
            {
                ball.Speed = new Vector(ball.Speed.X, -ball.Speed.Y);               // Reverse x direction
                bounces++;                                                          // increase bounces
                frameCount = -postBouncesCoolDownInFrames;                          // Set frame count below zero and when it is zero again this method can run
                                                                                    // This should prevent the ball from getting stuck
            }
        }
        #endregion

       
    }
}