using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HealthSearch
{
    [ServiceContract]
    public interface IHealthSearchRestService
    {
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "teste")]
        void teste();
    }
}
