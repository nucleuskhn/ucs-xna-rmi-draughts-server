using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace UCS.Draughts.Server
{
    [DataContractAttribute]
    public class Vector2
    {
        #region Attributes and Properties

        private float _x;
        [DataMemberAttribute]
        public float X
        {
            get { return this._x; }
            set { this._x = value; }
        }

        private float _y;
        [DataMemberAttribute]
        public float Y
        {
            get { return this._y; }
            set { this._y = value; }
        }

        #endregion

        #region Constructors

        public Vector2()
        {
            this._x = 0f;
            this._y = 0f;
        }

        public Vector2(float x, float y)
        {
            this._x = x;
            this._y = y;
        }

        #endregion

        #region Operators

        public static Vector2 operator +(Vector2 pointA, Vector2 pointB)
        {
            return new Vector2(pointA.X + pointB.X, pointA.Y + pointB.Y);
        }

        public static Vector2 operator -(Vector2 pointA, Vector2 pointB)
        {
            return new Vector2(pointA.X - pointB.X, pointA.Y - pointB.Y);
        }

        #endregion
    }
}
