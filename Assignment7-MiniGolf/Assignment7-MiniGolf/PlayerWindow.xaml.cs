// ******************************
// David Täljsten, 
// AG3181, 
// Programming in C#, 2015-04-14
// ******************************

using System;
using System.Windows;

namespace Assignment7_MiniGolf
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        
        private Player player; // holds the player

        /// <summary>
        /// Constructor
        /// </summary>
        public PlayerWindow(Player gamePlayer )
        {            
            InitializeComponent();
            player = gamePlayer;    // set the player
        }

        /// <summary>
        /// If ok is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, RoutedEventArgs e)
        {            
            player.Name = txtPlayerName.Text.Trim();
            DialogResult = true;
        }
              
        /// <summary>
        /// get player
        /// </summary>
        public Player Player
        {
            get { return player; }
        }

        /// <summary>
        /// Focus text box when window is activated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playerWindow_Activated(object sender, EventArgs e)
        {
            txtPlayerName.Focus();
        }
    }
}
