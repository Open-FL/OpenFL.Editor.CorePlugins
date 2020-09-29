using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ThemeEngine;

using Utility.ADL;
using Utility.ADL.Configs;

namespace OpenFL.Editor.CorePlugins.Forms
{
    public partial class LogDisplayForm : Form
    {

        private readonly StringBuilder logOut = new StringBuilder();
        private bool allowLogEdit;
        private bool ignoreSeveritySet;


        public LogDisplayForm()
        {
            InitializeComponent();
            cbMinSeverity.Items.AddRange(Enum.GetNames(typeof(Verbosity)));

            StyleManager.RegisterControls(this);

            //FLScriptEditor.RegisterLogTheme(rtbLogOut);
            //FLScriptEditor.RegisterDefaultTheme(btnClear);
            //FLScriptEditor.RegisterDefaultTheme(panelSide);
            //FLScriptEditor.RegisterDefaultTheme(lbLogger);
            //FLScriptEditor.RegisterDefaultTheme(cbProjects);
            //FLScriptEditor.RegisterDefaultTheme(panelMain);
            //FLScriptEditor.RegisterDefaultTheme(btnRefreshProjects);
            //FLScriptEditor.RegisterDefaultTheme(cbMinSeverity);
            //FLScriptEditor.RegisterDefaultTheme(lblLogSeverity);
            //FLScriptEditor.RegisterDefaultTheme(panelSideMain);
            //FLScriptEditor.RegisterDefaultTheme(panelSideTop);
        }

        public void Append(string str)
        {
            if (IsDisposed)
            {
                return;
            }

            logOut.Append(str);
            allowLogEdit = true;
            rtbLogOut.AppendText(str);
            allowLogEdit = false;
        }

        private void rtbLogOut_TextChanged(object sender, EventArgs e)
        {
            if (!allowLogEdit)
            {
                rtbLogOut.Text = logOut.ToString();
            }

            rtbLogOut.ScrollToCaret();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            allowLogEdit = true;
            logOut.Clear();
            rtbLogOut.Text = "";
            allowLogEdit = false;
        }


        private void cbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProjects.SelectedIndex != -1)
            {
                ReadOnlyDictionary<IProjectDebugConfig, List<ADLLogger>> map = ADLLogger.GetReadOnlyLoggerMap();
                IProjectDebugConfig config = (IProjectDebugConfig) cbProjects.SelectedItem;
                lbLogger.Items.Clear();
                lbLogger.Items.AddRange(map[config].ToArray());
                ignoreSeveritySet = true;
                cbMinSeverity.SelectedIndex =
                    cbMinSeverity.Items.IndexOf(((Verbosity) config.GetMinSeverity()).ToString());
                ignoreSeveritySet = false;
            }
        }

        private void btnRefreshProjects_Click(object sender, EventArgs e)
        {
            RefreshProjects();
        }

        private void cbMinSeverity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ignoreSeveritySet && cbProjects.SelectedIndex != -1)
            {
                IProjectDebugConfig config = (IProjectDebugConfig) cbProjects.SelectedItem;
                config.SetMinSeverity((int) Enum.Parse(typeof(Verbosity), cbMinSeverity.SelectedItem.ToString()));
            }
        }


        private void RefreshProjects()
        {
            cbProjects.Items.Clear();
            lbLogger.Items.Clear();
            ReadOnlyDictionary<IProjectDebugConfig, List<ADLLogger>> map = ADLLogger.GetReadOnlyLoggerMap();
            cbProjects.Items.AddRange(map.Keys.ToArray());
        }

        private void LogDisplay_Load(object sender, EventArgs e)
        {
            Icon = FLEditorPluginHost.FLEditorIcon;
            RefreshProjects();
        }

    }
}