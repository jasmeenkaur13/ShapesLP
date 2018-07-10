using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShapesNLP.Processor
{
    /// <summary>
    /// Class to Process the Input of below mentioned structure
    /// "Draw a(n) <shape> with a(n) <measurement> of <amount> (and a(n) <measurement> of <amount>)"
    /// </summary>
    public static class ShapeNLPProcessor
    {
        static readonly List<string> ignoreIdentifiers = new List<string> {"a", "an" };
        static readonly List<string> supportedShapes = new List<string> {"square", "circle", "oval", "rectangle", "triangle", "parallelogram", "hexagon", "pentagon", "heptagon", "octagon" };
        static readonly List<string> supportedDimensions = new List<string> {"height", "width", "length", "radius", "breadth" };
        static readonly string state4IgnoreIdentifier = "of";
        static readonly List<string> state3IgnoreIdentifier = new List<string> { "with" , "side"};
        static readonly string state3ReachableIdentifier = "and";
        static readonly string startIdentifier = "draw";
        static readonly string errorMessage = "String does not matches the Format. Please refer format specified below.";

        /// <summary>
        /// Functions processes the input
        /// </summary>
        /// <param name="inputReceived">Input to be processed </param>
        /// <returns>details of the processing and result</returns>
        public static ShapeDetails ProcessInput(string inputReceived) {
            ShapeDetails initializeShape = new ShapeDetails
            {
                Dimensions = new Dictionary<string, string>(),
                Error = ""
            };
            int state = 1;
            decimal amount = 0.0m;
            string dimensionName = "";
            String[] wordsParsed = inputReceived.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < wordsParsed.Length; i++)
            {
                switch (state)
                {
                    case 1: if (startIdentifier.Equals(wordsParsed[i].ToLower(), StringComparison.OrdinalIgnoreCase))
                        { state = 2; }
                        else { state = 6;
                        }
                        break;
                    case 2: if (ignoreIdentifiers.Any(identifier => identifier.Equals(wordsParsed[i].ToLower(), StringComparison.OrdinalIgnoreCase)))
                        {
                            state = 2;
                        }
                        else if (supportedShapes.Any(shape => shape.Equals(wordsParsed[i].ToLower(), StringComparison.OrdinalIgnoreCase)))
                        {
                            state = 3;
                            initializeShape.ShapeType = wordsParsed[i];
                        }
                        else state = 6;
                        break;
                    case 3: if (state3IgnoreIdentifier.Any(identifier => identifier.Equals(wordsParsed[i].ToLower(), StringComparison.OrdinalIgnoreCase)) ||
                             ignoreIdentifiers.Any(identifier => identifier.Equals(wordsParsed[i].ToLower(), StringComparison.OrdinalIgnoreCase)))
                        {
                            state = 3;
                        }
                        else if (supportedDimensions.Any(dimension => dimension.Equals(wordsParsed[i].ToLower(), StringComparison.OrdinalIgnoreCase)))
                        {
                            state = 4;
                            dimensionName = wordsParsed[i];
                            initializeShape.Dimensions.Add(wordsParsed[i], "");
                        }
                        else { state = 6;}
                        break;
                    case 4: if (state4IgnoreIdentifier.Equals(wordsParsed[i].ToLower(), StringComparison.OrdinalIgnoreCase))
                        {
                            state = 4;
                        }
                        else if (decimal.TryParse(wordsParsed[i], out amount))
                        {
                            state = 5;
                            initializeShape.Dimensions[dimensionName] = wordsParsed[i];
                        }
                        else { state = 6; }
                        break;
                    case 5: if (state3ReachableIdentifier.Equals(wordsParsed[i].ToLower(), StringComparison.OrdinalIgnoreCase))
                        {
                            state = 3;
                        }
                        break;
                    case 6:
                        initializeShape.Error = errorMessage;
                        break;
                }
            }
            return initializeShape;
        }
    }
}