using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using EngineLibrary.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Graphics;
using Microsoft.Xna.Framework.Content.Pipeline.Processors;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace EngineLibrary.GUI
{
    public enum GUIToolbarItemState
    {
        Normal = 0,
        Hover = 1,
        Activated = 2,
        Disabled = 4,
        HasUpdate = 5,
        Hidden = 6
    }
    public class GUIToolbarItem
    {
        public GUIToolbarItem()
        {
        }
        public string Handle { get; set; }
        public string Name { get; set; }
        [ContentSerializerIgnore]
        public GUIToolbarItemState State { get; set; }
        public bool Modal { get; set; }
        [ContentSerializerIgnore]
        public Texture2D Icon { get; set; }

        protected GUIWindow _window;
        public void SetWindow(GUIWindow window) { _window = window; }
        public virtual void Open()
        {
        }
    }

    [ContentTypeWriter]
    public class GUIToolbarItemWriter : ContentTypeWriter<GUIToolbarItem>
    {
        protected override void Write(ContentWriter output, GUIToolbarItem value)
        {
            output.Write(value.Handle);
            output.Write(value.Name);
            output.Write(value.Modal);
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return typeof(GUIToolbarItemReader).AssemblyQualifiedName;
        }
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return base.GetRuntimeType(targetPlatform);
        }
    }

    public class GUIToolbarItemReader : ContentTypeReader<GUIToolbarItem>
    {
        protected override GUIToolbarItem Read(ContentReader input, GUIToolbarItem existingInstance)
        {
            GUIToolbarItem toolbarItem = new GUIToolbarItem();
            toolbarItem.Handle = input.ReadString();
            toolbarItem.Name = input.ReadString();
            toolbarItem.Modal = input.ReadBoolean();
            toolbarItem.Icon = GUIGlobal.ContentManager.Load<Texture2D>(@"ToolbarItems\" +
                toolbarItem.Handle + "Icon");
            toolbarItem.SetWindow(GUIGlobal.ContentManager.Load<GUIWindow>(@"Windows\Map"));
            return toolbarItem;
        }
    }
}
