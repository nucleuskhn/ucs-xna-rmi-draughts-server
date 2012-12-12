using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UCS.Draughts.Server
{
    public class RulesValidator
    {
        #region Static Members

        private static Draughts _draughts;
        public static Draughts Draughts
        {
            get { return _draughts; }
            set { _draughts = value; }
        }

        private static RulesValidator _instance;
        public static RulesValidator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new RulesValidator();

                return RulesValidator._instance;
            }
            set { RulesValidator._instance = value; }
        }

        #endregion

        #region Events

        public event EventHandler<PieceEventArgs> PieceUpgraded;

        #endregion

        #region Contructors

        private RulesValidator()
        {
        }

        #endregion

        #region Public Methods

        public bool ValidateNewPosition(Piece piece, Square oldSquare, Square newSquare)
        {
            PieceMovement movement = this.GetValidMovement(piece, oldSquare, newSquare);

            //newSquare.Invalid = movement == null;

            if (movement == null)
                return false;

            return true;
        }

        public void ApplyMovement(Piece piece, Square oldSquare, Square newSquare)
        {
            if (newSquare.Piece != null)
                return;

            PieceMovement movement = this.GetValidMovement(piece, oldSquare, newSquare);

            if (movement == null)
                return;

            oldSquare.Piece = null;
            newSquare.Piece = piece;

            DrawingManager.Instance.DrawableObjects.Add(oldSquare);
            DrawingManager.Instance.DrawableObjects.Add(newSquare);

            piece.Selected = false;

            if (movement.JumpPosition != null)
            {
                BoardPosition killPosition = oldSquare.BoardPosition + movement.JumpPosition;
                Square killSquare = _draughts.Board[killPosition];
                killSquare.Piece = null;

                DrawingManager.Instance.DrawableObjects.Add(killSquare);
            }

            if (piece is CommonPiece && ((piece.BoardPosition.I == 7 && piece.Color == PieceColor.Black) || 
                                         (piece.BoardPosition.I == 0 && piece.Color == PieceColor.White)))
            {
                newSquare.Piece = new QueenPiece(piece.Color, piece.BoardPosition);
                if (this.PieceUpgraded != null)
                    this.PieceUpgraded(this, new PieceEventArgs(newSquare.Piece));

                _draughts.ChangePlayer();
            }
            else
                this.ChangePlayerIfNeeded(piece, movement);

            this.CheckPlayerVictory();
        }

        public bool CheckIfCanMovePiece(Piece piece)
        {
            return piece.Color == _draughts.CurrentPlayer.Color;
        }

        public void UpdateValidSquares(Piece selectedPiece)
        {
            List<PieceMovement> movements = this.GetValidMovements(selectedPiece);
            List<PieceMovement> allPiecesValidMovements = this.GetPlayerValidMovements(_draughts.CurrentPlayer);

            foreach (PieceMovement movement in movements)
            {
                BoardPosition position = selectedPiece.BoardPosition + movement.BoardMovement;
                Square square = _draughts.Board[position];

                PieceMovement newMovement = movements.FirstOrDefault(m => (selectedPiece.BoardPosition + m.BoardMovement).Equals(square.BoardPosition));

                if (newMovement == null)
                    continue;

                if (newMovement.JumpPosition == null)
                {
                    if (allPiecesValidMovements.FirstOrDefault(m => m.JumpPosition != null) != null)
                        continue;
                }
                else if (!(selectedPiece is QueenPiece))
                {
                    if (this.CheckIfPlayerHasQueenJumpMovements())
                        continue;
                }

                square.Valid = true;
                DrawingManager.Instance.DrawableObjects.Add(square);
            }
        }

        #endregion

        #region Private Methods

        private PieceMovement GetValidMovement(Piece piece, Square oldSquare, Square newSquare)
        {
            if (newSquare.Piece != null)
                return null;

            List<PieceMovement> pieceValidMovements = this.GetValidMovements(piece);
            
            PieceMovement newMovement = pieceValidMovements.FirstOrDefault(m => (oldSquare.BoardPosition + m.BoardMovement).Equals(newSquare.BoardPosition));

            if (newMovement == null)
                return null;

            if (newMovement.JumpPosition == null)
            {
                List<PieceMovement> allPiecesValidMovements = this.GetPlayerValidMovements(_draughts.CurrentPlayer);

                return allPiecesValidMovements.FirstOrDefault(m => m.JumpPosition != null) == null ? newMovement : null;
            }
            else if (!(piece is QueenPiece))
            {
                if (this.CheckIfPlayerHasQueenJumpMovements())
                    return null;
            }
            
            return newMovement;
        }

        private List<PieceMovement> GetValidMovements(Piece piece)
        {
            List<PieceMovement> validMovements = new List<PieceMovement>();

            foreach (var movement in piece.Movements)
            {
                Square movementSquare = _draughts.Board[piece.BoardPosition + movement.BoardMovement];

                if (movementSquare != null)
                {
                    if (movementSquare.Piece == null)
                    {
                        if (movement.JumpPosition != null)
                        {
                            Square jumpSquare = _draughts.Board[piece.BoardPosition + movement.JumpPosition];
                            if (jumpSquare != null && jumpSquare.Piece != null && jumpSquare.Piece.Color != piece.Color)
                                validMovements.Add(movement);
                        }
                        else
                            validMovements.Add(movement);
                    }
                }
            }

            return validMovements;
        }

        private List<PieceMovement> GetPlayerValidMovements(Player player)
        {
            List<PieceMovement> validMovements = new List<PieceMovement>();

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece squarePiece = _draughts.Board.Squares[i, j].Piece;

                    if (squarePiece != null && squarePiece.Color == player.Color)
                        validMovements.AddRange(this.GetValidMovements(squarePiece));
                }
            }

            return validMovements;
        }

        private void ChangePlayerIfNeeded(Piece piece, PieceMovement lastMovement)
        {
            List<PieceMovement> validMovements = this.GetPlayerValidMovements(_draughts.CurrentPlayer);

            PieceMovement jumpMovement = validMovements.FirstOrDefault(m => m.JumpPosition != null);
            
            if (jumpMovement == null || lastMovement.JumpPosition == null)
                _draughts.ChangePlayer();
        }

        private bool CheckIfPlayerHasQueenJumpMovements()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece squarePiece = _draughts.Board.Squares[i, j].Piece;

                    if (squarePiece != null && squarePiece.Color == _draughts.CurrentPlayer.Color && squarePiece is QueenPiece && this.GetValidMovements(squarePiece).Count(m => m.JumpPosition != null) > 0)
                        return true;
                }
            }

            return false;
        }

        private void CheckPlayerVictory()
        {
            bool foundWhite = false;
            bool foundBlack = false;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (_draughts.Board.Squares[i, j].Piece != null)
                        if (_draughts.Board.Squares[i, j].Piece.Color == PieceColor.Black)
                            foundBlack = true;
                        else
                            foundWhite = true;
                }
            }

            if (!foundWhite)
            {
                _draughts.SetWinner(_draughts.BlackPlayer);
                return;
            }
            else if (!foundBlack)
            {
                _draughts.SetWinner(_draughts.WhitePlayer);
                return;
            }

            List<PieceMovement> whiteMovements = this.GetPlayerValidMovements(_draughts.WhitePlayer);
            List<PieceMovement> blackMovements = this.GetPlayerValidMovements(_draughts.BlackPlayer);

            if (whiteMovements.Count == 0)
                _draughts.SetWinner(_draughts.BlackPlayer);
            else if (blackMovements.Count == 0)
                _draughts.SetWinner(_draughts.WhitePlayer);
        }

        #endregion
    }
}
