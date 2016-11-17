// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-05-13
// ******************************

using System;

namespace Assignment7_MiniGolf
{
    /// <summary>
    /// Player class
    /// </summary>
    public class Player
    {
        private int strokes;    // Number of current Strokes
        private string name;    // Player Name
       
        private int bestScore;  // This players best score
       
        /// <summary>
        /// Empty constructor
        /// </summary>
        public Player():this(String.Empty)
        {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="playerName"></param>
        public Player(String playerName)
        {
            strokes = 0;
            name = playerName;
           
            bestScore = 0;
        }

        /// <summary>
        /// get and set Strokes
        /// </summary>
        public int Strokes
        {
            set { strokes = value; }
            get { return strokes; }
        }

        /// <summary>
        /// get and set name
        /// </summary>
        public string Name
        {
            set { name = value; }
            get { return name; }
        }       

        /// <summary>
        /// get and set best score
        /// </summary>
        public int BestScore
        {
            set { bestScore = value; }
            get { return bestScore; }
        }


    }

}