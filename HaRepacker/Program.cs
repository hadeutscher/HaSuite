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
using System.IO;
using System.Security.Principal;

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
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            string wzToLoad = null;
            if (args.Length > 0)
                wzToLoad = args[0];
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            bool firstRun = PrepareApplication(true);
            Application.Run(new MainForm(wzToLoad, true, firstRun));
            EndApplication(true, true);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) 
        {
            new ThreadExceptionDialog((Exception)e.ExceptionObject).ShowDialog();
            Environment.Exit(-1);
        }

        public static string GetLocalSettingsPath()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string our_folder = Path.Combine(appdata, "HaRepacker");
            if (!Directory.Exists(our_folder))
                Directory.CreateDirectory(our_folder);
            return Path.Combine(our_folder, "Settings.wz");
        }

        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        public static bool PrepareApplication(bool from_internal)
        {
            SettingsManager = new WzSettingsManager(GetLocalSettingsPath(), typeof(UserSettings), typeof(ApplicationSettings));
            bool loaded = false;
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    SettingsManager.Load();
                    loaded = true;
                    break;
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
            if (!loaded)
            {
                Warning.Error("Could not load settings file, make sure it is not in use. If it is not, delete it and try again.");
                return true;
            }
            bool firstRun = ApplicationSettings.FirstRun;
            if (ApplicationSettings.FirstRun)
            {
                //new FirstRunForm().ShowDialog();
                ApplicationSettings.FirstRun = false;
                SettingsManager.Save();
            }
            if (UserSettings.AutoAssociate && from_internal && IsUserAdministrator())
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
