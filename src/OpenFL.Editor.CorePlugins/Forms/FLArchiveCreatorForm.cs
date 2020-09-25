using System;
using System.IO;
using System.Windows.Forms;

using OpenFL.Editor.Forms;
using OpenFL.ResourceManagement;
using OpenFL.Serialization;

using ThemeEngine;

using Utility.ProgressFeedback;
using Utility.WindowsForms.CustomControls;

namespace OpenFL.Editor.CorePlugins.Forms
{
    public partial class FLArchiveCreatorForm : Form
    {

        private readonly FLScriptEditor Editor;

        public FLArchiveCreatorForm(FLScriptEditor editor)
        {
            Editor = editor;
            InitializeComponent();


            StyleManager.RegisterControls(this);
            
            Icon = FLEditorPluginHost.FLEditorIcon;
        }

        private void CopyDirectory(string source, string target, IProgressIndicator indicator)
        {
            Directory.CreateDirectory(target);
            if (!Directory.Exists(source))
            {
                return;
            }

            string[] files = Directory.GetFiles(source, "*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string file = files[i];
                indicator.SetProgress("Copying File:" + file, i, files.Length - 1);
                string dstFile = file.Replace(source, target);
                try
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(dstFile));
                    File.Copy(file, dstFile);
                }
                catch (Exception e)
                {
                }
            }

            indicator.Dispose();
        }

        private void btnCreatePackage_Click(object sender, EventArgs e)
        {
            if (sfdSavePackage.ShowDialog() == DialogResult.OK)
            {
                string curDir = Directory.GetCurrentDirectory();

                IProgressIndicator ind = ProgressIndicator.CreateProgressIndicator(panelProgress);
                ind.SetProgress("Running Preparations.", 0, 2);
                if (cbExport.Checked)
                {
                    IProgressIndicator preps = ind.CreateSubTask(false);
                    preps.SetProgress("Copying Folder Contents...", 0, 1);

                    string dir = Path.Combine(
                                              Path.GetDirectoryName(Directory.GetCurrentDirectory()),
                                              "temp_" +
                                              Path.GetFileNameWithoutExtension(Directory.GetCurrentDirectory())
                                             );
                    CopyDirectory(Directory.GetCurrentDirectory(), dir, preps.CreateSubTask(false));
                    Directory.SetCurrentDirectory(dir);
                    string[] files = Directory.GetFiles(dir, "*.fl", SearchOption.AllDirectories);
                    preps.SetProgress("Exporting FL Scripts..", 0, 1);
                    IProgressIndicator fl2flc = ind.CreateSubTask(false);
                    IProgressIndicator fl2flcf = fl2flc.CreateSubTask(false);
                    for (int i = 0; i < files.Length; i++)
                    {
                        string file = files[i];

                        fl2flc.SetProgress("Exporting File:" + file, i, files.Length);
                        fl2flcf.SetProgress("Parsing..", 0, 1);
                        Editor.Path = file;
                        Editor.FLContainer.SerializedProgram = null;

                        //Editor.InitProgram();
                        Editor.Build();
                        if (Editor.FLContainer.SerializedProgram == null)
                        {
                            return;
                        }

                        fl2flcf.SetProgress("Exporting..", 1, 1);
                        string f = file + "c";
                        Stream stream = File.OpenWrite(f);
                        FLSerializer.SaveProgram(
                                                 stream,
                                                 Editor.FLContainer.SerializedProgram,
                                                 Editor.FLContainer.InstructionSet,
                                                 new string[0]
                                                );
                        stream.Close();
                        if (!cbKeepFlScripts.Checked)
                        {
                            File.Delete(file);
                        }
                    }

                    fl2flcf.Dispose();
                    fl2flc.Dispose();
                    preps.Dispose();
                }

                ind.SetProgress("Creating Package.", 1, 2);
                ProgressIndicator.RunTask(
                                          indicator => ResourceManager.Create(
                                                                              Directory.GetCurrentDirectory(),
                                                                              sfdSavePackage.FileName,
                                                                              tbPackageName.Text,
                                                                              tbUnpackConfig.Text,
                                                                              indicator
                                                                             ),
                                          panelProgress,
                                          Application.DoEvents,
                                          ind.CreateSubTask(false)
                                         );

                ind.SetProgress("Cleaning Up.", 2, 2);
                string oldDir = Directory.GetCurrentDirectory();
                Directory.SetCurrentDirectory(curDir);
                if (cbExport.Checked)
                {
                    Directory.Delete(oldDir, true);
                }

                Close();
                ind.Dispose();
            }
        }

        private void cbExport_CheckedChanged(object sender, EventArgs e)
        {
            if (!cbExport.Checked)
            {
                cbKeepFlScripts.Checked = true;
                cbKeepFlScripts.Enabled = false;
            }
            else
            {
                cbKeepFlScripts.Enabled = true;
            }
        }

    }
}