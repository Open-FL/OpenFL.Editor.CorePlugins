using ThemeEngine;

namespace OpenFL.Editor.CorePlugins.Forms
{
    partial class SettingsDialogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.gbResolution = new System.Windows.Forms.GroupBox();
            this.lblDepth = new System.Windows.Forms.Label();
            this.nudDepth = new System.Windows.Forms.NumericUpDown();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.lblHeight = new System.Windows.Forms.Label();
            this.lblWidth = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.gbMiscellaneous = new System.Windows.Forms.GroupBox();
            this.btnSetHomeDir = new System.Windows.Forms.Button();
            this.gbDebugger = new System.Windows.Forms.GroupBox();
            this.cbExperimentalKernelLoading = new System.Windows.Forms.CheckBox();
            this.cbUseMultithreadDebugger = new System.Windows.Forms.CheckBox();
            this.cdChangeThemeColor = new System.Windows.Forms.ColorDialog();
            this.sfdTheme = new System.Windows.Forms.SaveFileDialog();
            this.fbdSelectHomeDir = new System.Windows.Forms.FolderBrowserDialog();
            this.cbLogPreviewStacktrace = new System.Windows.Forms.CheckBox();
            this.cbLogParserStacktrace = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            this.gbResolution.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.gbMiscellaneous.SuspendLayout();
            this.gbDebugger.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudWidth
            // 
            this.nudWidth.BackColor = System.Drawing.Color.DimGray;
            this.nudWidth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nudWidth.Location = new System.Drawing.Point(59, 17);
            this.nudWidth.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(45, 16);
            this.nudWidth.TabIndex = 0;
            this.nudWidth.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.nudWidth.ValueChanged += new System.EventHandler(this.nudWidth_ValueChanged);
            // 
            // gbResolution
            // 
            this.gbResolution.BackColor = System.Drawing.Color.DimGray;
            this.gbResolution.Controls.Add(this.lblDepth);
            this.gbResolution.Controls.Add(this.nudDepth);
            this.gbResolution.Controls.Add(this.nudHeight);
            this.gbResolution.Controls.Add(this.lblHeight);
            this.gbResolution.Controls.Add(this.lblWidth);
            this.gbResolution.Controls.Add(this.nudWidth);
            this.gbResolution.Location = new System.Drawing.Point(12, 12);
            this.gbResolution.Name = "gbResolution";
            this.gbResolution.Size = new System.Drawing.Size(365, 86);
            this.gbResolution.TabIndex = 1;
            this.gbResolution.TabStop = false;
            this.gbResolution.Text = "Resolution";
            // 
            // lblDepth
            // 
            this.lblDepth.AutoSize = true;
            this.lblDepth.Location = new System.Drawing.Point(15, 55);
            this.lblDepth.Name = "lblDepth";
            this.lblDepth.Size = new System.Drawing.Size(39, 13);
            this.lblDepth.TabIndex = 5;
            this.lblDepth.Text = "Depth:";
            // 
            // nudDepth
            // 
            this.nudDepth.BackColor = System.Drawing.Color.DimGray;
            this.nudDepth.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nudDepth.Location = new System.Drawing.Point(59, 56);
            this.nudDepth.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.nudDepth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDepth.Name = "nudDepth";
            this.nudDepth.Size = new System.Drawing.Size(45, 16);
            this.nudDepth.TabIndex = 4;
            this.nudDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudDepth.ValueChanged += new System.EventHandler(this.nudDepth_ValueChanged);
            // 
            // nudHeight
            // 
            this.nudHeight.BackColor = System.Drawing.Color.DimGray;
            this.nudHeight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.nudHeight.Location = new System.Drawing.Point(59, 36);
            this.nudHeight.Maximum = new decimal(new int[] {
            2048,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(45, 16);
            this.nudHeight.TabIndex = 3;
            this.nudHeight.Value = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.nudHeight.ValueChanged += new System.EventHandler(this.nudHeight_ValueChanged);
            // 
            // lblHeight
            // 
            this.lblHeight.AutoSize = true;
            this.lblHeight.Location = new System.Drawing.Point(15, 35);
            this.lblHeight.Name = "lblHeight";
            this.lblHeight.Size = new System.Drawing.Size(41, 13);
            this.lblHeight.TabIndex = 2;
            this.lblHeight.Text = "Height:";
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(15, 16);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(38, 13);
            this.lblWidth.TabIndex = 1;
            this.lblWidth.Text = "Width:";
            // 
            // panelButtons
            // 
            this.panelButtons.BackColor = System.Drawing.Color.DimGray;
            this.panelButtons.Controls.Add(this.gbMiscellaneous);
            this.panelButtons.Controls.Add(this.gbDebugger);
            this.panelButtons.Controls.Add(this.gbResolution);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(389, 294);
            this.panelButtons.TabIndex = 2;
            // 
            // gbMiscellaneous
            // 
            this.gbMiscellaneous.Controls.Add(this.btnSetHomeDir);
            this.gbMiscellaneous.Location = new System.Drawing.Point(12, 231);
            this.gbMiscellaneous.Name = "gbMiscellaneous";
            this.gbMiscellaneous.Size = new System.Drawing.Size(365, 53);
            this.gbMiscellaneous.TabIndex = 20;
            this.gbMiscellaneous.TabStop = false;
            this.gbMiscellaneous.Text = "Miscellaneous";
            // 
            // btnSetHomeDir
            // 
            this.btnSetHomeDir.BackColor = System.Drawing.Color.DimGray;
            this.btnSetHomeDir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSetHomeDir.Location = new System.Drawing.Point(8, 19);
            this.btnSetHomeDir.Name = "btnSetHomeDir";
            this.btnSetHomeDir.Size = new System.Drawing.Size(351, 23);
            this.btnSetHomeDir.TabIndex = 15;
            this.btnSetHomeDir.Text = "Set Home Directory";
            this.btnSetHomeDir.UseVisualStyleBackColor = false;
            this.btnSetHomeDir.Click += new System.EventHandler(this.btnSetHomeDir_Click);
            // 
            // gbDebugger
            // 
            this.gbDebugger.Controls.Add(this.cbLogPreviewStacktrace);
            this.gbDebugger.Controls.Add(this.cbLogParserStacktrace);
            this.gbDebugger.Controls.Add(this.cbExperimentalKernelLoading);
            this.gbDebugger.Controls.Add(this.cbUseMultithreadDebugger);
            this.gbDebugger.Location = new System.Drawing.Point(12, 104);
            this.gbDebugger.Name = "gbDebugger";
            this.gbDebugger.Size = new System.Drawing.Size(365, 121);
            this.gbDebugger.TabIndex = 19;
            this.gbDebugger.TabStop = false;
            this.gbDebugger.Text = "Debugger";
            // 
            // cbExperimentalKernelLoading
            // 
            this.cbExperimentalKernelLoading.AutoSize = true;
            this.cbExperimentalKernelLoading.Location = new System.Drawing.Point(8, 42);
            this.cbExperimentalKernelLoading.Name = "cbExperimentalKernelLoading";
            this.cbExperimentalKernelLoading.Size = new System.Drawing.Size(160, 17);
            this.cbExperimentalKernelLoading.TabIndex = 1;
            this.cbExperimentalKernelLoading.Text = "Experimental Kernel Loading";
            this.cbExperimentalKernelLoading.UseVisualStyleBackColor = true;
            this.cbExperimentalKernelLoading.CheckedChanged += new System.EventHandler(this.cbExperimentalKernelLoading_CheckedChanged);
            // 
            // cbUseMultithreadDebugger
            // 
            this.cbUseMultithreadDebugger.AutoSize = true;
            this.cbUseMultithreadDebugger.Location = new System.Drawing.Point(8, 19);
            this.cbUseMultithreadDebugger.Name = "cbUseMultithreadDebugger";
            this.cbUseMultithreadDebugger.Size = new System.Drawing.Size(203, 17);
            this.cbUseMultithreadDebugger.TabIndex = 0;
            this.cbUseMultithreadDebugger.Text = "Experimental Multithreaded Debugger";
            this.cbUseMultithreadDebugger.UseVisualStyleBackColor = true;
            this.cbUseMultithreadDebugger.CheckedChanged += new System.EventHandler(this.cbUseMultithreadDebugger_CheckedChanged);
            // 
            // sfdTheme
            // 
            this.sfdTheme.DefaultExt = "xml";
            this.sfdTheme.Title = "Save the Theme";
            // 
            // fbdSelectHomeDir
            // 
            this.fbdSelectHomeDir.Description = "Select the Root directory from where the FL scripts will be called(this will fix " +
    "filenames)";
            // 
            // cbLogPreviewStacktrace
            // 
            this.cbLogPreviewStacktrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLogPreviewStacktrace.AutoSize = true;
            this.cbLogPreviewStacktrace.Location = new System.Drawing.Point(8, 88);
            this.cbLogPreviewStacktrace.Name = "cbLogPreviewStacktrace";
            this.cbLogPreviewStacktrace.Size = new System.Drawing.Size(140, 17);
            this.cbLogPreviewStacktrace.TabIndex = 33;
            this.cbLogPreviewStacktrace.Text = "Log Preview Stacktrace";
            this.cbLogPreviewStacktrace.UseVisualStyleBackColor = true;
            this.cbLogPreviewStacktrace.CheckedChanged += new System.EventHandler(this.cbLogPreviewStacktrace_CheckedChanged);
            // 
            // cbLogParserStacktrace
            // 
            this.cbLogParserStacktrace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbLogParserStacktrace.AutoSize = true;
            this.cbLogParserStacktrace.Location = new System.Drawing.Point(8, 65);
            this.cbLogParserStacktrace.Name = "cbLogParserStacktrace";
            this.cbLogParserStacktrace.Size = new System.Drawing.Size(132, 17);
            this.cbLogParserStacktrace.TabIndex = 32;
            this.cbLogParserStacktrace.Text = "Log Parser Stacktrace";
            this.cbLogParserStacktrace.UseVisualStyleBackColor = true;
            this.cbLogParserStacktrace.CheckedChanged += new System.EventHandler(this.cbLogParserStacktrace_CheckedChanged);
            // 
            // SettingsDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 294);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SettingsDialogForm";
            this.Text = "Settings Dialog";
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            this.gbResolution.ResumeLayout(false);
            this.gbResolution.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.gbMiscellaneous.ResumeLayout(false);
            this.gbDebugger.ResumeLayout(false);
            this.gbDebugger.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.GroupBox gbResolution;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Label lblHeight;
        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnSetHomeDir;
        private System.Windows.Forms.ColorDialog cdChangeThemeColor;
        private System.Windows.Forms.SaveFileDialog sfdTheme;
        private System.Windows.Forms.Label lblDepth;
        private System.Windows.Forms.NumericUpDown nudDepth;
        private System.Windows.Forms.GroupBox gbMiscellaneous;
        private System.Windows.Forms.GroupBox gbDebugger;
        [StyleSelector("critical")]
        private System.Windows.Forms.CheckBox cbExperimentalKernelLoading;
        [StyleSelector("critical")]
        private System.Windows.Forms.CheckBox cbUseMultithreadDebugger;
        private System.Windows.Forms.FolderBrowserDialog fbdSelectHomeDir;
        private System.Windows.Forms.CheckBox cbLogPreviewStacktrace;
        private System.Windows.Forms.CheckBox cbLogParserStacktrace;
    }
}