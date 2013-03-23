// ------------------------------------------------------------------------------------
// Blinker - A drive activity light utility for Windows.
// 
// Written because the morons who designed his Dell XPS 15 put the drive access light
// on the *back* of the computer, because, well, that's where everyone looks for it 
// while computing. Later, used as a coding example for config files and progrmatic 
// perfmon counter access.
//
// Author:    Tony Martin
// Github ID: tirith42
// Email:     tony@noeticart.com
// Source:    https://github.com/tirith42/Blinker.git
// License:   GPL 3.0
// ------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blinker
{
    /// <summary>
    /// Class to handle the display and removal of the splash graphic.
    /// </summary>
    public partial class BlinkerSplashForm : Form
    {
        private const int DisplayTime = 3782;  // Display splash for 3.782 seconds. Why not?

        public BlinkerSplashForm()
        {
            InitializeComponent();
        }

        private void BlinkerSplashForm_Load(object sender, EventArgs e)
        {
            // Display splash, set up the timer.
            SplashImage.Image = BlinkerResources.BlinkerSplash;
            SplashTimer.Interval = DisplayTime;   
        }

        private void SplashTimer_Tick(object sender, EventArgs e)
        {
            // Shut it down on the first tick.
            SplashTimer.Stop();
            this.Close();
        }

        private void BlinkerSplashForm_Shown(object sender, EventArgs e)
        {
            // Kick off the display timer.
            SplashTimer.Start();
        }

        private void SplashImage_Click(object sender, EventArgs e)
        {
            // If the user clicks the splash, why make them wait any longer?
            // Shut dwn the timer and get rid of the splash.
            SplashTimer.Stop();
            this.Close();
        }
    }
}
