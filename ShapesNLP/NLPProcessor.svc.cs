using System;
using System.ServiceModel.Web;

namespace ShapesNLP
{
    /// <summary>
    /// Service to implement the Interface
    /// </summary>
    public class NLPProcessor : INLPProcessor
    {
        /// <summary>
        /// Method to process the input and return processed output
        /// </summary>
        /// <param name="data"> inputed text</param>
        /// <returns>Shape details or error if any</returns>
        public ShapeDetails EvaluateShapeDetails(string data)
        {
            ShapeDetails shapeDetails = Processor.ShapeNLPProcessor.ProcessInput(data);
            if (!(String.IsNullOrWhiteSpace(shapeDetails.Error)))
            {
                throw new WebFaultException<string>(shapeDetails.Error, System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                return shapeDetails;
            }
        }
    }
}
