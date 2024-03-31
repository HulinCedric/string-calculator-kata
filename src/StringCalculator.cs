using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LanguageExt;
using static System.String;

namespace StringCalculatorKata;

public static partial class StringCalculator
{
    private const string NewLine = "\n";
    private const string DefaultDelimiter = ",";
    private const int BigNumber = 1000;

    public static Either<string, int> Add(string inputs)
    {
        if (IsEmpty(inputs))
        {
            return 0;
        }

        var (delimiters, numbersPart) = Delimiters(inputs);

        var numbers = Numbers(numbersPart)
            .Where(IsNotBigNumber)
            .ToList();

        var failures = Failures()
            .Concat(NegativeNumberFailures(numbers))
            .Concat(NumberRepresentationFailures(numbersPart, delimiters))
            .Concat(BadDelimiterFailures(numbersPart, delimiters))
            .ToList();

        return failures.Any()
                   ? Join(NewLine, failures)
                   : numbers.Sum();
    }

    private static bool IsEmpty(string inputs)
        => inputs == Empty;

    private static IEnumerable<string> Failures()
        => Enumerable.Empty<string>();

    private static (string[], string) Delimiters(string inputs)
    {
        var match = DelimiterDefinition().Match(inputs);
        if (!match.Success)
        {
            return ([DefaultDelimiter, NewLine], inputs);
        }

        var delimiter = match.Groups["delimiter"].Value;
        var numbers = match.Groups["numbers"].Value;

        return ([delimiter, NewLine], numbers);
    }

    private static int[] Numbers(string numbersPart)
        => NumbersDefinition()
            .Split(numbersPart)
            .Where(s => !IsNullOrWhiteSpace(s))
            .Select(int.Parse)
            .ToArray();

    private static bool IsNotBigNumber(int number)
        => number < BigNumber;

    private static IEnumerable<string> NegativeNumberFailures(List<int> numbers)
    {
        var negativeNumbers = numbers.Where(n => n < 0).ToList();
        if (negativeNumbers.Any())
        {
            yield return $"Negative number(s) not allowed: {Join(", ", negativeNumbers)}";
        }
    }

    private static IEnumerable<string> BadDelimiterFailures(
        string inputs,
        string[] delimiters)
    {
        var hasBadDelimiter = NotANumbersOrADelimiterDefinition(delimiters).Match(inputs);
        if (!hasBadDelimiter.Success)
        {
            yield break;
        }

        var badDelimiter = hasBadDelimiter.Value;
        var position = hasBadDelimiter.Index;

        yield return $"'{delimiters.First()}' expected but '{badDelimiter}' found at position {position}";
    }

    private static IEnumerable<string> NumberRepresentationFailures(string inputs, string[] delimiters)
    {
        var numberRepresentations = inputs.Split(delimiters, StringSplitOptions.None);
        if (numberRepresentations.Any(IsNullOrWhiteSpace))
        {
            var inputsWithoutDelimiters = delimiters.Aggregate(
                inputs,
                (current, delimiter) => current.Replace(delimiter, DefaultDelimiter));

            var position = inputsWithoutDelimiters.IndexOf(
                DefaultDelimiter + DefaultDelimiter,
                StringComparison.Ordinal);

            if (position == -1)
            {
                position = inputsWithoutDelimiters.Length;
            }

            yield return $"Number expected at position {position + 1}";
        }
    }

    [GeneratedRegex(@"^//(?<delimiter>.*)\n(?<numbers>.*)", RegexOptions.ExplicitCapture)]
    private static partial Regex DelimiterDefinition();

    private static Regex NotANumbersOrADelimiterDefinition(string[] delimiters)
        => new(@$"[^-\d{Join("", delimiters)}]", RegexOptions.Compiled);

    [GeneratedRegex(@"[^-\d]")]
    private static partial Regex NumbersDefinition();
}