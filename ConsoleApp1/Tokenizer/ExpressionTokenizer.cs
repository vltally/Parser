using ConsoleApp1.Parser_Utilities;
using ConsoleApp1.Parser_Utilities.Operations;

namespace ConsoleApp1.Tokenizer;

public sealed class ExpressionTokenizer : ITokenizer
{
    private const string Whitespace = " ";
    private const string UnexpectedCharacterMessage = "Unexpected character '{0}' at position {1}.";

    private readonly OperationFactory _operationFactory = new();

    public List<Token> Tokenize(string input)
    {
        input = input.Replace(Whitespace, string.Empty);

        List<Token> tokens = [];
        int currentIndex = 0;

        while (currentIndex < input.Length)
        {
            char currentChar = input[currentIndex];

            Token? operationToken = CreateOperationToken(currentChar);
            if (operationToken != null)
            {
                tokens.Add(operationToken);
                currentIndex++;
                continue;
            }

            Token? operandToken = CreateOperandToken(ref input, ref currentIndex);
            if (operandToken != null)
            {
                tokens.Add(operandToken);
                continue;
            }

            Parenthesis parenthesis = new(currentChar);
            if (parenthesis.Direction != ParenthesisDirection.None)
            {
                tokens.Add(parenthesis);
                currentIndex++;
                continue;
            }

            throw new InvalidOperationException(
                string.Format(UnexpectedCharacterMessage, currentChar, currentIndex));
        }

        return tokens;
    }

    private OperationToken? CreateOperationToken(char currentChar)
    {
        IOperation? operation = _operationFactory.GetOperation(currentChar);
        return operation != null ? new OperationToken(operation) : null;
    }

    private static OperandToken? CreateOperandToken(ref string input, ref int currentIndex)
    {
        if (currentIndex >= input.Length || !char.IsDigit(input[currentIndex]))
            return null;

        string number = string.Empty;
        while (currentIndex < input.Length && char.IsDigit(input[currentIndex]))
        {
            number += input[currentIndex];
            currentIndex++;
        }

        return new OperandToken(number);
    }
}