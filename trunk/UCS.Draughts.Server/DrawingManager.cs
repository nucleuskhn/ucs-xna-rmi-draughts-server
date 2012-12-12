using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UCS.Draughts.Server
{
    public class DrawingManager
    {
        #region Attributes and Properties

        private static DrawingManager _instance;
        public static DrawingManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DrawingManager();

                return _instance;
            }
        }

        List<IDrawable> _drawableObjects;
        public List<IDrawable> DrawableObjects
        {
            get { return this._drawableObjects; }
            set { this._drawableObjects = value; }
        }

        #endregion

        #region Constructors

        private DrawingManager()
        {
            this._drawableObjects = new List<IDrawable>();
        }

        #endregion

        #region Public Methods

        public void DrawObjects(Graphics graphics)
        {
            foreach (IDrawable drawable in this._drawableObjects)
                drawable.Draw(graphics);

            this._drawableObjects.Clear();
        }

        #endregion
    }
}
