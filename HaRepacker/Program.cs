/* Copyright (C) 2015 haha01haha01

* This Source Code Form is subject to the terms of the Mozilla Public
* License, v. 2.0. If a copy of the MPL was not distributed with this
* file, You can obtain one at http://mozilla.org/MPL/2.0/. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using HaRepackerLib;
using HaRepacker.GUI;
using Microsoft.Win32;
using System.Threading;
using MapleLib.WzLib;
using System.IO.Pipes;
using System.Text;
using System.Security.Permissions;

namespace HaRepacker
{
    public static class Program
    {
        public static WzFileManager WzMan = new HaRepackerLib.WzFileManager();
        public static WzSettingsManager SettingsManager;
        public static NamedPipeServerStream pipe;
        public static Thread pipeThread;
        public const string pipeName = "HaRepacker";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            string wzToLoad = null;
            if (args.Length > 0)
                wzToLoad = args[0];

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool firstRun = PrepareApplication(true);
            Application.Run(new MainForm(wzToLoad, true, firstRun));
            EndApplication(true, true);
        }

        public static bool PrepareApplication(bool from_internal)
        {
            SettingsManager = new WzSettingsManager(System.IO.Path.Combine(Application.StartupPath, "HRSettings.wz"), typeof(UserSettings), typeof(ApplicationSettings));
            int tryCount = 0;
        tryagain:
            try
            {
                SettingsManager.Load();
            }
            catch
            {
                tryCount++;
                if (tryCount < 5)
                {
                    Thread.Sleep(1000);
                    goto tryagain;
                }
                else
                {
                    Warning.Error("Could not load settings file, make sure it is not in use. If it is not, delete it and try again.");
                    return true;
                }
            }
            bool firstRun = ApplicationSettings.FirstRun;
            if (ApplicationSettings.FirstRun)
            {
                //new FirstRunForm().ShowDialog();
                ApplicationSettings.FirstRun = false;
                SettingsManager.Save();
            }
            if (UserSettings.AutoAssociate && from_internal)
            {
                string path = Application.ExecutablePath;
                Registry.ClassesRoot.CreateSubKey(".wz").SetValue("", "WzFile");
                RegistryKey wzKey = Registry.ClassesRoot.CreateSubKey("WzFile");
                wzKey.SetValue("", "Wz File");
                wzKey.CreateSubKey("DefaultIcon").SetValue("", path + ",1");
                wzKey.CreateSubKey("shell\\open\\command").SetValue("", "\"" + path + "\" \"%1\"");
            }
            return firstRun;
        }

        public static void EndApplication(bool usingPipes, bool disposeFiles)
        {
            if (pipe != null && usingPipes)
            {
                pipe.Close();
            }
            if (disposeFiles)
            {
                WzMan.Terminate();
            }
            SettingsManager.Save();
        }
    }
}
