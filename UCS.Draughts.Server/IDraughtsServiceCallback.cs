using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace UCS.Draughts.Server
{
    public interface IDraughtsServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void InitializeCommonPiece(BoardPosition position, PieceColor color);
    }
}
