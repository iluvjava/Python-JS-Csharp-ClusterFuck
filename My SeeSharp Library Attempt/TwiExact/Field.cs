using System;
using System.Numerics;
using System.Text;

namespace TwiExact.Field
{
    /// <summary>
    /// Rational Numbers as in the mathematical sense.
    /// </summary>
    public class ExactRational: OrderedField
    {
        // numerator
        public BigInteger n { get; protected set; }
        //denominator 
        public BigInteger d { get; protected set; }

        internal ExactRational()
        {

        }

        /// <summary>
        /// Public static method for getting an instance of the class. 
        /// </summary>
        /// <Exception>
        /// Divided by zero. 
        /// </Exception>
        /// <param name="numerator"></param>
        /// <param name="denominator"></param>
        /// <returns>
        /// 
        /// </returns>
        public static ExactRational ConstructExactRational
            (BigInteger numerator, BigInteger denominator)
        {
            if (denominator.IsZero) throw new DivideByZeroException();
            var res = new ExactRational();
            var commonfactor = GCD(numerator, denominator);
            numerator /= commonfactor;
            denominator /= commonfactor;
            res.n = numerator;
            res.d = denominator;
            return res;
        }


        public static ExactRational ConstructExactRational(int a, int b)
        {
            if (b == 0) throw new DivideByZeroException();
            var abi = new BigInteger(a);
            var bbi = new BigInteger(b);
            return ConstructExactRational(abi,bbi);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name=""></param>
        /// <returns>
        /// The greatest common factor between 2 bigintegers. 
        /// </returns>
        public static BigInteger GCD(BigInteger a, BigInteger b)
        {
            if (b == 0) return a;
            return GCD(b, a%b);
        }

        public override OrderedField Add(OrderedField f)
        {
            throw new NotImplementedException();
        }

        public override OrderedField AdditiveInverse()
        {
            throw new NotImplementedException();
        }

        public override int CompareTo(OrderedField other)
        {
            throw new NotImplementedException();
        }

        public override OrderedField DeepCopy()
        {
            throw new NotImplementedException();
        }

        public override bool IsNegative()
        {
            if (IsZero()) return false;
            return n.Sign!= n.Sign;
        }

        public override bool IsPositive()
        {
            if (IsZero()) return false; 
            return n.Sign == n.Sign;
        }

        public override bool IsZero()
        {
            return n.IsZero;
        }

        public override OrderedField MultiplicativeInverse()
        {
            throw new NotImplementedException();
        }

        public override OrderedField Multiply(OrderedField f)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The number will be represented as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (this.IsZero())
            {
                return "0";
            }
            var res = new StringBuilder();
            if (n.IsOne && d.IsOne)
            {
                res.Append("1");
            }
            else
            {
                res.Append(n.ToString());
                res.Append("/");
                res.Append(d.ToString());
            }
            if (this.IsPositive())
            {
                return res.ToString();
            }
            res.Insert(0, "-(");
            res.Append(")");
            return res.ToString(); 
        }
    }

    /// <summary>
    /// Field as defined in pure math
    /// </summary>
    public abstract class OrderedField : IComparable<OrderedField>
    {
        public static OrderedField operator -(OrderedField arg1)
        {
            return arg1.AdditiveInverse();
        }

        public static OrderedField operator -(OrderedField arg1, OrderedField arg2)
        {
            return arg1.Add(arg2.AdditiveInverse());
        }

        public static OrderedField operator *(OrderedField arg1, OrderedField arg2)
        {
            return arg1.Multiply(arg2);
        }

        public static OrderedField operator /(OrderedField arg1, OrderedField arg2)
        {
            if (arg2.IsZero()) throw new DivideByZeroException();
            return arg1.Multiply(arg2.MultiplicativeInverse());
        }

        public static OrderedField operator +(OrderedField arg1, OrderedField arg2)
        {
            return arg1.Add(arg2);
        }

        /// <summary>
        /// Internal State cannot be altered
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public abstract OrderedField Add(OrderedField f);

        /// <summary>
        /// Internal State cannot be altered
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public abstract OrderedField AdditiveInverse();

        public abstract int CompareTo(OrderedField other);

        public abstract OrderedField DeepCopy();

        public abstract bool IsZero();
        public abstract bool IsPositive();
        public abstract bool IsNegative();

        /// <summary>
        /// Internal State cannot be altered
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public abstract OrderedField MultiplicativeInverse();

        /// <summary>
        /// Internal State cannot be altered
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public abstract OrderedField Multiply(OrderedField f);
    }


}