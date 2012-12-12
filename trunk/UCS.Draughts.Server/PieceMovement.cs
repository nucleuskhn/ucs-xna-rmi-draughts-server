using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UCS.Draughts.Server
{
    [DataContractAttribute]
    public class PieceMovement
    {
        #region Attributes and Properties

        private BoardPosition _boardMovement;
        [DataMemberAttribute]
        public BoardPosition BoardMovement
        {
            get { return _boardMovement; }
        }

        private BoardPosition _jumpPosition;
        [DataMemberAttribute]
        public BoardPosition JumpPosition
        {
            get { return _jumpPosition; }
        }

        #endregion

        #region Constructors

        public PieceMovement(BoardPosition boardMovement)
        {
            this._boardMovement = boardMovement;
            this._jumpPosition = null;
        }

        public PieceMovement(BoardPosition boardMovement, BoardPosition jumpPosition)
        {
            this._boardMovement = boardMovement;
            this._jumpPosition = jumpPosition;
        }

        #endregion
    }
}
