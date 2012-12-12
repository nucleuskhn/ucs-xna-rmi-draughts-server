using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;

namespace UCS.Draughts.Server
{
    [DataContractAttribute]
    public abstract class Piece : IDrawable
    {
        #region Constants

        private const int WIDTH = 44;
        private const int HEIGHT = 44;

        #endregion

        #region Attributes and Properties

        protected List<PieceMovement> _movements;
        [DataMemberAttribute]
        public List<PieceMovement> Movements
        {
            get { return this._movements; }
        }

        protected BoardPosition _boardPosition;
        [DataMemberAttribute]
        public BoardPosition BoardPosition
        {
            get { return this._boardPosition; }
            set
            {
                this._boardPosition = value;

                this.ResetPosition();
            }
        }

        protected Vector2 _position;
        [DataMemberAttribute]
        public Vector2 Position
        {
            get { return this._position; }
            set
            {
                this._position = value;

                if (this._position != null)
                    this._bounds = new Rectangle(Convert.ToInt32(this._position.X), Convert.ToInt32(this._position.Y), WIDTH, HEIGHT);
            }
        }

        protected PieceColor _color;
        [DataMemberAttribute]
        public PieceColor Color
        {
            get { return this._color; }
        }

        private bool _selected;
        [DataMemberAttribute]
        public bool Selected
        {
            get { return this._selected; }
            set { this._selected = value; }
        }

        #endregion

        #region Protected Members

        protected Image _blackImage;
        protected Image _whiteImage;
        protected Rectangle _bounds;
        protected MouseState _lastMouseState;

        #endregion

        #region Events

        public event EventHandler<CancelEventArgs> Selecting;
        
        #endregion

        #region Private Methods

        private void CheckAndExecuteSelection(MouseState mouseState)
        {
            if (this._lastMouseState == null)
                return;

            if (mouseState.LeftButton == ButtonState.Released && this._lastMouseState.LeftButton == ButtonState.Pressed &&
                mouseState.X >= this._bounds.Left && mouseState.X <= this._bounds.Right &&
                mouseState.Y >= this._bounds.Top && mouseState.Y <= this._bounds.Bottom)
            {
                CancelEventArgs e = new CancelEventArgs();
                if (this.Selecting != null)
                    this.Selecting(this, e);

                if (!e.Cancel)
                    this._selected = true;
            }

            this._lastMouseState = mouseState;
        }

        private void ResetPosition()
        {
            this.Position = new Vector2(this._boardPosition.J * 58 + 8, this._boardPosition.I * 58 + 8);
        }

        #endregion

        #region Public Methods

        public void Update(MouseState mouseState)
        {
            this.CheckAndExecuteSelection(mouseState);

            this._lastMouseState = mouseState;
        }

        public void Draw(Graphics graphics)
        {
            //this._spriteBatch.Draw(this._texture, this._position, null, Microsoft.Xna.Framework.Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, this._mouseDragOffset.HasValue ? 0.1f : 0.3f);

            graphics.DrawImage(this._color == PieceColor.Black ? this._blackImage : this._whiteImage, new PointF(this._position.X, this._position.Y));
        }

        #endregion
    }
}
