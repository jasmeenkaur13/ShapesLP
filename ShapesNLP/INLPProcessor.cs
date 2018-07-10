using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace ShapesNLP
{
    /// <summary>
    /// Interface for REST WCF service
    /// </summary>
    [ServiceContract]
    public interface INLPProcessor
    {
        /// <summary>
        /// Method to Post the string and get the result back after processing
        /// </summary>
        /// <param name="data">input passed </param>
        /// <returns>processed output</returns>
        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Shapes", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        ShapeDetails EvaluateShapeDetails(string data);

        // TODO: Add your service operations here  
    }

    /// <summary>
    /// Data contract containing the details to be returned
    /// </summary>
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
