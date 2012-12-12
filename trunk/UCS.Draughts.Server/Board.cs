using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace UCS.Draughts.Server
{
    public class Board : IDrawable
    {
        #region Attributes and Properties

        private Square[,] _squares;
        public Square[,] Squares
        {
            get { return _squares; }
        }

        #endregion

        #region Private Members

        private Piece _selectedPiece;
        private MouseState _lastMouseState;

        #endregion

        #region Indexer

        public Square this[BoardPosition boardPosition]
        {
            get
            {
                if (boardPosition.I >= 0 && boardPosition.J >= 0 && boardPosition.I < 8 && boardPosition.J < 8)
                    return this._squares[boardPosition.I, boardPosition.J];

                return null;
            }
        }

        #endregion

        #region Constructors

        public Board()
        {
            RulesValidator.Instance.PieceUpgraded += this.RulesValidatorPieceUpgraded;

            this.InitializeSquares();
            this.InitializePieces();
        }

        #endregion

        #region Private Methods

        private void InitializeSquares()
        {
            this._squares = new Square[8, 8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    BoardPosition position = new BoardPosition(i, j);
                    SquareType type = (i + j) % 2 == 0 ? SquareType.White : SquareType.Black;

                    this._squares[i, j] = new Square(type, position);
                }
            }
        }

        private void InitializePieces()
        {
            if (this._squares == null)
                return;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Square square = this._squares[i, j];
                    CommonPiece piece = null;
                    if (square.Type == SquareType.Black)
                        piece = new CommonPiece(PieceColor.Black, square.BoardPosition);
                    else
                    {
                        square = this._squares[i + 5, j];
                        piece = new CommonPiece(PieceColor.White, square.BoardPosition);
                    }

                    piece.Selecting += this.PieceSelecting;

                    square.Piece = piece;

                    DraughtsManager.Instance.SendInitializeCommonPiece(piece.BoardPosition, piece.Color);
                }
            }
        }

        private void CheckPieceMovement(MouseState mouseState)
        {
            if (this._selectedPiece == null || this._lastMouseState == null)
                return;

            if (this._lastMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
            {
                int j = (mouseState.X + 2) / 58;
                int i = (mouseState.Y + 2) / 58;
                BoardPosition position = new BoardPosition(i, j);

                if (position.I < 8 && position.J < 8 && position.I >= 0 && position.J >= 0)
                {
                    Square originalSquare = this._squares[this._selectedPiece.BoardPosition.I, this._selectedPiece.BoardPosition.J];
                    Square hoveringSquare = this._squares[position.I, position.J];

                    bool valid = RulesValidator.Instance.ValidateNewPosition(this._selectedPiece, originalSquare, hoveringSquare);
                    if (valid)
                    {
                        for (i = 0; i < 8; i++)
                            for (j = 0; j < 8; j++)
                                if (this._squares[i, j].Valid)
                                {
                                    this._squares[i, j].Valid = false;
                                    DrawingManager.Instance.DrawableObjects.Add(this._squares[i, j]);
                                }

                        RulesValidator.Instance.ApplyMovement(this._selectedPiece, originalSquare, hoveringSquare);
                    }
                }
            }
        }

        private void UpdateSquares(MouseState mouseState)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    this._squares[i, j].Update(mouseState);
        }

        private void DrawBackground(Graphics graphics)
        {
            graphics.DrawRectangle(Pens.Black, new Rectangle(0, 0, 466, 466));
        }

        private void DrawSquares(Graphics graphics)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    this._squares[i, j].Draw(graphics);
        }

        #endregion

        #region Signed Events Methods

        private void PieceSelecting(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (this._squares[i, j].Valid)
                    {
                        this._squares[i, j].Valid = false;
                        DrawingManager.Instance.DrawableObjects.Add(this._squares[i, j]);
                    }

            Piece piece = sender as Piece;
            if (RulesValidator.Instance.CheckIfCanMovePiece(piece))
            {
                this._selectedPiece = piece;
                RulesValidator.Instance.UpdateValidSquares(piece);
            }
            else
            {
                this._selectedPiece = null;
                e.Cancel = true;
            }
        }

        private void RulesValidatorPieceUpgraded(object sender, PieceEventArgs e)
        {
            e.Piece.Selecting += this.PieceSelecting;
        }

        #endregion

        #region Public Methods

        public void Update(MouseState mouseState)
        {
            this.UpdateSquares(mouseState);
            this.CheckPieceMovement(mouseState);

            this._lastMouseState = mouseState;
        }

        public void Draw(Graphics graphics)
        {
            this.DrawBackground(graphics);

            this.DrawSquares(graphics);
        }

        #endregion
    }
}
