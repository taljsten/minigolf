// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-04-14
// ******************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Assignment7_MiniGolf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {    
        // Props
        private GameManager gameManager;            // The game manager. Brain
        private DispatcherTimer gameTimer;          // The game timer. Heart  

        #region Inits and constructors
        /// <summary>
        /// Constructor for this window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitGame();             // init the game
            StartTimer();           // Start the game timer for ticking att speed set in the game manager            
        }

        /// <summary>
        /// Init game
        /// </summary>
        private void InitGame()
        {

            gameManager = new GameManager(gameGrid, new Player("Default player"), 2);   // Start the game manager with difficulty 2
            sliderDiff.Value = 2.0;                                                     // Set the slider to same difficulty
            UpdatePlayerName();                                                         // update the player name text    
        }

        /// <summary>
        /// Start Game Timer. Used for ticking the game.
        /// </summary>
        private void StartTimer()
        {
            gameTimer = new DispatcherTimer();                                                      // Create the timer
            gameTimer.Tick += new EventHandler(GameTimer_Tick);                                     // and an eventhandler for the timer
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, (int)1000 / gameManager.TicksPerSecond);  // Set the tick speed
            gameTimer.Start();                                                                      // Start the timer

        }

        #endregion
        
        #region Game Tick
        /// <summary>
        /// Runs att specified frame rate given from Game Manager
        /// Initiated from Timer start.
        /// This is the game clock
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            gameManager.Update();   // let game manager handle all game mechanics updates
            lblGameState.Content = gameManager.GameState.ToString();            // TODO: Remove this
            lblPlayerInput.Content = gameManager.PlayerInputButton.ToString();   // TODO: Remove this                                        
            UpdateGui();            // Update all graphics      

        }

        /// <summary>
        /// All Graphic updates. Happens every tick
        /// </summary>
        private void UpdateGui()
        {
            UpdateArrow();          // Check and update the direction arrow
            UpdateMessages();       // Update screen messages
            UpdateStrokes();        // Update number of strokes
            UpdateForce();          // Update Force text
            UpdateScore();          // Update Score text
            UpdateBestScore();      // Maybe not updating every tick???
            UpdateLastScore();      // Maybe not updating every tick???
            UpdateLastStroke();     // Maybe not updating every tick ???
            UpdatePlayerButton();   // Set player button enabled or disabled
            UpdateDiffSlider();     // Set difficulty slider to enabled or disabled
            UpdateResetButton();    // Set Reset button enabled or disabled           
        }
        #endregion

        #region Arrow Update
        /// <summary>
        /// Update Arrow graphics and rotation
        /// This is 
        /// </summary>
        private void UpdateArrow()
        {
            
            if (gameManager.ShouldDirectionArrowBeVisible)              // If arrow should be visible
            {
                gameManager.Ball.ArrowVisibility = Visibility.Visible;          // Set arrow visible
                if (gameManager.GameState == GameState.WaitingForDirection)     // If we are waiting for the user to give a direction...
                    AddArrowAngle(gameManager.ArrowRotSpeed / gameManager.TicksPerSecond);  // ...continue to rotate the arrow using method AddArrowAngle below
            }
            else
            {
                gameManager.Ball.ArrowVisibility = Visibility.Hidden;   // if not set arrow to hidden.      
            }
        }

        /// <summary>
        /// Add an angle to the arrow
        /// Handled from game manager but shown on screen with this method
        /// </summary>
        /// <param name="angle"></param>
        private void AddArrowAngle(double angle)
        {
            gameManager.BallDirection += angle;                                                                 // Update ball direction in game manager            
            if (gameManager.BallDirection >= 360.0) gameManager.BallDirection -= 360.0;                         // Correct some angles if they are "out of bounds"
            if (gameManager.BallDirection <= 0.0) gameManager.BallDirection = 360 + gameManager.BallDirection;  // Correct some angles if they are "out of bounds"
            gameManager.Ball.Arrow.RenderTransform = new RotateTransform(gameManager.BallDirection);            // Change the Transform of the arrow
        }
        #endregion

        #region Control Updates (Texts, buttons and sliders)
        /// <summary>
        /// Update messages on screen
        /// </summary>
        private void UpdateMessages()
        {
            switch (gameManager.GameState)       // Check in which state we are in
            {
                case GameState.Started:         // If in start state
                    txtMessage.Text = "Click left mouse button.";
                    break;
                case GameState.WaitingForDirection: // if arrow is visible and waiting for direction
                    txtMessage.Text = "Click left mouse button to set ball direction.\nClick right mouse button to reverse rotation.";
                    break;
                case GameState.WaitingForForce:     // if game is waiting for a force
                    txtMessage.Text = "Hold left mouse button for the amount of force.";
                    break;
                case GameState.BallInHole:          // if ball went in
                    txtMessage.Text = "Yes, you did it.";   // TODO: This state need to be of a duration so we can see this message
                    break;
            }
        }

        /// <summary>
        /// Update text for strokes
        /// </summary>
        private void UpdateStrokes()
        {
            lblStrokes.Content = "Strokes: " + gameManager.Player.Strokes.ToString();
        }

        /// <summary>
        /// Update text for Force
        /// </summary>
        private void UpdateForce()
        {
            lblForce.Content = "Force: " + gameManager.BallForce.ToString("###0.0");
            progressForce.Value = gameManager.BallForce;
        }

        /// <summary>
        /// Update text for Score
        /// </summary>
        private void UpdateScore()
        {
            lblScore.Content = "Current Score: " + gameManager.PlayerScore.ToString("####0");
        }

        /// <summary>
        /// Update text for Best score
        /// </summary>
        private void UpdateBestScore()
        {
            lblBest.Content = "Best Score: " + gameManager.BestScore.ToString("####0") + " (" + gameManager.BestPlayer + ")";
        }

        /// <summary>
        /// Update text for last score
        /// </summary>
        private void UpdateLastScore()
        {
            lblLastScore.Content = "Last Score: " + gameManager.LastScore.ToString("####0");
        }

        /// <summary>
        /// Update text for last stroke
        /// </summary>
        private void UpdateLastStroke()
        {
            lblLastStrokes.Content = "Last Strokes: " + gameManager.LastStrokes.ToString();
        }

        /// <summary>
        /// Update text for player name
        /// </summary>
        private void UpdatePlayerName()
        {
            lblPlayer.Content = "Welcome " + gameManager.Player.Name + ".";
        }

        /// <summary>
        /// Set enabled or disabled for player button
        /// </summary>
        private void UpdatePlayerButton()
        {
            if (gameManager.GameState == GameState.NotStarted || gameManager.GameState == GameState.Started)
                btnPlayer.IsEnabled = true;
            else
                btnPlayer.IsEnabled = false;

        }

        /// <summary>
        /// Set enabled or disabled for Slider
        /// </summary>
        private void UpdateDiffSlider()
        {
            if (gameManager.GameState == GameState.NotStarted || gameManager.GameState == GameState.Started)
                sliderDiff.IsEnabled = true;
            else
                sliderDiff.IsEnabled = false;
            SetSliderValueToGame();
        }

        /// <summary>
        /// Set enabled or disabled for reset button
        /// </summary>
        private void UpdateResetButton()
        {
            if (gameManager.GameState == GameState.NotStarted || gameManager.GameState == GameState.Started)
                btnReset.IsEnabled = true;
            else
                btnReset.IsEnabled = false;
        }
        #endregion
        
        #region Player Input Actions
        /// <summary>
        /// If left mouse button was clicked on the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // If no player input is ongoing set a new player input
            if (gameManager.PlayerInputButton == PlayerInput.None) gameManager.PlayerInputButton = PlayerInput.LeftActionButton;
        }

        /// <summary>
        /// If left mouse button was released from grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // If correct action button is ongoing set the player input to none
            if (gameManager.PlayerInputButton == PlayerInput.LeftActionButton) gameManager.PlayerInputButton = PlayerInput.None;
        }

        /// <summary>
        /// If right button was clicked on grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            // If no player input is ongoing set a new player input
            if (gameManager.PlayerInputButton == PlayerInput.None) gameManager.PlayerInputButton = PlayerInput.RightActionButton;
        }

        /// <summary>
        /// If right button was released from Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            // If correct action button is ongoing set the player input to none
            if (gameManager.PlayerInputButton == PlayerInput.RightActionButton) gameManager.PlayerInputButton = PlayerInput.None;
        }
        #endregion

        #region Other Window events
        /// <summary>
        /// If player button was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayer_Click(object sender, RoutedEventArgs e)
        {
            Player initPlayer = new Player();       // set a temp player
            PlayerWindow playerWindow = new PlayerWindow(initPlayer);   // init the player window
            playerWindow.ShowDialog();                                  // Show it
            if (playerWindow.DialogResult.HasValue && playerWindow.DialogResult.Value)  // Get the result and do stuff if we have any
            {
                gameManager.Player = playerWindow.Player;               // set player in game manager
                gameManager.GameState = GameState.PlayerChanged;        // Notify the state machine that player is changed
                UpdatePlayerName();                                     // Update the player name on screen

            }
            playerWindow.Close();                   // Close the player window so that it "is ready to init" at the next time we want it
        }


        /// <summary>
        ///  if slider value was changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sliderDiff_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetSliderValueToGame(); // Call a method to set slider value. it is used in two places.
        }

        /// <summary>
        ///  Set slider value
        /// </summary>
        private void SetSliderValueToGame()
        {
            int value = (int)sliderDiff.Value;                      // cast the slider value to an integer
            lblDiff.Content = "Difficulty : " + value.ToString();   // Set the text
            
        }

        /// <summary>
        ///  if the reset button was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            gameManager.Reset((int)sliderDiff.Value);   // Reset game in game manager using difficulty. Cast to an integer.
        }

        /// <summary>
        /// if exit button was clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Close();        // Try closing application
        }

        /// <summary>
        /// When user wants to quit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Ask player if he really wants to quit this amazing game.
            if (MessageBox.Show("Do you want to quit playing?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                e.Cancel = true;
        } 
        #endregion
    }
}
