using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ShapesNLP
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface INLPProcessor
    {
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Shapes", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ShapeDetails EvaluateShapeDetails(string data);

        // TODO: Add your service operations here  
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.  
    [DataContract]
    public class ShapeDetails
    {
        [DataMember]
        public string ShapeType { get; set; }

        [DataMember]
        public Dictionary<string, string> Dimensions { get; set; }

        [DataMember]
        public string Error { get; set; }
    }
}
