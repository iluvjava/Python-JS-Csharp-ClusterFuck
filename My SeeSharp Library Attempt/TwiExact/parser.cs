using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TwiExact
{
    /// <summary>
    /// This class parse a string of input into a datastructure. 
    /// </summary>
    class Parser
    {
        protected string Content { get; set; }
        protected IList<string> Parsed;
        /// <summary>
        /// A list of predefined field operators. 
        /// </summary>
        public static char[] DefieldFieldOpt = new char[]{'+','-','*','/'};

        protected Parser()
        { }

        protected Parser GetInstance(string arg)
        {
            var p = new Parser();
            p.Content = arg;
            return null; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///  
        /// </returns>
        public IList<string> TryParse(string expression)
        {
            if (expression.Length == 0)
            {
                throw new EmptyExpressionError();
            }
            foreach(char cha in expression)
            {

            }

            return null;
        }
    }

    [Serializable]
    internal class EmptyExpressionError : Exception
    {
        public EmptyExpressionError()
        {
        }

        public EmptyExpressionError(string message) : base(message)
        {
        }

        public EmptyExpressionError(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EmptyExpressionError(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


}
