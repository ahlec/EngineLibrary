using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EngineLibrary.Graphics;

namespace EngineLibrary.GUI
{
    public class GUIToolbar
    {
        private SpriteFont font;
        public GUIToolbar()
        {
            font = GUIGlobal.ContentManager.Load<SpriteFont>("Fonts/SmallGUIFont");
        }
        private Vector2 _toolbarItemSize = new Vector2(50, 50);
        private Vector2 _portraitSize = new Vector2(80, 80);
        private Vector2 _toolbarItemPadding = new Vector2(10, 15);
        private Vector2 _portraitPadding = new Vector2(10, 10);
        private bool _showToolbar = true;
        public bool ShowToolbar { get { return _showToolbar; } set { _showToolbar = value; } }
        private int opacity = 255;
        public int Opacity { get { return opacity; } set { if (value >= 0 && value <= 255) opacity = value; } }
        private List<GUIToolbarItem> _toolbarItems = new List<GUIToolbarItem>();
        public void AddItem(GUIToolbarItem toolbarItem) { _toolbarItems.Add(toolbarItem); }
        public void AddItemAt(GUIToolbarItem toolbarItem, int index) { _toolbarItems.Insert(index, toolbarItem); }
        public bool ContainsItem(GUIToolbarItem toolbarItem) { return _toolbarItems.Contains(toolbarItem); }
        public bool ContainsItem(string handle)
        {
            AdvancedPredicate<GUIToolbarItem> hasHandle = new AdvancedPredicate<GUIToolbarItem>();
            hasHandle.AddParameter("Handle", handle);
            return _toolbarItems.Exists(hasHandle);
        }
        public void RemoveItem(GUIToolbarItem toolbarItem) { _toolbarItems.Remove(toolbarItem); }
        public void RemoveItemAt(int index) { _toolbarItems.RemoveAt(index); }
        public void RemoveItem(string handle)
        {
            AdvancedPredicate<GUIToolbarItem> hasHandle = new AdvancedPredicate<GUIToolbarItem>();
            hasHandle.AddParameter("Handle", handle);
            List<GUIToolbarItem> foundItems = _toolbarItems.FindAll(hasHandle.Predicate);
            foreach (GUIToolbarItem item in foundItems)
                RemoveItem(item);
            return;
        }
        int prev_opacity = 255;
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!_showToolbar)
                return;

            int opacityToGloballyReduce = 255 - opacity;

            spriteBatch.FillRectangle(0, spriteBatch.GraphicsDevice.Viewport.Height - 40,
                spriteBatch.GraphicsDevice.Viewport.Width, 40,
                new Color(255, 255, 255, (byte)(255 - opacityToGloballyReduce)));
            spriteBatch.DrawLine(0, spriteBatch.GraphicsDevice.Viewport.Height - 40,
                spriteBatch.GraphicsDevice.Viewport.Width, spriteBatch.GraphicsDevice.Viewport.Height - 40,
                new Color(0, 0, 0, (byte)(255 - opacityToGloballyReduce)));

            // Player Portrait
            spriteBatch.FillRectangle(10, spriteBatch.GraphicsDevice.Viewport.Height - 90, 80, 80,
                new Color(Color.Gray, (byte)(255 - opacityToGloballyReduce)));

