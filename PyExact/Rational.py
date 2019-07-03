


class RationalNumber: 
    
    def __init__(self, numerator, denominator):
        if(denominator == 0):
            raise Exception("Divided by zero")
        if(type(numerator) is not int or type(denominator) is not int):
            raise Exception("Only integer is alowed. ")
        self.numerator = numerator
        self.denominator = denominator
        self._simplyfy()

    def __repr__(self):
        if(self.numerator == 0):
            return "0"
        n, d = abs(self.numerator), abs(self.denominator)
        if((self.numerator > 0) is (self.denominator > 0)):
            if(d == 1):
                return str(n)
            return str(n)+"/"+str(d)
        if(d == 1):
                return "-" + str(n)
        return "-"+str(n)+"/"+str(d)

    def _simplyfy(self):
        commondivisor = GCD(self.denominator, self.numerator)
        self.numerator//=commondivisor
        self.denominator//=commondivisor
        return

    def __add__(self, rational):
        """
            Add 2 rational.
        """
        resultdenominator = self.denominator * rational.denominator
        resultnumerator = self.numerator * rational.denominator
        resultnumerator += rational.numerator * self.denominator
        return RationalNumber(resultnumerator, resultdenominator)

    def __mul__(self, rational):
        """
            Multiply 2 rational. 
        """
        resultnumerator = self.numerator * rational.numerator
        resultdenominator = self.denominator * rational.denominator
        return RationalNumber(resultnumerator, resultdenominator)


def GCD(a,b):
    if(b == 0):
        return int(a)
    return GCD(b, a%b)


if __name__ == "__main__":
    print("Testing the class")
    a = RationalNumber(6, 12)
    b = RationalNumber(0, 2)
    c = RationalNumber(22, 22)
    d = RationalNumber(-1,2)
    e = RationalNumber(-2, 4)
    print(d + e)
    print(d * e)
    print(RationalNumber(25, 75) * RationalNumber(9, 27))