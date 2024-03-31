# String Calculator Kata
The following is a TDD Kata, an exercise in coding, refactoring and test-first, that you should apply daily for at least 15-30 minutes.

## Kata variation
The exercise is a variation of the original String Calculator Kata provided by [Roy Osherove](http://osherove.com/tdd-kata-1).

This version is a bit more complex and requires a bit more work to complete.

You can start with the original version 
and then try this one if you want to see how **small changes can make a big difference**.

The description comes from the [TDD Manifesto](https://tddmanifesto.com/exercises/).

## Before you start
* Try not to read ahead.
* Do **one task at a time**. The trick is to learn to work incrementally.
* Make sure you only test for **correct inputs**. There is no need to test for invalid inputs for this kata.

## The kata

### Step 1: the simplest thing
In a *test-first manner*, create a simple class `StringCalculator` with a method `int Add(string numbers)`.

The string argument can contain *0, 1 or 2 numbers*, and will *return their __sum__* (for an `empty string` it will return *0*) for example `""` or `"1"` or `"1,2"`.

*Hint*:
* Start with the **simplest** test case of an `empty string` and move to `one and two numbers`.
* Remember to solve things as **simply as possible** so that you force yourself to write tests you did not think about.
* Remember to **refactor** after each passing test.

### Step 2: handle an unknown amount of numbers
Allow the `Add` method to handle an unknown amount of numbers.

### Step 3: handle new lines between numbers
Allow the `Add` method to handle `new lines` between numbers (instead of `commas`).

* the following input is ok:  `"1\n2,3"` (will equal *6*)
* the following input is **NOT** ok:  `"1,\n"` (not need to prove it - just clarifying)

### Step 4: support different delimiters
To change a delimiter, the beginning of the string will contain a separate line that looks like this:   

`"//[delimiter]\n[numbers...]"`

For example:

`"//;\n1;2"` should return `3` where the default delimiter is `';'`.

The first line is optional. 
All existing scenarios should still be supported.

### Step 5: negative numbers
Calling `Add` with a `negative number` will throw an `exception` `"negatives not allowed"` - and the negative that was passed.

For example `Add("1,4,-1")` should throw an exception with the message `"negatives not allowed: -1"`.

### Step 6: multiple negative numbers
If there are multiple negatives, show all of them in the exception message.

For example `Add("1,4,-1,-2")` should throw an exception with the message `"negatives not allowed: -1, -2"`.

---

STOP HERE if you are a beginner.
Continue if you can finish the steps so far in less than 30 minutes.

---

### Step 7: `Add` call tracker
In a *test-first manner*, add a method to `StringCalculator` called `int GetCalledCount` that returns how many times `Add` was invoked.

**Remember** - Start with a failing (or even non compiling) test.

### Step 8: event `Add` call tracker (.NET Only)
In a *test-first manner*, add an event to the `StringCalculator` named `event Action<string, int> AddOccured`, that is triggered after every `Add` call.

*Hint*:

Create the `event` declaration first:
then write a failing test that listens to the `event` and proves it should have been triggered when calling `Add`.

**Hint 2**:

Example:
``` csharp

    string giveninput = null;
    sc.AddOccured += delegate(string input, int result)
    {
        giveninput = input;
    };

```

### Step 9: ignore big numbers
Numbers bigger than `1000` should be **ignored**.

For example:

`2 + 1001 = 2`

### Step 10: support any length delimiters

Delimiters can be of any length with the following format:

`“//[delimiter]\n”`

For example:

`“//[***]\n1***2***3”` should return `6`.

### Step 11: support multiple delimiters

Allow multiple delimiters like this:

`“//[delim1][delim2]\n”`

For example:

`“//[*][%]\n1*2%3”` should return `6`.

### Step 12: support multiple any length delimiters

make sure you can also handle multiple delimiters with length longer than one char.

For example:

`“//[**][%%]\n1**2%%3”` should return `6`.
