using ConsoleApp1.Parser_Utilities.Nodes;
using ConsoleApp1.Parser_Utilities.Operations;
using ConsoleApp1.Parser_Utilities.Tokens;
using ConsoleApp1.Tokenizer;

namespace ConsoleApp1.Parser_Utilities;

public sealed class ExpressionParser : IParser
{
    private List<Token> _tokens = null!;
    private int _currentTokenIndex;

    public Node Parse(List<Token> tokens)
    {
        _tokens = tokens;
        _currentTokenIndex = 0;
        
        Node root = ParseLowestPriority();

        if (_currentTokenIndex < _tokens.Count)
        {
            throw new InvalidOperationException("Extra tokens found after parsing completed.");
        }

        return root;
    }

    private Node ParsePrimary()
    {
        if (_currentTokenIndex >= _tokens.Count)
            throw new InvalidOperationException("Unexpected end of tokens while parsing primary.");

        Token currentToken = _tokens[_currentTokenIndex];

        if (currentToken is OperandToken operandToken)
        {
            if (!int.TryParse(operandToken.Value, out int value))
                throw new FormatException($"Invalid number format: {operandToken.Value}");

            _currentTokenIndex++;
            return new NumberNode(value);
        }

        if (currentToken is Parenthesis paren && paren.Direction == ParenthesisDirection.Left)
        {
            _currentTokenIndex++; // consume '('
            Node expression = ParseLowestPriority();

            if (_currentTokenIndex >= _tokens.Count || !(_tokens[_currentTokenIndex] is Parenthesis closeParen) ||
                closeParen.Direction != ParenthesisDirection.Right)
                throw new InvalidOperationException("Missing closing parenthesis.");

            _currentTokenIndex++; 
            return expression;
        }

        throw new InvalidOperationException(
            $"Unexpected token {currentToken.GetType().Name} at position {_currentTokenIndex}.");
    }

    private Node ParseBinaryOperation(Func<Node> parseNextLevel, Priority priority)
    {
        Node left = parseNextLevel();
        while (IsOperationWithPriority(priority))
        {
            OperationToken opToken = GetOperationToken();
            Func<double, double, double> operationFunc = opToken.Operation.Execute;
            _currentTokenIndex++;
            Node right = parseNextLevel();
            left = new BinaryOperationNode(left, right, operationFunc);
        }

        return left;
    }

    private OperationToken GetOperationToken()
    {
        if (_currentTokenIndex >= _tokens.Count || _tokens[_currentTokenIndex] is not OperationToken opToken)
            throw new InvalidOperationException("Expected an operation token at this position.");

        return opToken;
    }

    private bool IsOperationWithPriority(Priority priority)
    {
        return _currentTokenIndex < _tokens.Count &&
               _tokens[_currentTokenIndex] is OperationToken opToken &&
               opToken.Operation.Priority == priority;
    }

    private Node ParseLowestPriority() =>
        ParseBinaryOperation(ParseMediumPriority, Priority.Low);

    private Node ParseMediumPriority() =>
        ParseBinaryOperation(ParseHigherPriority, Priority.Medium);

    private Node ParseHigherPriority() =>
        ParseBinaryOperation(ParsePrimary, Priority.High);
}