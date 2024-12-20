using ConsoleApp1.Parser_Utilities.Nodes;
using ConsoleApp1.Parser_Utilities.Operations;
using ConsoleApp1.Tokenizer;

namespace ConsoleApp1.Parser_Utilities
{
    public sealed class ExpressionParser : IParser
    {
        private const string NoTokensErrorMessage = "No tokens were provided to parse.";
        private const string UnexpectedEndErrorMessage = "Unexpected end of tokens while parsing primary.";
        private const string ExtraTokensErrorMessage = "Extra tokens found after parsing completed.";
        private const string MissingClosingParenthesisErrorMessage = "Missing closing parenthesis.";
        private const string InvalidOperationTokenErrorMessage = "Expected an operation token at this position.";
        private const string InvalidNumberFormatErrorMessage = "Invalid number format: {0}";
        private const string UnexpectedTokenErrorMessage = "Unexpected token {0} at position {1}.";

        private List<Token> _tokens = null!;
        private int _currentTokenIndex;

        public Node Parse(List<Token> tokens)
        {
            if (tokens == null) 
                throw new ArgumentNullException(nameof(tokens), NoTokensErrorMessage);

            if (tokens.Count == 0)
                throw new InvalidOperationException(NoTokensErrorMessage);

            _tokens = tokens;
            _currentTokenIndex = 0;

            Node root = ParseLowestPriority();

            if (_currentTokenIndex < _tokens.Count)
                throw new InvalidOperationException(ExtraTokensErrorMessage);

            return root;
        }

        private Node ParsePrimary()
        {
            if (_currentTokenIndex >= _tokens.Count)
                throw new InvalidOperationException(UnexpectedEndErrorMessage);

            Token currentToken = _tokens[_currentTokenIndex];

            if (currentToken is OperandToken operandToken)
            {
                if (!int.TryParse(operandToken.Value, out int numericValue))
                    throw new FormatException(string.Format(InvalidNumberFormatErrorMessage, operandToken.Value));

                _currentTokenIndex++;
                return new NumberNode(numericValue);
            }

            if (currentToken is Parenthesis parenthesis && parenthesis.Direction == ParenthesisDirection.Left)
            {
                _currentTokenIndex++; // consume '('
                Node expression = ParseLowestPriority();

                if (_currentTokenIndex >= _tokens.Count ||
                    !(_tokens[_currentTokenIndex] is Parenthesis closeParen) ||
                    closeParen.Direction != ParenthesisDirection.Right)
                {
                    throw new InvalidOperationException(MissingClosingParenthesisErrorMessage);
                }

                _currentTokenIndex++; // consume ')'
                return expression;
            }

            throw new InvalidOperationException(
                string.Format(UnexpectedTokenErrorMessage, currentToken.GetType().Name, _currentTokenIndex));
        }

        private Node ParseBinaryOperation(Func<Node> parseNextLevel, OperationPriority operationPriority)
        {
            Node leftNode = parseNextLevel();
            while (IsOperationWithPriority(operationPriority))
            {
                OperationToken operationToken = GetOperationToken();
                Func<double, double, double> operationFunc = operationToken.Operation.Execute;

                _currentTokenIndex++;
                Node rightNode = parseNextLevel();
                leftNode = new BinaryOperationNode(leftNode, rightNode, operationFunc);
            }

            return leftNode;
        }

        private OperationToken GetOperationToken()
        {
            if (_currentTokenIndex >= _tokens.Count || _tokens[_currentTokenIndex] is not OperationToken opToken)
                throw new InvalidOperationException(InvalidOperationTokenErrorMessage);

            return opToken;
        }

        private bool IsOperationWithPriority(OperationPriority operationPriority) =>
             _currentTokenIndex < _tokens.Count &&
                   _tokens[_currentTokenIndex] is OperationToken opToken &&
                   opToken.Operation.OperationPriority == operationPriority;

        private Node ParseLowestPriority() =>
            ParseBinaryOperation(ParseMediumPriority, OperationPriority.Low);

        private Node ParseMediumPriority() =>
            ParseBinaryOperation(ParseHigherPriority, OperationPriority.Medium);

        private Node ParseHigherPriority() =>
            ParseBinaryOperation(ParsePrimary, OperationPriority.High);
    }
}