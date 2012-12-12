using System;
using System.Collections.Generic;
using System.Drawing;

namespace UCS.Draughts.Server
{
    public class QueenPiece : Piece
    {
        #region Constructors

        public QueenPiece(PieceColor color, BoardPosition boardPosition)
        {
            this._color = color;
            this._boardPosition = boardPosition;
            this._blackImage = Image.FromFile(@"E:\Projetos\C#\ucs-xna-rmi-draughts\UCS.XNA.Draughts\UCS.Forms.Draughts\Resources\BlackQueen.png");
            this._whiteImage = Image.FromFile(@"E:\Projetos\C#\ucs-xna-rmi-draughts\UCS.XNA.Draughts\UCS.Forms.Draughts\Resources\WhiteQueen.png");

            this.InitializeValidMovementsList();
        }

        #endregion

        #region Private Methods

        private void InitializeValidMovementsList()
        {
            this._movements = new List<PieceMovement>();

            this._movements.Add(new PieceMovement(new BoardPosition(1, 1)));
            this._movements.Add(new PieceMovement(new BoardPosition(1, -1)));
            this._movements.Add(new PieceMovement(new BoardPosition(2, 2), new BoardPosition(1, 1)));
            this._movements.Add(new PieceMovement(new BoardPosition(2, -2), new BoardPosition(1, -1)));
            this._movements.Add(new PieceMovement(new BoardPosition(-1, 1)));
            this._movements.Add(new PieceMovement(new BoardPosition(-1, -1)));
            this._movements.Add(new PieceMovement(new BoardPosition(-2, 2), new BoardPosition(-1, 1)));
            this._movements.Add(new PieceMovement(new BoardPosition(-2, -2), new BoardPosition(-1, -1)));
        }

        #endregion
    }
}
