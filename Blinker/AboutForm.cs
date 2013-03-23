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
    /// Class to support the meager functionality of the About box.
    /// </summary>
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void LaterButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = BlinkerResources.BlinkerSplash;
        }
    }
}
