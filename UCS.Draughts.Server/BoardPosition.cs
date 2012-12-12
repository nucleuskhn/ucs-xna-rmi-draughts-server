using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UCS.Draughts.Server
{
    [DataContractAttribute]
    public class BoardPosition
    {
        #region Attributes and Properties

        private int _i;
        [DataMemberAttribute]
        public int I
        {
          get { return this._i; }
          set { this._i = value; }
        }

        private int _j;
        [DataMemberAttribute]
        public int J
        {
            get { return this._j; }
            set { this._j = value; }
        }

        #endregion

        #region Constructors

        public BoardPosition(int i, int j)
        {
            this._i = i;
            this._j = j;
        }

        #endregion

        #region Overriden Methods

        public override bool Equals(object obj)
        {
            BoardPosition boardPosition = obj as BoardPosition;
            return this.I == boardPosition.I &&
                   this.J == boardPosition.J;
        }

        #endregion

        #region Operators

        public static BoardPosition operator +(BoardPosition boardPosition1, BoardPosition boardPosition2)
        {
            return new BoardPosition(boardPosition1.I + boardPosition2.I, boardPosition1.J + boardPosition2.J);
        }

        #endregion
    }
}
