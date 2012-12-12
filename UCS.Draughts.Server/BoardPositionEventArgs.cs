using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UCS.Draughts.Server
{
    public class BoardPositionEventArgs : EventArgs
    {
        #region Attributes and Properties

        private BoardPosition _boardPosition;
        public BoardPosition BoardPosition
        {
            get { return this._boardPosition; }
            set { this._boardPosition = value; }
        }

        #endregion

        #region Constructors

        public BoardPositionEventArgs(BoardPosition boardPosition) : base()
        {
            this._boardPosition = boardPosition;
        }

        #endregion
    }
}
