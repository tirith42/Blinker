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
    /// Indicates how the user left the dialog.
    /// </summary>
    public enum DialogExitType
    {
        Done,
        Cancel
    }

    /// <summary>
    /// Class to handle all functionality and events from the Settings dialog.
    /// </summary>
    public partial class SettingsForm : Form
    {
        private List<string> driveList;
        private List<NameValuePair> drivesForDisplay;
        private string m_LEDcolor;
        private string m_Drive;
        private bool m_ShowSplash;
        private DialogExitType m_ExitType = DialogExitType.Cancel;

        /// <summary>
        /// Initialize the Settings dialog.
        /// </summary>
        /// <param name="drives">A list of drives available from the PerfMon library.</param>
        public SettingsForm(List<string> drives)
        {
            if (drives == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                driveList = drives;
            }
            InitializeComponent();
        }

        /// <summary>
        /// Handle the form load event by pupulating lists of values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsForm_Load(object sender, EventArgs e)
        {
            drivesForDisplay = new List<NameValuePair>();

            // Build a drive list we can use for display
            int i = 0;
            foreach (string d in driveList)
            {
                drivesForDisplay.Add(new NameValuePair(Convert.ToInt32(d.Substring(0, 2).Trim()), d.Substring(2, 2)));
                DriveCombo.Items.Add(drivesForDisplay[i++]);
            }

            // Fill in the list of available colors.
            ColorCombo.Items.Add("Red");
            ColorCombo.Items.Add("Green");
            ColorCombo.Items.Add("Blue");

            // Initialize the drive and LED color.
            SetDrive(m_Drive);
            SetColor(m_LEDcolor);
            SetSplash(m_ShowSplash);
        }

        /// <summary>
        /// Sets the drive selection combobox to the drive passed in.
        /// </summary>
        /// <param name="d">The drive to which the drive combobox should be set.</param>
        private void SetDrive(string d)
        {
            if (d.Length < 2)
            {
                throw new ArgumentException(String.Format("The drive value passed in ('{0}') is not valid.", d));
            }

            int IDtoFind = Convert.ToInt32(d.Substring(0,2));

            // Find the index of the drive in the combo box that matches the one passed in from the caller.
            int i = 0;
            foreach (NameValuePair nvp in DriveCombo.Items)
            {
                if (nvp.id == IDtoFind)
                {
                    // Got a match, so set it and get out
                    DriveCombo.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        /// <summary>
        /// Sets the Color selector combobox to the passed-in color.
        /// </summary>
        /// <param name="c">The color to which the combobox should be set.</param>
        private void SetColor(string c)
        {
            int i = 0;
            foreach (string clr in ColorCombo.Items)
            {
                if (clr.ToUpper() == c.ToUpper())
                {
                    ColorCombo.SelectedIndex = i;
                    break;
                }
                i++;
            }
        }

        /// <summary>
        /// Sets the Show Splash checkbox value.
        /// </summary>
        /// <param name="showIt">True to check the box, false to clear it.</param>
        private void SetSplash(bool showIt)
        {
            SplashCheck.Checked = showIt;
        }

        /// <summary>
        /// Gets or sets the user-selected icon color.
        /// </summary>
        public string LEDColor
        {
            get { return m_LEDcolor; }
            set { m_LEDcolor = value; }
        }

        /// <summary>
        /// Gets or sets the user-selected drive to monitor.
        /// </summary>
        public string Drive
        {
            get { return m_Drive; }
            set { m_Drive = value; }
        }

        /// <summary>
        /// Gets or sets the Show Splash Screen option.
        /// </summary>
        public bool ShowSplash
        {
            get { return m_ShowSplash; }
            set { m_ShowSplash = value; }
        }

        /// <summary>
        /// Provides access to the method by which the user closed this dialog.
        /// </summary>
        public DialogExitType ExitMode
        {
            get { return m_ExitType; }
        }

        /// <summary>
        /// Handles a change of the icon color combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ColorCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_LEDcolor = (string)ColorCombo.SelectedItem;
            SetPreviewImage();
        }

        /// <summary>
        /// Sets the icon preview image based on the current user-selected LED color.
        /// </summary>
        private void SetPreviewImage()
        {
            switch (m_LEDcolor.ToUpper())
            {
                case "RED": PreviewImage.Image = BlinkerResources.BlinkerRed.ToBitmap(); break;
                case "GREEN": PreviewImage.Image = BlinkerResources.BlinkerGreen.ToBitmap(); break;
                case "BLUE": PreviewImage.Image = BlinkerResources.BlinkerBlue.ToBitmap(); break;
            }
        }

        /// <summary>
        /// Updates the drive value when it is changed by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DriveCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            NameValuePair d = DriveCombo.SelectedItem as NameValuePair;
            m_Drive = d.id + " " + d.value;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Get out, telling the rest of the app that the user chose to save changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DoneButton_Click(object sender, EventArgs e)
        {
            m_ExitType = DialogExitType.Done;
            this.Close();
        }

        /// <summary>
        /// Update the value of the Show Splash Screen option when the user changes it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashCheck_CheckedChanged(object sender, EventArgs e)
        {
            m_ShowSplash = SplashCheck.Checked;
        }
    }

    /// <summary>
    /// Used for Combo boxes to keep track of what's displayed (the string part) and a corresponding id
    /// (the int part).
    /// </summary>
    public class NameValuePair
    {
        public int id;
        public string value;
        public NameValuePair(int i, string v)
        {
            id = i;
            value = v;
        }
        /// <summary>
        /// Returns the string part so the Combobox has something to show.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return value;
        }
    }
}
