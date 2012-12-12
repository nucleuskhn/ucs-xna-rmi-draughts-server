using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace UCS.Draughts.Server
{
    public interface IDrawable
    {
        void Draw(Graphics graphics);
    }
}
