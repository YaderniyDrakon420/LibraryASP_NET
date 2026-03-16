using Calculator;

namespace Books.Tests;

public class BookTest
{
    [Fact]
    public void CalculatorSum_FiveAndTwo_ReturnsSeven()
    {
        int a = 5;
        int b = 2;

        int result = 7;
        int answer = Calc.Sum(a, b);
        Assert.Equal(result, answer);
    }

    [Fact]
    public void CalculatorMult_FiveAndTwo_ReturnsSeven()
    {
        int a = 5;
        int b = 3;

        int result = 10;
        int answer = Calc.Mult(a, b);
        Assert.Equal(result, answer);
    }
}