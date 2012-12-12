using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UCS.Draughts.Server
{
    public class MouseState
    {
        #region Attributes and Properties

        private int x;
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        private int y;
        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }

        private ButtonState _leftButton;
        public ButtonState LeftButton
        {
            get { return this._leftButton; }
            set { this._leftButton = value; }
        }

        private ButtonState _rightButton;
        public ButtonState RightButton
        {
            get { return this._rightButton; }
            set { this._rightButton = value; }
        }

        #endregion
    }
}
