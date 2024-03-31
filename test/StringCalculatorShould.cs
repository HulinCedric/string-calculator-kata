using FluentAssertions.LanguageExt;  
using Xunit;  
  
namespace StringCalculatorKata.Tests;  
  
public class StringCalculatorShould  
{  
    [Theory]  
    [InlineData("", 0)]  
    [InlineData("1", 1)]  
    [InlineData("1,2", 3)]  
    public void Step1(string inputs, int result)  
        => StringCalculator.Add(inputs).Should().Be(result);  
  
    [Theory]  
    [InlineData("1,2,10", 13)]  
    [InlineData("1,2,10,11", 24)]  
    public void Step2(string inputs, int result)  
        => StringCalculator.Add(inputs).Should().Be(result);  
  
    [Theory]  
    [InlineData("1,2\n3", 6)]  
    [InlineData("1,2\n3\n4", 10)]  
    public void Step3(string inputs, int result)  
        => StringCalculator.Add(inputs).Should().Be(result);  
  
    [Theory]  
    [InlineData("1,2,", "Number expected at position 5")]  
    [InlineData("2,\n3", "Number expected at position 2")]  
    public void Step4(string inputs, string reason)  
        => StringCalculator.Add(inputs)  
            .Should()  
            .Be(reason);  
  
    [Theory]  
    [InlineData("//;\n1;3", 4)]  
    [InlineData("//|\n1|2|3", 6)]  
    [InlineData("//sep\n2sep5", 7)]  
    public void Step5(string inputs, int result)  
        => StringCalculator.Add(inputs).Should().Be(result);  
  
    [Theory]  
    [InlineData("//|\n1|2,3", "'|' expected but ',' found at position 3")]  
    public void Step5_fail(string inputs, string reason)  
        => StringCalculator.Add(inputs)  
            .Should()  
            .Be(reason);  
  
    [Theory]  
    [InlineData("1,-2", "Negative number(s) not allowed: -2")]  
    [InlineData("2,-4,-9", "Negative number(s) not allowed: -4, -9")]  
    public void Step6(string inputs, string reason)  
        => StringCalculator.Add(inputs).Should().Be(reason);  
  
    [Theory]  
    [InlineData("//|\n1|2,-3", "Negative number(s) not allowed: -3\n'|' expected but ',' found at position 3")]  
    public void Step7(string inputs, string reason)  
        => StringCalculator.Add(inputs).Should().Be(reason);  
  
    [Theory]  
    [InlineData("2,1000", 2)]  
    public void Step8(string inputs, int result)  
        => StringCalculator.Add(inputs).Should().Be(result);  
}