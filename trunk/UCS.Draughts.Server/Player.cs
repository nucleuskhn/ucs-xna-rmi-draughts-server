using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UCS.Draughts.Server
{
    [DataContractAttribute]
    public class Player
    {
        #region Attributes and Properties

        private PieceColor _color;
        [DataMemberAttribute]
        public PieceColor Color
        {
            get { return this._color; }
            set { this._color = value; }
        }

        #endregion

        #region Constructors

        public Player(PieceColor color)
        {
            this._color = color;
        }

        #endregion
    }
}
