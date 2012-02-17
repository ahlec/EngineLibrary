using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using EngineLibrary.Graphics;

namespace EngineLibrary.GUI
{
    public class GUIWindow
    {
        public GUIWindow()
        {
            _position = new Vector2(10, 40);
        }
        protected Vector2 _position, _size;
        public string Handle { get; set; }
        [ContentSerializerIgnore]
        public Vector2 Position { get { return _position; } set { _position = value; } }
        public float X { get { return _position.X; } set { _position.X = value; } }
        public float Y { get { return _position.Y; } set { _position.Y = value; } }
        [ContentSerializerIgnore]
        public Vector2 Size { get { return _size; } set { _size = value; } }
        public float Width { get { return _size.X; } set { _size.X = value; } }
        public float Height { get { return _size.Y; } set { _size.Y = value; } }
        [ContentSerializerIgnore]
        public bool HasFocus { get; set; }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            Color backColor = Color.BurlyWood;
            backColor.A = 150;
            spriteBatch.FillRectangle(0, 0, spriteBatch.GraphicsDevice.Viewport.Width,
                spriteBatch.GraphicsDevice.Viewport.Height, backColor);
            spriteBatch.FillRectangle(_position, _size, Color.White);
            spriteBatch.DrawRectangle(_position, _size, Color.White, 5);
            spriteBatch.FillRectangle(new Vector2(300, 300), new Vector2(100, 100),
                (HasFocus ? Color.Blue : Color.Green));
        }
        public virtual bool ProcessMouseInput(GameTime gameTime)
        {
            if (new Rectangle(300, 300, 100, 100).Contains(new Point(Mouse.GetState().X,
                Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                HasFocus = true;
                return false;
            }
            else
                HasFocus = false;
            return true;
        }
    }
}
