﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

using ThemeEngine;

using Utility.WindowsForms.Forms;

namespace OpenFL.Editor.CorePlugins.Forms
{
    public partial class AboutInfo : Form
    {

        public AboutInfo()
        {
            InitializeComponent();

            StyleManager.RegisterControls(this);


            lblVersionInfo.Text = "Debugger Version: " + Assembly.GetExecutingAssembly().GetName().Version;
            InitializeLibraryList();
        }


        public void InitializeLibraryList()
        {
            Icon = FLEditorPluginHost.FLEditorIcon;
            List<Assembly> asm = AppDomain.CurrentDomain.GetAssemblies().ToList();
            asm.Sort((x, y) => string.Compare(x.GetName().Name, y.GetName().Name, StringComparison.Ordinal));
            foreach (Assembly assembly in asm)
            {
                AssemblyName name = assembly.GetName();
                if (name.Name.StartsWith("System") ||
                    name.Name == "mscorlib" ||
                    name.Name == "netstandard" ||
                    name.Name == "Accessibility")
                {
                    continue;
                }

                lbLoadedAssemblies.Items.Add($"[{name.Version}]{name.Name}");
            }
        }

        private void llblVersionArchive_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MiniBrowser mb = new MiniBrowser(
                                             "http://213.109.162.193/flrepo/FLDebugger",
                                             args => BrowserNavigate(args, true, "FLDebugger")
                                            );
            mb.ShowDialog();
            mb.Dispose();
        }

        private void llblFLProjects_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MiniBrowser mb = new MiniBrowser(
                                             "http://213.109.162.193/flrepo/FLDebugger_Projects",
                                             args => BrowserNavigate(args, false, "FLDebugger_Projects")
                                            );
            mb.ShowDialog();
            mb.Dispose();
        }

        private void lblGithub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(@"https://github.com/ByteChkR/Byt3");
        }

        private void BrowserNavigate(WebBrowserNavigatingEventArgs e, bool directInstall, string projectName)
        {
            e.Cancel = true;

            //if (e.Url.AbsolutePath.EndsWith(".zip"))
            //{
            //    if (directInstall && UpdateChecker.UpdaterPresent)
            //    {
            //        UpdateChecker.Direct(
            //                             "http://213.109.162.193/flrepo/",
            //                             projectName,
            //                             Assembly.GetExecutingAssembly().Location,
            //                             Version.Parse("0.0.0.0"),
            //                             Version.Parse(Path.GetFileNameWithoutExtension(e.Url.AbsolutePath))
            //                            );
            //        Thread.Sleep(1000);
            //        Application.Exit();
            //    }
            //    else
            //    {
            //        Process.Start(e.Url.ToString());
            //    }
            //}
        }

    }
}