using System;
using System.Numerics;
using System.Text;

namespace TwiExact.Field
{
    /// <summary>
    /// Rational Numbers as in the mathematical sense.
    /// </summary>
    public class ExactRational : OrderedField
    {
        internal ExactRational()
        {
        }

        //denominator
        public BigInteger d { get; protected set; }
        // numerator
        public BigInteger n { get; protected set; }
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
            return ConstructExactRational(abi, bbi);
        }

        public static ExactRational ConstructExactRational(long a, long b)
        {
            return ConstructExactRational(new BigInteger(a), new BigInteger(b));
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
            return GCD(b, a % b);
        }

        /// <summary>
        /// The method add two field togther.
        /// - Only Exact rationals are supported.
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        public override OrderedField Add(OrderedField f)
        {
            if (f is ExactRational)
            {
                ExactRational that = f as ExactRational;
                BigInteger thisn = n, thisd = d, thatn = that.n, thatd = that.d;
                var commonnumerator = thisn * thatn;
                thisn *= thatd;
                thatn *= thisd;
                return ConstructExactRational(thisn + thatn, commonnumerator);
            }
            throw new NotImplementedException();
        }

        public override OrderedField AdditiveInverse()
        {
            if (this.IsZero())
            {
                return this;
            }
            return ConstructExactRational(-n, d);
        }

        public override int CompareTo(OrderedField other)
        {
            if (other is ExactRational)
            {
                ExactRational that = other as ExactRational;
                var difference = this - that;
                if (difference.IsZero())
                {
                    return 0;
                }
                return difference.IsPositive() ? 1 : -1;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return a deepcopy of the current instance.
        /// </summary>
        /// <returns></returns>
        public override OrderedField DeepCopy()
        {
            return ConstructExactRational(n, d);
        }

        public override bool IsNegative()
        {
            if (IsZero()) return false;
            return n.Sign != d.Sign;
        }

        public override bool IsPositive()
        {
            if (IsZero()) return false;
            return n.Sign == d.Sign;
        }

        public override bool IsZero()
        {
            return n.IsZero;
        }

        public override OrderedField MultiplicativeInverse()
        {
            return ConstructExactRational(d, n);
        }

        public override OrderedField Multiply(OrderedField f)
        {
            if (f is OrderedField)
            {
                ExactRational b = f as ExactRational;
                var thisn = this.n;
                var thisd = this.d;
                var thatn = b.n;
                var thatd = b.d;
                var c1 = GCD(thisn, thatd);
                var c2 = GCD(thatn, thisd);
                thisn /= c1;
                thatd /= c1;
                thatn /= c2;
                thisd /= c2;
                return ConstructExactRational(thisn * thatn, thatd * thisd);
            }
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
                res.Append(BigInteger.Abs(n).ToString());
                res.Append("/");
                res.Append(BigInteger.Abs(d).ToString());
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

        public static bool operator !=(OrderedField a, OrderedField b)
        {
            return a.CompareTo(b) != 0;
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

        public static bool operator <(OrderedField a, OrderedField b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator ==(OrderedField a, OrderedField b)
        {
            return a.CompareTo(b) == 0;
        }

        public static bool operator >(OrderedField a, OrderedField b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator >=(OrderedField a, OrderedField b)
        {
            return a.CompareTo(b) >= 0;
        }
        public static bool operator <=(OrderedField a, OrderedField b)
        {
            return a.CompareTo(b) <= 0;
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

        public abstract bool IsNegative();

        public abstract bool IsPositive();

        public abstract bool IsZero();
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