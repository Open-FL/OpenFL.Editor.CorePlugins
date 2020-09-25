using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using OpenFL.Editor.Forms;

using ThemeEngine;

namespace OpenFL.Editor.CorePlugins.Forms
{
    public partial class SettingsDialogForm : Form
    {

        private readonly bool NoSet;

        private Dictionary<string, FieldInfo> ThemeSettings;

        public SettingsDialogForm()
        {
            InitializeComponent();

            Closing += (sender, args) => FLScriptEditor.Settings.Save(FLEditorSettingsPath);

            StyleManager.RegisterControls(this);
            

            NoSet = true;
            cbLogParserStacktrace.Checked = FLScriptEditor.Settings.LogParserStacktrace;
            cbLogPreviewStacktrace.Checked = FLScriptEditor.Settings.LogProgramStacktrace;
            nudWidth.Value = FLScriptEditor.Settings.ResX;
            nudHeight.Value = FLScriptEditor.Settings.ResY;
            nudDepth.Value = FLScriptEditor.Settings.ResZ;

            cbUseMultithreadDebugger.Checked = FLScriptEditor.Settings.ExperimentalDebuggerMultiThread;
            cbExperimentalKernelLoading.Checked = FLScriptEditor.Settings.ExperimentalKernelLoading;
            NoSet = false;
        }

        public static string FLEditorSettingsPath => Path.Combine(FLScriptEditor.ConfigPath, "fleditor.settings.xml");

        private void btnSetHomeDir_Click(object sender, EventArgs e)
        {
            SetWorkingDirectory();
        }

        public void SetWorkingDirectory()
        {
            if (fbdSelectHomeDir.ShowDialog() == DialogResult.OK)
            {
                Directory.SetCurrentDirectory(fbdSelectHomeDir.SelectedPath);
            }
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            if (NoSet)
            {
                return;
            }

            SetResolution((int) nudWidth.Value, (int) nudHeight.Value, (int) nudDepth.Value);
        }

        private void nudHeight_ValueChanged(object sender, EventArgs e)
        {
            if (NoSet)
            {
                return;
            }

            SetResolution((int) nudWidth.Value, (int) nudHeight.Value, (int) nudDepth.Value);
        }

        public void SetResolution(int width, int height, int depth)
        {
            FLScriptEditor.Settings.ResX = width;
            FLScriptEditor.Settings.ResY = height;
            FLScriptEditor.Settings.ResZ = depth;
        }

        private void nudDepth_ValueChanged(object sender, EventArgs e)
        {
            if (NoSet)
            {
                return;
            }

            SetResolution((int) nudWidth.Value, (int) nudHeight.Value, (int) nudDepth.Value);
        }


        private void cbUseMultithreadDebugger_CheckedChanged(object sender, EventArgs e)
        {
            if (NoSet)
            {
                return;
            }

            FLScriptEditor.Settings.ExperimentalDebuggerMultiThread = cbUseMultithreadDebugger.Checked;

            MessageBox.Show(
                            "The changes will come into effect when restarting the application or reloading the CL Kernels",
                            "Restart Required",
                            MessageBoxButtons.OK
                           );
        }

        private void cbExperimentalKernelLoading_CheckedChanged(object sender, EventArgs e)
        {
            if (NoSet)
            {
                return;
            }

            FLScriptEditor.Settings.ExperimentalKernelLoading = cbExperimentalKernelLoading.Checked;

            MessageBox.Show(
                            "The changes will come into effect when restarting the application or reloading the CL Kernels",
                            "Restart Required",
                            MessageBoxButtons.OK
                           );
        }

        private void cbLogParserStacktrace_CheckedChanged(object sender, EventArgs e)
        {
            if (NoSet)
            {
                return;
            }

            FLScriptEditor.Settings.LogParserStacktrace = cbLogParserStacktrace.Checked;
        }

        private void cbLogPreviewStacktrace_CheckedChanged(object sender, EventArgs e)
        {
            if (NoSet)
            {
                return;
            }

            FLScriptEditor.Settings.LogProgramStacktrace = cbLogPreviewStacktrace.Checked;
        }
    }
}