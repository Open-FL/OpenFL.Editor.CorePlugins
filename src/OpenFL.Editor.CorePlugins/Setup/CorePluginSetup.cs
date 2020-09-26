using System.Drawing;
using System.IO;
using System.Windows.Forms;

using OpenFL.Core;
using OpenFL.Core.Buffers;
using OpenFL.Core.DataObjects.ExecutableDataObjects;
using OpenFL.Editor.CorePlugins.Forms;
using OpenFL.Editor.Forms;
using OpenFL.Editor.Forms.Util;
using OpenFL.Editor.Utils.Plugins;
using OpenFL.ResourceManagement;
using OpenFL.Serialization;

using PluginSystem.Core.Pointer;
using PluginSystem.FileSystem;
using PluginSystem.Utility;

using Utility.ProgressFeedback;
using Utility.WindowsForms.CustomControls;

namespace OpenFL.Editor.CorePlugins.Setup
{
    public class CorePluginSetup : APlugin<FLEditorPluginHost>
    {

        public override string Name => "fl-editor-core-plugins";

        public override bool IsMainPlugin => true;

        private FLArchiveCreatorForm archiveCreatorForm = null;
        private LogDisplayForm logDisplayForm = null;
        private SettingsDialogForm settingsDialogForm = null;
        private AboutInfo aboutForm = null;
        private ListPluginsForm pluginList = null;
        private SaveFileDialog sfdScript;

        private bool HasDefaults = false;

        public override void OnLoad(PluginAssemblyPointer ptr)
        {
            base.OnLoad(ptr);

            sfdScript = new SaveFileDialog();
            sfdScript.DefaultExt = "fl";
            sfdScript.FileName = "FLFile";
            sfdScript.Filter = "FLScript|*.fl|Exported FL Script|*.flc|Exported Image|*.png";
            sfdScript.Title = "Save FL Script";

            PluginHost.Editor.OnLoadStartupFile += OnInitLoad;
        }

        private void OnInitLoad(string path)
        {
            if (path.EndsWith(".flres"))
            {
                UnpackPackage(path, true);
                PluginHost.Editor.Path = FLScriptEditor.TempEditorContentPath;
            }
        }

        #region Toolbar Items

        [ToolbarItem("FL", 2)]
        private void FLDummy()
        {
        }

        [ToolbarItem("File", 0)]
        private void FileDummy()
        {
        }

        [ToolbarItem("Run", 1)]
        private void RunDummy()
        {
        }

        [ToolbarItem("View", 4)]
        private void ViewDummy()
        {
        }

        [ToolbarItem("Plugins", 5)]
        private void PluginDummy()
        {
        }

        [ToolbarItem("About", int.MaxValue)]
        private void AboutDummy()
        {

        }

        #endregion

        #region FL Package Creator




        [ToolbarItem("FL/Create Package", 1)]
        private void OnCreatePackage()
        {
            if (archiveCreatorForm == null || archiveCreatorForm.IsDisposed)
            {
                archiveCreatorForm = new FLArchiveCreatorForm(PluginHost.Editor);
                archiveCreatorForm.Show();
            }
        }

