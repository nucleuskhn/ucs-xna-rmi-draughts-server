using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UCS.Draughts.Server
{
    public class DraughtsService : IDraughtsService
    {

        public IDraughtsServiceCallback CallBack
        {
            get { return OperationContext.Current.GetCallbackChannel<IDraughtsServiceCallback>(); }
        }

        public Player LogIn()
        {
            return DraughtsManager.Instance.GetPlayer();;
        }
    }
}
