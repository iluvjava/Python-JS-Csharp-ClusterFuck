using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TwiExact.Parser
{
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

    /// <summary>
    /// This class parse a string of input into a datastructure. 
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// A list of predefined field operators. 
        /// </summary>

        public static readonly HashSet<char> DefinedFieldOptSet =
            new HashSet<char> { '+', '-', '*', '/' };
        public static readonly HashSet<char> DefinedFieldUnaryOptSet =
            new HashSet<char> { '+', '-' };
        public static readonly HashSet<char> DefinedFieldBinaryOptSet =
            new HashSet<char> {'+', '-', '*', '/' };

        public static readonly char[] FiledOptUnary = new char[] { '+', '-' };
        
        public static readonly char[] SyntacticOpt = new char[] { '(', ')' };
        public static readonly HashSet<char> SyntaticOpt = new HashSet<char>() { '(', ')' };
        protected IList<string> Parsed;
        protected Parser()
        {

        }

        protected string Content { get; set; }
        /// <summary>
        /// split the operator and the fields (operand)
        ///     - In the case of unary opts, it should be bundled with the operands. 
        ///         - If unary opt, then previous char is '(' or beginning of the expression 
        /// </summary>
        /// <returns>
        /// A list where all the strig are separate. 
        /// </returns>
        [Obsolete("")]
        public IList<string> TryParse()
        {
            var expression = this.Content;
            if (expression.Length == 0)
                throw new EmptyExpressionError();
            if (!ValidateBracketBalance())                throw new UnBalancedBracketException();

            var seperatorposition = new List<int>();
            for (int i = 1; i < expression.Length - 1; i++)
            {
                //bracket is syntactical 
                if (expression[i] == '(' || expression[i] == ')')
                {
                    seperatorposition.Add(i);
                    continue;
                }
                bool preisleftbrac = expression[i - 1] == '(';
                bool isopt = DefinedFieldOptSet.Contains(expression[i]);
                bool canbeuniaryopt = DefinedFieldUnaryOptSet.Contains(expression[i]);
                if (isopt)
                {
                    if (canbeuniaryopt)
                    {
                        if (preisleftbrac) seperatorposition.Add(i);
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
        public bool ValidateBracketBalance()
        {
            var arg = this.Content;
            int bracketcount = 0;
            foreach (char c in arg)
            {
                bracketcount += c == '(' ? 1 : 0;
                bracketcount += c == ')' ? -1 : 0;
                if (bracketcount == -1) return false;
            }
            return true;
        }

        /// <summary>
        /// Validate if the operators are all in the right place. 
        /// Conditions: 
        ///     - If unary: 
        ///         left side is empty or a parenthesis and not an operator.
        ///         and right side must be an operand
        ///     - If binary: 
        ///         left and right side must be an operand. 
        /// <remarks>
        /// 
        /// </remarks>
        /// </summary>
        /// <param name="expression">
        /// Metod assumes the expression is non empty. 
        /// </param>
        /// <returns>
        /// Flase if there the expression contains invalide operators configurations. 
        /// </returns>
        [Obsolete("Not Yet Tested")]
        public bool ValidateOptBalance()
        {
            var expression = this.Content;
            if (expression.Length == 0)
            {
                throw new EmptyExpressionError();
            }
            for (int l = -1, j = 0, r = 1; j < expression.Length; l++, r++, j++)
            {
                char c = expression[j];
                bool unary = DefinedFieldBinaryOptSet.Contains(c);
                bool binary = DefinedFieldBinaryOptSet.Contains(c);
                // Unary first cause it's a stronger prdicates. 
                if (unary)
                {
                    // right side. 
                    if (r == expression.Length ||
                        !DefinedFieldOptSet.Contains(expression[r]))
                    {
                        return false; 
                    }
                    // left side 
                    if(!binary)
                    {
                        // Must be a left parenthesis or empty
                        if (!(c == '(' || r == expression.Length))
                        {
                            return false; 
                        }
                    }
                }
                if (binary)
                {
                    // if binary then: 
                    // left side and right side must be operands
                    // if not binary then: 
                    // left side or right side are operators 
                    // (Including syntactic operators) or empty
                    if (l == -1 || r == expression.Length)
                    {
                        return false; 
                    }
                    if
                    (
                    DefinedFieldOptSet.Contains(expression[l])
                    ||
                    SyntacticOpt.Contains(expression[l])
                    )
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static Parser GetInstance(string arg)
        {
            var p = new Parser();
            p.Content = arg;
            return p; 
        }
    }
}
