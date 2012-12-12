using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UCS.Draughts.Server
{
    public class PieceEventArgs : EventArgs
    {
        #region Attribues and Properties

        private Piece _piece;
        public Piece Piece
        {
            get { return this._piece; }
            set { this._piece = value; }
        }

        #endregion

        #region Constructors

        public PieceEventArgs(Piece piece)
        {
            this._piece = piece;
        }

        #endregion
    }
}
