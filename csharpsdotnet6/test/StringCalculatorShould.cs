using FluentAssertions;
using Xunit;

namespace StringCalculator.Tests;

public class StringCalculatorShould
{
    [Fact]
    public void Test1()
    {
        false.Should().BeTrue();
    }
}