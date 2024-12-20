using ConsoleApp1.Parser_Utilities.Nodes;
using ConsoleApp1.Parser_Utilities.Operations;
using ConsoleApp1.Parser_Utilities.Tokens;

namespace ConsoleApp1.Parser_Utilities
{
    public class ExpressionParser : IParser
    {
        public Node Parse(IEnumerable<Token> tokens)
        {
            if (tokens == null)
                throw new ArgumentNullException(nameof(tokens));

            List<Token> tokenList = tokens.ToList();
            if (tokenList.Count == 0)
                throw new InvalidOperationException("No tokens to parse.");

            return ParseTokens(tokenList);
        }

        private Node ParseTokens(List<Token> tokens)
        {
            int currentTokenIndex = 0; // index of current token

            Node root = ParseLowestPriority();
            if (currentTokenIndex < tokens.Count)
                throw new InvalidOperationException("Extra tokens found after parsing completed.");

            return root;

            Node ParseLowestPriority()
            {
                return ParseBinaryOperation(ParseMediumPriority, Priority.Low);
            }

            Node ParseMediumPriority()
            {
                return ParseBinaryOperation(ParseHigherPriority, Priority.Medium);
            }

            Node ParseHigherPriority()
            {
                return ParseBinaryOperation(ParsePrimary, Priority.High);
            }

            Node ParseBinaryOperation(Func<Node> parseNextLevel, Priority priority)
            {
                Node left = parseNextLevel();
                while (IsOperationWithPriority(priority))
                {
                    OperationToken opToken = GetOperationToken();
                    Func<double, double, double> operationFunc = opToken.Operation.Execute;
                    currentTokenIndex++;
                    Node right = parseNextLevel();
                    left = new BinaryOperationNode(left, right, operationFunc);
                }

                return left;
            }

            Node ParsePrimary()
            {
                if (currentTokenIndex >= tokens.Count)
                    throw new InvalidOperationException("Unexpected end of tokens while parsing primary.");

                Token currentToken = tokens[currentTokenIndex];

                if (currentToken is OperandToken operandToken)
                {
                    if (!int.TryParse(operandToken.Value, out int value))
                        throw new FormatException($"Invalid number format: {operandToken.Value}");

                    currentTokenIndex++;
                    return new NumberNode(value);
                }

                if (currentToken is Paren paren && paren.Direction == ParenthesisDirection.Left)
                {
                    currentTokenIndex++; // consume '('
                    Node expression = ParseLowestPriority();

                    if (currentTokenIndex >= tokens.Count || !(tokens[currentTokenIndex] is Paren closeParen) || closeParen.Direction != ParenthesisDirection.Right)
                        throw new InvalidOperationException("Missing closing parenthesis.");

                    currentTokenIndex++; // consume ')'
                    return expression;
                }

                throw new InvalidOperationException($"Unexpected token {currentToken.GetType().Name} at position {currentTokenIndex}.");
            }

            OperationToken GetOperationToken()
            {
                if (currentTokenIndex >= tokens.Count || tokens[currentTokenIndex] is not OperationToken opToken)
                    throw new InvalidOperationException("Expected an operation token at this position.");

                return opToken;
            }

            bool IsOperationWithPriority(Priority priority)
            {
                return currentTokenIndex < tokens.Count &&
                       tokens[currentTokenIndex] is OperationToken opToken &&
                       opToken.Operation.Priority == priority;
            }
        }
    }
}