            // Toolbar Items
            int drawnIndex = 0;
            foreach (GUIToolbarItem item in _toolbarItems)
            {
                if (item.State != GUIToolbarItemState.Hidden)
                {
                    bool isActivated = ((item.State & GUIToolbarItemState.Activated) ==
                        GUIToolbarItemState.Activated);
                    bool hasHover = ((item.State & GUIToolbarItemState.Hover) == GUIToolbarItemState.Hover);

                    Color itemColor = Color.White;
                    if ((item.State & GUIToolbarItemState.Disabled) == GUIToolbarItemState.Disabled)
                        itemColor = Color.Gray;
                    /*if (!hasHover)
                        itemColor.A = 200;*/
                    spriteBatch.Draw(item.Icon, new Rectangle((int)(_portraitSize.X + _portraitPadding.X *
                        2 + drawnIndex * (_toolbarItemPadding.X + _toolbarItemSize.X)),
                        (int)(spriteBatch.GraphicsDevice.Viewport.Height - _toolbarItemPadding.Y -
                        _toolbarItemSize.Y - (isActivated ? 10 : 0)), (int)_toolbarItemSize.X,
                        (int)_toolbarItemSize.Y), itemColor);
                    if ((item.State & GUIToolbarItemState.Hover) == GUIToolbarItemState.Hover)
                    {
                        Color hoverBackgroundColor = Color.White;
                        hoverBackgroundColor.A = 255;
                        Vector2 labelNameLocation = new Vector2(_portraitSize.X +
                            _portraitPadding.X * 2 + drawnIndex * (_toolbarItemPadding.X + _toolbarItemSize.X) -
                            (font.MeasureString(item.Name).X - _toolbarItemSize.X) / 2,
                            spriteBatch.GraphicsDevice.Viewport.Height - _toolbarItemPadding.Y * 1.5f -
                            _toolbarItemSize.Y - (hasHover ? 10 : 0) -
                            font.MeasureString(item.Name).Y);
                        if (labelNameLocation.X < _portraitSize.X + _portraitPadding.X * 2 + 3)
                            labelNameLocation.X = _portraitSize.X + _portraitPadding.X * 2 + 3;
                        spriteBatch.FillRectangle(_portraitSize.X + _portraitPadding.X * 2,
                            labelNameLocation.Y,
                            spriteBatch.GraphicsDevice.Viewport.Width - _portraitSize.X - _portraitPadding.X * 3,
                            font.MeasureString(item.Name).Y, hoverBackgroundColor);
                        spriteBatch.DrawRectangle(_portraitSize.X + _portraitPadding.X * 2,
                            labelNameLocation.Y,
                            spriteBatch.GraphicsDevice.Viewport.Width - _portraitSize.X - _portraitPadding.X * 3,
                            font.MeasureString(item.Name).Y, Color.Black);
                        spriteBatch.DrawString(font, item.Name, labelNameLocation, Color.Black, 0,
                            Vector2.Zero, 1.0f, SpriteEffects.None, 0);
                    }
                    drawnIndex++;
                }
            }
        }
        public void OpenItem(GUIToolbarItem item)
        {
            if ((item.State & GUIToolbarItemState.Activated) == GUIToolbarItemState.Activated)
                return;
            if ((item.State & GUIToolbarItemState.Disabled) == GUIToolbarItemState.Disabled)
                return;

            if (!item.Modal)
                item.State = item.State | GUIToolbarItemState.Activated;
            item.Open();
        }
        public void OpenItem(int index)
        {
            OpenItem(_toolbarItems[index]);
        }
        public void CloseItem(GUIToolbarItem item)
        {
            if ((item.State & GUIToolbarItemState.Activated) != GUIToolbarItemState.Activated)
                return;
            if ((item.State & GUIToolbarItemState.Disabled) == GUIToolbarItemState.Disabled)
                return;

            item.State = item.State & ~GUIToolbarItemState.Activated;
        }
        public void CloseItem(int index)
        {
            CloseItem(_toolbarItems[index]);
        }
        bool hasReleasedLeftMouseButton = true;
        public void Update(GameTime gametime, GraphicsDevice device)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Released)
                hasReleasedLeftMouseButton = true;

            int outputIndex = 0;
            foreach (GUIToolbarItem item in _toolbarItems)
            {
                if (item.State != GUIToolbarItemState.Hidden && item.State != GUIToolbarItemState.Disabled)
                {
                    if (new Rectangle((int)_portraitSize.X + (int)_portraitPadding.X * 2 + outputIndex *
                        (int)(_toolbarItemPadding.X + _toolbarItemSize.X), device.Viewport.Height -
                        (int)_toolbarItemPadding.Y - (int)_toolbarItemSize.Y -
                        ((item.State & GUIToolbarItemState.Activated) == GUIToolbarItemState.Activated ?
                        10 : 0), (int)_toolbarItemSize.X, (int)_toolbarItemSize.Y).Contains(Mouse.GetState().X,
                        Mouse.GetState().Y))
                    {
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && hasReleasedLeftMouseButton)
                            if ((item.State & GUIToolbarItemState.Activated) == GUIToolbarItemState.Activated)
                                CloseItem(item);
                            else
                                OpenItem(item);
                        item.State = item.State | GUIToolbarItemState.Hover;
                    }
                    else if ((item.State & GUIToolbarItemState.Hover) == GUIToolbarItemState.Hover)
                        item.State = item.State & ~GUIToolbarItemState.Hover;
                    outputIndex++;
                }
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && hasReleasedLeftMouseButton)
                hasReleasedLeftMouseButton = false;
        }
    }
}
