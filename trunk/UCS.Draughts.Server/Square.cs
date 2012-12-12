using System.Drawing;

namespace UCS.Draughts.Server
{
    public class Square : IDrawable
    {
        #region Attributes and Properties

        private SquareType _type;
        public SquareType Type
        {
            get { return this._type; }
            set { this._type = value; }
        }

        private Piece _piece;
        public Piece Piece
        {
            get { return this._piece; }
            set
            {
                this._piece = value;

                if (this._piece != null)
                    this._piece.BoardPosition = this.BoardPosition;                 
            }
        }

        private BoardPosition _boardPosition;
        public BoardPosition BoardPosition
        {
            get { return this._boardPosition; }
            set { this._boardPosition = value; }
        }

        private bool _valid = false;
        public bool Valid
        {
            get { return this._valid; }
            set { this._valid = value; }
        }

        private bool _invalid = false;
        public bool Invalid
        {
            get { return this._invalid; }
            set { this._invalid = value; }
        }

        #endregion

        #region Constructors

        public Square(SquareType type, BoardPosition boardPosition)
        {
            this._type = type;
            this._boardPosition = boardPosition;
        }

        #endregion

        #region Private Methods

        private void DrawPiece(Graphics graphics)
        {
            if (this._piece != null)
                this._piece.Draw(graphics);
        }

        #endregion

        #region Public Methods

        public void Update(MouseState mouseState)
        {
            if (this._piece != null)
                this._piece.Update(mouseState);
        }

        public void Draw(Graphics graphics)
        {
            int x = this._boardPosition.J * 58 + 2;
            int y = this._boardPosition.I * 58 + 2;

            graphics.DrawImage(this._type == SquareType.Black ? Image.FromFile(@"E:\Projetos\C#\ucs-xna-rmi-draughts\UCS.XNA.Draughts\UCS.Forms.Draughts\Resources\BlackSquare.png") : Image.FromFile(@"E:\Projetos\C#\ucs-xna-rmi-draughts\UCS.XNA.Draughts\UCS.Forms.Draughts\Resources\WhiteSquare.png"), new Point(x, y));

            if (this._valid)
                graphics.DrawImage(Image.FromFile(@"E:\Projetos\C#\ucs-xna-rmi-draughts\UCS.XNA.Draughts\UCS.Forms.Draughts\Resources\GreenSquare.png"), new Point(x, y));

            if (this._invalid)
                graphics.DrawImage(Image.FromFile(@"E:\Projetos\C#\ucs-xna-rmi-draughts\UCS.XNA.Draughts\UCS.Forms.Draughts\Resources\RedSquare.png"), new Point(x, y));

            this.DrawPiece(graphics);
        }

        #endregion
    }
}
