using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace UCS.Draughts.Server
{
    [KnownType(typeof(Piece))]
    [DataContractAttribute]
    public class CommonPiece : Piece
    {
        #region Constructors

        public CommonPiece(PieceColor color, BoardPosition boardPosition)
        {
            this._color = color;
            this._boardPosition = boardPosition;
            this._blackImage = Image.FromFile(@"E:\Projetos\C#\ucs-xna-rmi-draughts\UCS.XNA.Draughts\UCS.Forms.Draughts\Resources\BlackCommon.png");
            this._whiteImage = Image.FromFile(@"E:\Projetos\C#\ucs-xna-rmi-draughts\UCS.XNA.Draughts\UCS.Forms.Draughts\Resources\WhiteCommon.png");

            this.InitializeValidMovementsList();
        }

        #endregion

        #region Private Methods

        private void InitializeValidMovementsList()
        {
            this._movements = new List<PieceMovement>();
            if (this._color == PieceColor.Black)
            {
                this._movements.Add(new PieceMovement(new BoardPosition(1, 1)));
                this._movements.Add(new PieceMovement(new BoardPosition(1, -1)));
                this._movements.Add(new PieceMovement(new BoardPosition(2, 2), new BoardPosition(1, 1)));
                this._movements.Add(new PieceMovement(new BoardPosition(2, -2), new BoardPosition(1, -1)));
            }
            else
            {
                this._movements.Add(new PieceMovement(new BoardPosition(-1, 1)));
                this._movements.Add(new PieceMovement(new BoardPosition(-1, -1)));
                this._movements.Add(new PieceMovement(new BoardPosition(-2, 2), new BoardPosition(-1, 1)));
                this._movements.Add(new PieceMovement(new BoardPosition(-2, -2), new BoardPosition(-1, -1)));
            }
        }

        #endregion
    }
}
