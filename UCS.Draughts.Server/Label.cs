using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UCS.Draughts.Server
{
    public class Label : IDrawable
    {
        #region Attributes and Properties

        private string _text;
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        private Point _position;
        public Point Position
        {
            get { return this._position; }
            set { this._position = value; }
        }

        private Font _font;
        public Font Font
        {
            get { return this._font; }
            set { this._font = value; }
        }

        private Brush _brush;
        public Brush Brush
        {
            get { return this._brush; }
            set { this._brush = value; }
        }

        private Color _backgroundColor;
        public Color BackgroundColor
        {
            get { return this._backgroundColor; }
            set { this._backgroundColor = value; }
        }

        #endregion

        #region Constructors

        public Label(string text, Point position, Font font, Brush brush)
        {
            this._text = text;
            this._position = position;
            this._font = font;
            this._brush = brush;
            this._backgroundColor = Color.Blue;
        }

        #endregion

        #region Public Methods

        public void Draw(Graphics graphics)
        {
            SizeF textSize = graphics.MeasureString(this._text, this._font);

            graphics.FillRectangle(new SolidBrush(this._backgroundColor), new Rectangle(this._position.X, this._position.Y, 170, (int)textSize.Height));

            graphics.DrawString(this._text, this._font, this._brush, this._position);
        }

        #endregion
    }
}