        [ToolbarItem("FL/Unpack Package", 2)]
        public void OnUnpackPackage()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                UnpackPackage(dialog.FileName);
            }
        }

        private void UnpackPackage(IProgressIndicator indicator, string filename, bool createSubfolder = false)
        {
            indicator.SetProgress("Loading Package: " + filename, 1, 2);

            string name = ResourceManager.Load(filename);
            if (name == null)
            {
                return;
            }

            string path = Directory.GetCurrentDirectory();
            if (createSubfolder)
            {
                Directory.CreateDirectory(name);
                path = Path.Combine(path, name);
            }

            IProgressIndicator sub = indicator.CreateSubTask();
            ResourceManager.Activate(name, sub, path);
            indicator.SetProgress("Activating Package: " + name, 2, 2);
            indicator.Dispose();
        }

        private void UnpackPackage(string filename, bool createSubfolder = false)
        {
            ProgressIndicator.RunTask(
                                      indicator => UnpackPackage(indicator, filename, createSubfolder),
                                      Application.DoEvents
                                     );
        }



        #endregion

        #region Log Display

        [ToolbarItem("View/View Debug Log", 0)]
        private void OnViewerOpen()
        {
            if (logDisplayForm == null || logDisplayForm.IsDisposed)
            {
                logDisplayForm = new LogDisplayForm();
                logDisplayForm.Closing += LogDisplayForm_Closing;
                PluginHost.Editor.AddLogger(logDisplayForm.Append);
                logDisplayForm.Show();
            }
        }

        private void LogDisplayForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PluginHost.Editor.RemoveLogger(logDisplayForm.Append);
        }

        #endregion

        #region Settings Dialog


        [ToolbarItem("File/Settings...", 2)]
        private void OnShowSettings()
        {
            if (settingsDialogForm == null || settingsDialogForm.IsDisposed)
            {
                settingsDialogForm = new SettingsDialogForm();
                settingsDialogForm.Show();
            }
        }

        #endregion

        #region Default Toolbar Entries


        [ToolbarItem("About/Info")]
        private void OnShowAbout()
        {
            if (aboutForm == null || aboutForm.IsDisposed)
            {
                aboutForm = new AboutInfo();
            }

            aboutForm.Show();
        }


        [ToolbarItem("File/Exit...", int.MaxValue)]
        private void OnExit()
        {
            PluginHost.Editor.Close();
        }

        [ToolbarItem("File/Open...", 0)]
        private void OnLoadFile()
        {
            PluginHost.Editor.OpenFileDialog();
        }


        [ToolbarItem("File/Save...", 1)]
        private void OnSaveFile()
        {
            if (sfdScript.ShowDialog() == DialogResult.OK)
            {
                if (sfdScript.FileName.EndsWith(".fl"))
                {
                    PluginHost.Editor.Path = sfdScript.FileName;
                    File.WriteAllText(PluginHost.Editor.Path, PluginHost.Editor.WrittenText);
                }
                else if (sfdScript.FileName.EndsWith(".flc"))
                {
                    PluginHost.Editor.FLContainer.SerializedProgram = null;
                    PluginHost.Editor.WriteAndBuild();
                    PluginHost.Editor.Build();
                    if (PluginHost.Editor.FLContainer.SerializedProgram == null)
                    {
                        return;
                    }

                    Stream stream = File.OpenWrite(sfdScript.FileName);
                    FLSerializer.SaveProgram(
                                             stream,
                                             PluginHost.Editor.FLContainer.SerializedProgram,
                                             PluginHost.Editor.FLContainer.InstructionSet,
                                             new string[0]
                                            );
                    stream.Close();
                }
                else if (sfdScript.FileName.EndsWith(".png"))
                {
                    PluginHost.Editor.FLContainer.SerializedProgram = null;
                    PluginHost.Editor.WriteAndBuild();
                    PluginHost.Editor.Build();
                    if (PluginHost.Editor.FLContainer.SerializedProgram == null)
                    {
                        return;
                    }
                    
                    FLBuffer input = PluginHost.Editor.FLContainer.CreateBuffer(
                                                                                FLScriptEditor.Settings.ResX,
                                                                                FLScriptEditor.Settings.ResY,
                                                                                1,
                                                                                "ImageExportInput"
                                                                               );
                    FLProgram prog =
                        PluginHost.Editor.FLContainer.SerializedProgram.Initialize(PluginHost.Editor.FLContainer);
                    prog.Run(input, true);

                    Bitmap bmp = prog.GetActiveBitmap();

                    prog.FreeResources();
                    bmp.Save(sfdScript.FileName);
                    bmp.Dispose();
                }
            }
        }


        [ToolbarItem("Run/Debug", 1)]
        private void OnStartDebugger()
        {
            PluginHost.Editor.StartDebugger();
        }

        [ToolbarItem("Run/Parse", 2)]
        private void OnParseDebugger()
        {
            PluginHost.Editor.Parse();
        }

        [ToolbarItem("View/Toggle Toolbar Side", 0)]
        private void OnToggleToolbarSide()
        {
            if (PluginHost.Editor.PanelToolbar.Dock == DockStyle.Left)
            {
                PluginHost.Editor.PanelToolbar.Dock = DockStyle.Right;
            }
            else if (PluginHost.Editor.PanelToolbar.Dock == DockStyle.Right)
            {
                PluginHost.Editor.PanelToolbar.Dock = DockStyle.Left;
            }
        }

        [ToolbarItem("View/Enable Toolbar", 1)]
        private void OnEnableToolbar()
        {
            PluginHost.Editor.PanelToolbar.Visible = true;
        }

        [ToolbarItem("View/Disable Toolbar", 1)]
        private void OnDisableToolbar()
        {
            PluginHost.Editor.PanelToolbar.Visible = false;
        }
        

        [ToolbarItem("Plugins/Remove Packages from Startup List", 1)]
        private void OnRemovePluginsFromStartup()
        {
            if (File.Exists(PluginPaths.PluginListFile))
            {
                File.Delete(PluginPaths.PluginListFile);
                if (MessageBox.Show(
                                    "Cleared File: " +
                                    PluginPaths.PluginListFile +
                                    "\nFor the Changes to take effect, the application has to be Restarted. Do you want to restart?",
                                    "Restart Required",
                                    MessageBoxButtons.YesNo
                                   ) ==
                    DialogResult.Yes)
                {
                    PluginHost.Editor.Close();
                    Application.Restart();
                }
            }
        }

        #endregion
    }
}