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
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Configuration;
using System.IO;

namespace Blinker
{
    public class BlinkerApp : Form
    {
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new BlinkerApp());
        }

        private NotifyIcon  BlinkerIcon;                // The actual icon displayed in the system tray.
        private ContextMenuStrip BlinkerMenu;           // The context menu for the app.
        private Timer       BlinkerTimer;               // Timer for the disk activity checks.
        private Icon        OnIcon;                     // The icon displayed to show an active disk.
        private Icon        OffIcon;                    // The icon displayed to show an inactive disk.

        private PerformanceCounter diskIO = null;       // The object used for the performance counter.
        private long currentValue = 0;                  // The most recently read disk IO value.
        private long oldValue = 0;                      // The previously read disk IO value.

        private string LEDcolor = "red";                // Stores the current color of the "LED" indicator.
        private string drive = "0 C:";                  // Stores the current drive being monitored.
        private bool showSplash = true;                 // Stores app setting: show the splash at startup
        private AppSettingsSection appSettingSection;   // Used to read/write application settings from app.config.
        private System.Configuration.Configuration config;
        private List<string> allDrives = null;          // List of all drives reported by PerfMon.

        public BlinkerApp()
        {
            // Load the list of available drives.
            LoadDriveList();

            // Setup the performance counter to track disk I/O.
            diskIO = new PerformanceCounter();
            diskIO.CategoryName = "PhysicalDisk";
            diskIO.CounterName = @"Disk Bytes/sec";
            diskIO.MachineName = ".";
            diskIO.InstanceName = drive;
            diskIO.ReadOnly = true;

            // Create the tray icon.
            BlinkerIcon = new NotifyIcon();

            // Load configuration information.
            LoadSettings();

            // Create the context menu and its items.
            BlinkerMenu = new ContextMenuStrip();
            BlinkerMenu.Items.Add("Settings");
            BlinkerMenu.Items.Add("Collect Garbage");
            BlinkerMenu.Items.Add("About");
            BlinkerMenu.Items.Add("-");
            BlinkerMenu.Items.Add("Exit");

            // Create the timer.
            BlinkerTimer = new Timer();
            BlinkerTimer.Interval = 1;
            BlinkerTimer.Tick += new EventHandler(BlinkerTimer_Tick);

            // Hook up the context menu and its required events.
            BlinkerIcon.ContextMenuStrip = BlinkerMenu;
            BlinkerMenu.Closing += new ToolStripDropDownClosingEventHandler(BlinkerMenu_Closing);
            BlinkerMenu.Opening += new System.ComponentModel.CancelEventHandler(BlinkerMenu_Opening);
            BlinkerMenu.ItemClicked += new ToolStripItemClickedEventHandler(BlinkerMenu_ItemClicked);
            
            // Get the icons set up.
            BlinkerIcon.Visible = true;
            OffIcon = new Icon(BlinkerResources.BlinkerOff, 16, 16);
            BlinkerIcon.Icon = OffIcon;

            // Kick things off.
            BlinkerTimer.Start();
        }

        /// <summary>
        /// Gets all settings from the app.config file.
        /// </summary>
        private void LoadSettings()
        {
            // Get setting information from config file.
            if (config == null)
            {
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            appSettingSection = (AppSettingsSection)config.GetSection("appSettings");
            LEDcolor = appSettingSection.Settings["ledcolor"].Value;
            drive = appSettingSection.Settings["drive"].Value;
            showSplash = Convert.ToBoolean(appSettingSection.Settings["showSplash"].Value);
            
            // Update the "On" icon to the user-selected color.
            SetIconColor();

            // The default drive in the settings file may not exist. If not,
            // just use the first item in the list of drives that *do* exist.
            if (IsDriveAvailable(drive))
            {
                diskIO.InstanceName = drive;
            }
            else
            {
                drive = allDrives[0];
                diskIO.InstanceName = allDrives[0];
            }

            // Set the tooltip on the system tray icon to the current drive.
            SetTooltip();
        }

        /// <summary>
        /// Checks the list of actual drives for the passed in drive to see if it exists.
        /// </summary>
        /// <param name="d">The drive to check for (in the format "0 C:").</param>
        /// <returns></returns>
        private bool IsDriveAvailable(string d)
        {
            foreach (string drive in allDrives)
            {
                if (drive == d)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Sets the color of the LED icon (actually, which icon to use based on color selection).
        /// </summary>
        private void SetIconColor()
        {
            // Set the "On" icon based on settings.
            switch (LEDcolor.ToUpper().Trim())
            {
                case "RED": OnIcon = new Icon(BlinkerResources.BlinkerRed, 16, 16); break;
                case "GREEN": OnIcon = new Icon(BlinkerResources.BlinkerGreen, 16, 16); break;
                case "BLUE": OnIcon = new Icon(BlinkerResources.BlinkerBlue, 16, 16); break;
                default: OnIcon = new Icon(BlinkerResources.BlinkerRed, 16, 16); break;
            }
        }

        /// <summary>
        /// Saves all settings to the app config file.
        /// </summary>
        private void SaveSettings()
        {
            // Save the settings to the config file.
            appSettingSection.Settings["ledcolor"].Value = LEDcolor;
            appSettingSection.Settings["drive"].Value = drive;
            appSettingSection.Settings["showSplash"].Value = showSplash.ToString();
            config.Save();

            // Make the new settings active.
            SetIconColor();
            diskIO.InstanceName = drive;
            SetTooltip();
        }

        /// <summary>
        /// Load the list of available drives. Get them from PerfMon instead of the OS, so we
        /// can get the correct instance names (i.e., drive C: comes in as "0 C:" and 0 is a drive ID).
        /// </summary>
        private void LoadDriveList()
        {
            if (allDrives == null)
            {
                allDrives = new List<string>();
            }

            // Get the list of all available drive instances from PerfMon.
            PerformanceCounterCategory cat = new PerformanceCounterCategory("PhysicalDisk");
            string[] instances = cat.GetInstanceNames();

            // Run through the list of drives and add them to our list of available drives on the system.
            foreach (string instance in instances)
            {
                using (PerformanceCounter cntr = new PerformanceCounter("PhysicalDisk", @"Disk Bytes/sec", instance, true))
                {
                    string n = cntr.InstanceName;
                    if (!(n.CompareTo("_Total") == 0))   // Don't count the "_Total" drive instance.
                    {
                        allDrives.Add(n);
                    }
                }
            }
        }

        /// <summary>
        /// Update the tooltip with the app name and the current drive.
        /// </summary>
        private void SetTooltip()
        {
            BlinkerIcon.Text = "Blinker [" + drive.Substring(2, 2) + "]";
        }

        /// <summary>
        /// Handles startup tasks.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            Visible = false;        // Hide the useless default form.
            ShowInTaskbar = false;  // Make sure we stay out of the Windows taskbar.

            // Show the splash.
            if (showSplash)
            {
                BlinkerSplashForm splash = new BlinkerSplashForm();
                splash.Show();
            }

            base.OnLoad(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                BlinkerIcon.Dispose();
                OnIcon.Dispose();
                OffIcon.Dispose();
                diskIO.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Shut down the app (and stop the timer, and close out the performance counter for the disk).
        /// </summary>
        private void OnExit()
        {
            BlinkerTimer.Stop();
            diskIO.Close();
            Application.Exit();
        }

        /// <summary>
        /// Display the About box.
        /// </summary>
        private void OnAbout()
        {
            AboutForm af = new AboutForm();
            af.Show();
        }

        /// <summary>
        /// Forces garbage collection when context menu item is clicked. Not necessary, but someone wanted
        /// to know how to do it, and here seemed as good a place as any to demonstrate.
        /// </summary>
        private void OnCollect()
        {
            GC.Collect();
        }

        /// <summary>
        /// Loads settings from the config file when users selects Settings from the context menu.
        /// </summary>
        private void OnSettings()
        {
            // Create the settings form and set the starting point properties.
            SettingsForm sf = null;
            try
            {
                sf = new SettingsForm(allDrives);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load a list of your available drives, for some reason. Maybe you should check them out.\n\nError: " + ex.Message, "Uh oh...", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            sf.LEDColor = LEDcolor;
            sf.Drive = drive;
            sf.ShowSplash = showSplash;

            // Display the dialog.
            sf.ShowDialog();

            // If the user clicked Done on the Settings dialog, then accept and save the new settings.
            if (sf.ExitMode == DialogExitType.Done)
            {
                LEDcolor = sf.LEDColor;
                drive = sf.Drive;
                showSplash = sf.ShowSplash;
                SaveSettings();
            }
        }

        /// <summary>
        /// Reads current disk activity every millisecond, and updates the LED display appropriately.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlinkerTimer_Tick(object sender, EventArgs e)
        {
            // Save the current bytes read or written per sec.
            currentValue = diskIO.RawValue;

            // Set the tray icon based on the change in data read. If the amount of bytes read or
            // written has changed in the last millisecond, then we have some disk activity.
            if (currentValue != oldValue)
            {
                if (BlinkerIcon.Icon != OnIcon)
                {
                    BlinkerIcon.Icon = OnIcon;
                }
            }
            else
            {
                if (BlinkerIcon.Icon != OffIcon)
                {
                    BlinkerIcon.Icon = OffIcon;
                }
            }

            // Save the last diskIO value we just got.
            oldValue = currentValue;

            // Make sure the UI gets a chance to update.
            System.Windows.Forms.Application.DoEvents();
        }

        /// <summary>
        /// Handle all click events on the context menu items.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlinkerMenu_ItemClicked(Object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.ToString().ToUpper())
            {
                case "SETTINGS": OnSettings(); break;
                case "ABOUT": OnAbout(); break;
                case "COLLECT GARBAGE": OnCollect(); break;
                case "EXIT": OnExit(); break;
                default: break;
            }
        }

        /// <summary>
        /// Handles the context menu opening event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlinkerMenu_Opening(Object sender, EventArgs e)
        {
            // Stop the event timer and don't read any disk activity while the conext menu is open.
            // This prevents missed click events on the menu.
            BlinkerTimer.Stop();
            BlinkerIcon.Icon = OffIcon;   // We don't want it to look like the disk is constantly reading while the menu is open.
        }

        /// <summary>
        /// Handles the context menu closing event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BlinkerMenu_Closing(Object sender, EventArgs e)
        {
            BlinkerTimer.Start();     // Restart the event timer.
        }
    }
}
