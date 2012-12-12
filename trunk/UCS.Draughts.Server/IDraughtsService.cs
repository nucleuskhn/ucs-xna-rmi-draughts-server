using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace UCS.Draughts.Server
{
    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IDraughtsServiceCallback))]
    public interface IDraughtsService
    {
        [OperationContract]
        Player LogIn();
    }
}
