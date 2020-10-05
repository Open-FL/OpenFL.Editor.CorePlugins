using System.Collections.Generic;

using OpenFL.Editor.CorePlugins.Forms;
using OpenFL.Editor.Forms.Debug;

using PluginSystem.Core.Pointer;
using PluginSystem.Utility;

namespace OpenFL.Editor.CorePlugins.Setup
{
    public class BufferViewPlugin : APlugin<FLDebuggerWindow>
    {

        private readonly List<BufferView> Views = new List<BufferView>();

        public override void OnLoad(PluginAssemblyPointer ptr)
        {
            base.OnLoad(ptr);

            PluginHost.OnBufferClick += (program, buffer) =>
                                        {
                                            BufferView bvv = new BufferView(
                                                                            program.Instance,
                                                                            buffer,
                                                                            program.Dimensions.x,
                                                                            program.Dimensions.y
                                                                           );
                                            Views.Add(bvv);
                                            bvv.Show();
                                        };

            PluginHost.OnInternalBufferClick += (program, buffer) =>
                                                {
                                                    BufferView bvv = new BufferView(
                                                         program.Instance,
                                                         buffer,
                                                         program.Dimensions.x,
                                                         program.Dimensions.y
                                                        );
                                                    Views.Add(bvv);
                                                    bvv.Show();
                                                };
        }

    }
}