using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using OpenCL.Wrapper;
using OpenCL.Wrapper.TypeEnums;

using OpenFL.Core;
using OpenFL.Core.Buffers.BufferCreators;
using OpenFL.Core.Instructions.InstructionCreators;
using OpenFL.Editor.CorePlugins.Forms;
using OpenFL.Editor.Forms;
using OpenFL.Parsing;

using PluginSystem.Core;
using PluginSystem.Core.Pointer;
using PluginSystem.Utility;

using Utility.Exceptions;
using Utility.ExtPP.API;
using Utility.IO.Callbacks;

namespace OpenFL.Editor.CorePlugins.Setup
{
    public class CLInitializerPlugin : APlugin<PluginSystemHost>
    {

        public override string Name => "fl-editor-cl-initializer";

        public override void OnLoad(PluginAssemblyPointer ptr)
        {
            StartupSequence.CustomStartupActions += Initialize;
        }

        private void Initialize()
        {
            CLAPI instance = CLAPI.GetInstance();
            string path = FLScriptEditor.Settings.KernelPath;
            StartupSequence.loaderForm.SetStatus("Discovering Files in Path: " + path);
            string[] files = IOManager.DirectoryExists(path) ? IOManager.GetFiles(path, "*.cl") : new string[0];

            if (files.Length == 0)
            {
                DialogResult res = MessageBox.Show(
                                                   "No Files found at path: " + path,
                                                   "Error",
                                                   MessageBoxButtons.AbortRetryIgnore,
                                                   MessageBoxIcon.Error
                                                  );
                if (res == DialogResult.Retry)
                {
                    Initialize();
                    return;
                }

                if (res == DialogResult.Abort)
                {
                    StartupSequence.loaderForm.DialogResult = DialogResult.Abort;
                    StartupSequence.loaderForm.Close();
                    return;
                }

                if (res == DialogResult.Ignore)
                {
                    StartupSequence.loaderForm.DialogResult = DialogResult.OK;
                }
            }

            KernelDatabase dataBase = new KernelDatabase(DataVectorTypes.Uchar1);
            List<CLProgramBuildResult> results = new List<CLProgramBuildResult>();
            bool throwEx = false;
            int kernelCount = 0;
            int fileCount = 0;
            if (FLScriptEditor.Settings.ExperimentalKernelLoading)
            {
                try
                {
                    string source = TextProcessorAPI.PreprocessSource(files, new Dictionary<string, bool>());
                    CLProgram prog = dataBase.AddProgram(instance, source, "./", false, out CLProgramBuildResult res);
                    throwEx |= !res;
                    if (res)
                    {
                        kernelCount += prog.ContainedKernels.Count;
                    }

                    results.Add(res);
                    StartupSequence.loaderForm.SetStatus($"File Loaded(Kernels Loaded): ({kernelCount})");
                }
                catch (Exception e)
                {
                    throw new SoftException(e);
                }
            }
            else
            {
                foreach (string file in files)
                {
                    StartupSequence.loaderForm.SetStatus($"[{fileCount}/{files.Length}]Loading: {file} ({kernelCount})");
                    try
                    {
                        CLProgram prog = dataBase.AddProgram(instance, file, false, out CLProgramBuildResult res);
                        kernelCount += prog.ContainedKernels.Count;
                        throwEx |= !res;
                        results.Add(res);
                    }
                    catch (Exception e)
                    {
                        StartupSequence.loaderForm.Log("ERROR: " + e.Message, Color.Red);

                        throw e; //Let the Exception Viewer Catch that
                    }

                    fileCount++;
                }
            }


            StartupSequence.loaderForm.SetStatus("Loading Finished");
            StartupSequence.loaderForm.Log("Loading Finished", Color.White);
            StartupSequence.loaderForm.Log("Kernels Loaded: " + kernelCount, Color.White);

            if (throwEx)
            {
                DialogResult res =
                    MessageBox.Show(
                                    "There are errors in one or more OpenCL kernels. Do you want to open the OpenCL Build Excepion Viewer?",
                                    "OpenCL Build Errors",
                                    MessageBoxButtons.YesNoCancel,
                                    MessageBoxIcon.Warning
                                   );
                if (res == DialogResult.Cancel)
                {
                    StartupSequence.loaderForm.DialogResult = DialogResult.Abort;
                    StartupSequence.loaderForm.Close();
                }
                else if (res == DialogResult.Yes)
                {
                    BuildExceptionViewer bvr = new BuildExceptionViewer(new CLBuildException(results));
                    if (bvr.ShowDialog() == DialogResult.Retry)
                    {
                        dataBase.Dispose();
                        Initialize();
                    }
                }
            }

            FLInstructionSet iset = FLInstructionSet.CreateWithBuiltInTypes(dataBase);
            BufferCreator creator = BufferCreator.CreateWithBuiltInTypes();
            FLParser parser = new FLParser(iset, creator, new WorkItemRunnerSettings(true, 2));

            StartupSequence.FlContainer = new FLDataContainer(instance, iset, creator, parser);
        }

    }
}