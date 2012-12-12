using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web;

namespace UCS.Draughts.Server
{
    public class DraughtsManager
    {
        private static DraughtsManager _instance;
        public static DraughtsManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DraughtsManager();

                return DraughtsManager._instance;
            }
        }

        private readonly List<IDraughtsServiceCallback> _subscribers = new List<IDraughtsServiceCallback>();

        private Draughts _draughts;
        private Player _blackPlayer;
        private Player _whitePlayer;
        private bool _canInitialize = false;

        private DraughtsManager()
        {
            Thread thread = new Thread(this.InitializeDraughts);
            thread.Start();
        }

        public Player GetPlayer()
        {
            try
            {
                IDraughtsServiceCallback callback = OperationContext.Current.GetCallbackChannel<IDraughtsServiceCallback>();
                if (!_subscribers.Contains(callback))
                    _subscribers.Add(callback);
            }
            catch
            {
            }

            if (this._blackPlayer == null)
            {
                this._blackPlayer = new Player(PieceColor.Black);
                return this._blackPlayer;
            }

            if (this._whitePlayer == null)
            {
                this._whitePlayer = new Player(PieceColor.White);

                this._canInitialize = true;

                return this._whitePlayer;
            }

            return null;
        }

        public void SendInitializeCommonPiece(BoardPosition position, PieceColor color)
        {
            foreach (var subscriber in this._subscribers)
            {
                subscriber.InitializeCommonPiece(position, color);
            }
        }

        private void InitializeDraughts()
        {
            while (true)
            {
                if (this._draughts == null && this._canInitialize)
                    this._draughts = new Draughts();
                else
                    Thread.Sleep(1000);
            }
        }
    }
}