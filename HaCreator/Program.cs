/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HaCreator.MapEditor;
using System.Runtime.InteropServices;
using MapleLib.WzLib;
using HaCreator.GUI;

namespace HaCreator
{
    static class Program
    {
        public static WzFileManager WzManager;
        public static WzInformationManager InfoManager;
        public static WzSettingsManager SettingsManager;

        static void SelfCheck()
        {
            string exceptResult = "";
            if (!MapleLib.WzLib.Util.WzTool.AESSelfCheck(ref exceptResult))
            {
                if (exceptResult.Contains("System.Core"))
                {
                    switch (MessageBox.Show("HaCreator has detected that your computer is not running the latest .NET Framework.\r\n\r\nClick \"Yes\" to close the editor and go to the download page on Microsoft's site\r\nClick \"No\" to ignore the error and continue loading\r\nClick \"Cancel\" to close the editor and do nothing", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error))
                    {
                        case DialogResult.Yes:
                            System.Diagnostics.Process.Start(@"http://www.microsoft.com/downloads/details.aspx?FamilyID=a9ef9a95-58d2-4e51-a4b7-bea3cc6962cb&displaylang=en#filelist");
                            Application.Exit();
                            return;
                        case DialogResult.No:
                            break;
                        case DialogResult.Cancel:
                            Application.Exit();
                            return;
                    }
                }
                else
                {
                    MessageBox.Show("Unknown error occured during wzKey generation self check: " + exceptResult);
                    Application.Exit();
                }
            }
            if (!GUI.Initialization.XNASelfCheck(ref exceptResult))
            {
                switch (MessageBox.Show("HaCreator has detected that your computer is not running the XNA Framework.\r\n\r\nClick \"Yes\" to close the editor and go to the download page on Microsoft's site\r\nClick \"No\" to ignore the error and continue loading\r\nClick \"Cancel\" to close the editor and do nothing", "Error", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error))
                {
                    case DialogResult.Yes:
                        System.Diagnostics.Process.Start(@"http://www.microsoft.com/downloads/details.aspx?FamilyID=53867a2a-e249-4560-8011-98eb3e799ef2&displaylang=en");
                        Application.Exit();
                        return;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        Application.Exit();
                        return;
                }
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SelfCheck();
            InfoManager = new WzInformationManager();
            SettingsManager = new WzSettingsManager(System.IO.Path.Combine(Application.StartupPath, "HCSettings.wz"), typeof(UserSettings), typeof(ApplicationSettings), typeof(Microsoft.Xna.Framework.Color));
            SettingsManager.Load();
            MultiBoard.RecalculateSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            try
            {
                Application.Run(new GUI.Initialization());
            }
            catch (Exception e)
            {
                Application.Run(new ExceptionHandler(e));
            }
#else
            //System.Windows.Application a = new System.Windows.Application();
            //a.Run(new CustomControls.HaRibbon());
            //Application.Run(new GUI.HaEditor());
            Application.Run(new GUI.Initialization());
#endif
            SettingsManager.Save();
        }
    }
}

