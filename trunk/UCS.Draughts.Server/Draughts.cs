using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace UCS.Draughts.Server
{
    public class Draughts : IDrawable
    {
        #region Attributes and Properties

        private Board _board;
        public Board Board
        {
            get { return this._board; }
            set { this._board = value; }
        }

        private Player _currentPlayer;
        public Player CurrentPlayer
        {
            get { return this._currentPlayer; }
            set { this._currentPlayer = value; }
        }

        private Player _whitePlayer;
        public Player WhitePlayer
        {
            get { return this._whitePlayer; }
        }

        private Player _blackPlayer;
        public Player BlackPlayer
        {
            get { return this._blackPlayer; }
        }

        public Label CurrentPlayerLabel
        {
            get
            {
                string text = this._currentPlayer.Color == PieceColor.Black ? "Pretos" : "Brancos";
                Color color = this._currentPlayer.Color == PieceColor.Black ? Color.Black : Color.White;

                Label label = new Label(text, new Point(500, 50), new Font("Arial", 14), new SolidBrush(color));

                return label;
            }
        }

        public Label WinnerLabel
        {
            get
            {
                string text = this._winner.Color == PieceColor.Black ? "Pretos Venceram!" : "Brancos Venceram!";
                Color color = this._winner.Color == PieceColor.Black ? Color.Black : Color.White;

                Label label = new Label(text, new Point(500, 50), new Font("Arial", 14), new SolidBrush(color));

                return label;
            }
        }

        #endregion

        #region Private Members

        private Player _winner;
        
        #endregion

        #region Constructors

        public Draughts()
        {
            this.Initialize();

            DrawingManager.Instance.DrawableObjects.Add(this);
        }

        #endregion

        #region Initialize

        protected void Initialize()
        {
            this.InitializeBoard();
            this.InitializeRulesValidator();
            this.InitializePlayers();
        }

        private void InitializeBoard()
        {
            this._board = new Board();
        }

        private void InitializeRulesValidator()
        {
            RulesValidator.Draughts = this;
        }

        private void InitializePlayers()
        {
            this._whitePlayer = new Player(PieceColor.White);
            this._blackPlayer = new Player(PieceColor.Black);

            this._currentPlayer = this._whitePlayer;
        }

        #endregion

        #region Update

        public void Update(MouseState mouseState)
        {
            //MouseState mouseState = Mouse.GetState();

            this._board.Update(mouseState);
        }

        #endregion

        #region Draw

        public void Draw(Graphics graphics)
        {
            this._board.Draw(graphics);

            this.DrawCurrentPlayer(graphics);
            this.DrawWinner(graphics);
        }

        private void DrawCurrentPlayer(Graphics graphics)
        {
            if (this._winner != null)
                return;

            this.CurrentPlayerLabel.Draw(graphics);
        }

        private void DrawWinner(Graphics graphics)
        {
            if (this._winner == null)
                return;

            this.WinnerLabel.Draw(graphics);
        }

        #endregion

        #region Public Methods

        public void ChangePlayer()
        {
            if (this._currentPlayer == this._whitePlayer)
                this._currentPlayer = this._blackPlayer;
            else
                this._currentPlayer = this._whitePlayer;

            DrawingManager.Instance.DrawableObjects.Add(this.CurrentPlayerLabel);
        }

        public void SetWinner(Player winner)
        {
            this._winner = winner;

            DrawingManager.Instance.DrawableObjects.Add(this.WinnerLabel);
        }

        #endregion
    }
}
