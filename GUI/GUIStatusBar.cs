using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using EngineLibrary.Graphics;

namespace EngineLibrary.GUI
{
    public class GUIStatusBar
    {
        public GUIStatusBar(Vector2 position, Vector2 size, SpriteFont font, string label,
            string currentValue, string maxValue, Color baseColor, Color borderColor, Color textColor)
        {
            Position = position;
            Size = size;
            Font = font;
            Label = label;
            CurrentValue = currentValue;
            MaxValue = maxValue;
            BaseColor = baseColor;
            BorderColor = borderColor;
            TextColor = textColor;
        }
        public Color BaseColor { get; set; }
        public Color BorderColor { get; set; }
        public Color TextColor { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public string CurrentValue { get; set; }
        public string MaxValue { get; set; }
        public string Label { get; set; }
        public SpriteFont Font { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.FillRectangle(Position, Size, BaseColor);
            spriteBatch.DrawRectangle(Position, Size, BorderColor);
            spriteBatch.DrawString(Font, Label + ": " + CurrentValue + " / " + MaxValue,
                new Vector2(Position.X + 1, Position.Y + Size.Y - Font.MeasureString(Label + ": " +
                CurrentValue + " / " + MaxValue).Y), TextColor);
        }
    }
}
