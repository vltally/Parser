using ConsoleApp1.Parser_Utilities;
using ConsoleApp1.Parser_Utilities.Nodes;
using ConsoleApp1.Parser_Utilities.Tokens;
using ConsoleApp1.Tokenizer;

namespace ConsoleApp1.Evaluator;

public sealed class ExpressionEvaluator(ITokenizer tokenizer, IParser parser, INodeEvaluator nodeEvaluator)
    : IExpressionEvaluator
{
    public double Evaluate(string expression)
    {
        List<Token> tokens = tokenizer.Tokenize(expression);
        Node ast = parser.Parse(tokens);
        return nodeEvaluator.Evaluate(ast);
    }
}