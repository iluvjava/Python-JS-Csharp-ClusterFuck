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
        public static char[] DefinedFieldOpt = new char[]{'+','-','*','/'};
        public static HashSet<char> DefinedFieldOptSet = new HashSet<char>(){ '+', '-', '*', '/' }; 
        public static char[] Brackets = new char[] { '(', ')'};

        protected Parser()
        { }

        protected Parser GetInstance(string arg)
        {
            var p = new Parser();
            p.Content = arg;
            return null; 
        }

        /// <summary>
        /// split the operator and the fields. 
        /// </summary>
        /// <returns>
        /// A list where all the strig are separate. 
        /// </returns>
        public IList<string> TryParse(string expression)
        {
            if (expression.Length == 0)
                throw new EmptyExpressionError();
            if (ValidateBracketBalance(expression))
                throw new UnBalancedBracketException();

            var seperatorposition = new List<int>();

            for (int i = 1;  i < expression.Length - 1; i++)
            {
                //bracket is syntactical 
                if (expression[i] == '(' || expression[i] == ')')
                {
                    seperatorposition.Add(i);
                    continue;
                }
                bool preisleftbrac = expression[i - 1] == '(';
                bool isopt = Parser.DefinedFieldOptSet.Contains(expression[i]);
                bool canbeuniaryopt = expression[i] == '-';
                if (isopt)
                {
                    if (canbeuniaryopt)
                    {
                        if(preisleftbrac) seperatorposition.Add(i);
                    }
                    seperatorposition.Add(i);
                }
            }

            //begin the parsing. 
            var result = new List<string>(); 


            return result;
        }

        /// <summary>
        /// Validate the expression is valid. 
        /// - It checks the balance of the brackets. 
        /// </summary>
        /// <returns>
        /// 
        /// </returns>
        public bool ValidateBracketBalance(string arg)
        {
            int bracketcount = 0;
            foreach(char c in arg)
            {
                bracketcount += c == '(' ? 1 : 0;
                bracketcount += c==')'? -1: 0;
                if (bracketcount == -1) return false; 
            }
            return true; 
        }

        /// <summary>
        /// Validate if the operators are all in the right place. 
        /// </summary>
        /// <returns></returns>
        public bool ValidateOptBalance()
        {
            return true;
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


    [Serializable]
    public class UnBalancedBracketException : Exception
    {
        public UnBalancedBracketException()
        {
        }

        public UnBalancedBracketException(string message) : base(message)
        {
        }

        public UnBalancedBracketException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnBalancedBracketException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }


}
