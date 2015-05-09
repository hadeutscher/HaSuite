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
        public const string Version = "2.1";
        public static bool Restarting;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Startup
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            InfoManager = new WzInformationManager();
            SettingsManager = new WzSettingsManager(System.IO.Path.Combine(Application.StartupPath, "HCSettings.wz"), typeof(UserSettings), typeof(ApplicationSettings), typeof(Microsoft.Xna.Framework.Color));
            SettingsManager.Load();
            MultiBoard.RecalculateSettings();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            // Program run here
            Application.Run(new GUI.Initialization());

            // Shutdown
            SettingsManager.Save();
            if (Restarting)
            {
                Application.Restart();
            }
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            new ThreadExceptionDialog((Exception)e.ExceptionObject).ShowDialog();
            Environment.Exit(-1);
        }
    }
}

