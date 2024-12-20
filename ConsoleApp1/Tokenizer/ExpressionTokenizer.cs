using ConsoleApp1.Parser_Utilities;
using ConsoleApp1.Parser_Utilities.Operations;
using ConsoleApp1.Parser_Utilities.Tokens;

namespace ConsoleApp1.Tokenizer;

public sealed class ExpressionTokenizer : ITokenizer
{
    private readonly OperationFactory _operationFactory = new();

    public List<Token> Tokenize(string input)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));

        List<Token> tokens = [];
        input = input.Replace(" ", "");

        int currentIndex = 0;
        while (currentIndex < input.Length)
        {
            char currentCharacter = input[currentIndex];
            Token? operation = CreateOperationToken(currentCharacter);
            if (operation != null)
            {
                tokens.Add(operation);
                currentIndex++;
                continue;
            }

            Token? operand = CreateOperandToken(ref input, ref currentIndex);
            if (operand != null)
            {
                tokens.Add(operand);
                continue;
            }

            Parenthesis parenthesis = new(currentCharacter);
            if (parenthesis.Direction != ParenthesisDirection.None)
            {
                tokens.Add(parenthesis);
                currentIndex++;
                continue;
            }

            throw new InvalidOperationException(
                $"Unexpected character '{currentCharacter}' at position {currentIndex}.");
        }

        return tokens;
    }

    private OperationToken? CreateOperationToken(char currentCharacter)
    {
        IOperation? operation = _operationFactory.GetOperation(currentCharacter);
        return operation != null ? new OperationToken(operation) : null;
    }

    private static OperandToken? CreateOperandToken(ref string input, ref int currentIndex)
    {
        if (currentIndex >= input.Length || !char.IsDigit(input[currentIndex]))
            return null;

        string number = "";
        while (currentIndex < input.Length && char.IsDigit(input[currentIndex]))
        {
            number += input[currentIndex];
            currentIndex++;
        }

        return new OperandToken(number);
    }
}
