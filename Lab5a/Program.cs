using System;
using System.Xml;

namespace Lab5a
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                string filename = args[0];
                XmlDocument inputFile = new XmlDocument();
                inputFile.Load(filename);
                LoadDocument(inputFile);
            }
            else
            {
                Console.WriteLine("No progrem arguments were provided. You should at least include the input file name.");
                Console.WriteLine("Program exiting.");
                return;
            }
        }
        static void LoadDocument(XmlDocument pinputFile)
        {
            XmlNode declarationNode = pinputFile.FirstChild;
            XmlNode sumsNode = declarationNode.NextSibling;
            LoadSums(sumsNode);
        }
        static void LoadSums(XmlNode pSumsNode)
        {
            XmlNodeList sumNodes = pSumsNode.ChildNodes;
            int numberofSums = sumNodes.Count;
            Console.WriteLine("Loading " + numberofSums + " sums.");
            for(int i = 0; i < numberofSums; i++)
            {
                Console.WriteLine("Loading sum: " + i);
                XmlNode sumNode = sumNodes[i];
                double result = LoadChildNode(sumNode);
                Console.WriteLine(" = " + result);
            }
        }
       
        static double LoadChildNode(XmlNode pNode)
        {
            if(pNode.Name == "number")
            {
                return LoadNumberNode(pNode);
            }
            else
            {
                return LoadOperatorNode(pNode);
            }
        }
        static double LoadNumberNode(XmlNode pNumberNode)
        {
            double number = double.Parse(pNumberNode.FirstChild.Value);
            Console.WriteLine(number);
            return number;
        }
        static double LoadOperatorNode(XmlNode pOperatorNode)
        {
            double result = 0;
            XmlNodeList childNodes = pOperatorNode.ChildNodes;
            for(int i = 0; i < childNodes.Count; i++)
            {
                XmlNode childNode = childNodes[i];
                double childResult = LoadChildNode(childNode);
                if(i == 0)
                {
                    result = childResult;
                    OutputOperatorString(pOperatorNode.Name);
                }
                else
                {
                    result = PerformOperator(pOperatorNode.Name, result, childResult);
                    if(i != childNodes.Count - 1)
                    {
                        OutputOperatorString(pOperatorNode.Name);
                    }
                    
                }
                
            }
            return result;
        }
        static double PerformOperator(string pNodeType, double pResult, double pChildResult)
        {
            if(pNodeType == "add")
            {
                pResult += pChildResult;
            }
            else if (pNodeType == "subtract")
            {
                pResult -= pChildResult;
            }
            else if (pNodeType == "multiply")
            {
                pResult *= pChildResult;
            }
            else if (pNodeType == "divide")
            {
                pResult /= pChildResult;
            }
            
            return pResult;
        }

        static void OutputOperatorString(string pNodeType)
        {
            switch (pNodeType)
            {
                case "add":
                    Console.Write(" + ");
                    break;
                case "subtract":
                    Console.Write(" - ");
                    break;
                case "multiply":
                    Console.Write(" * ");
                    break;
                case "divide":
                    Console.Write(" / ");
                    break;
            }
        }
    }
}
