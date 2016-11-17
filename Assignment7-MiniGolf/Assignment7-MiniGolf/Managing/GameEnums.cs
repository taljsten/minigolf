// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-05-13
// ******************************

namespace Assignment7_MiniGolf
{
   
    /// <summary>
    /// The gamestate enum
    /// </summary>
    public enum GameState
    {
        None,
        NotStarted,                 // Before all inits in game manager
        Started,                    // Game started but ball is not rolling, it is on Tee      
        WaitingForDirection,        // Waiting for a ball direction
        WaitingForForce,            // Waiting for a ball force
        WaitingForButtonRelease,    // Waiting for the action button to release        
        BallRolling,                // Ball is rolling
        BallCollided,               // Ball just collided
        BallStopped,                // Ball is still
        BallInHole,                 // Ball is in hole, go to state Started  
        PlayerChanged

              
    }

    /// <summary>
    /// Actions for a player input
    /// </summary>
    public enum PlayerInput
    {
        LeftActionButton,   // Player pressed left action button
        RightActionButton,  // Player pressed right action button        
        None    
    }
}
