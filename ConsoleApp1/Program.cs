using ConsoleApp1.Evaluator;
using ConsoleApp1.Parser_Utilities;
using ConsoleApp1.Parser_Utilities.Nodes;
using ConsoleApp1.Tokenizer;

namespace ConsoleApp1;

internal class Program
{
    private static void Main()
    {
        const string input = "3%2+4*(20-5*(100+3)+16*2)^2-18/2^2";

        ITokenizer tokenizer = new ExpressionTokenizer();
        IParser parser = new ExpressionParser();
        INodeEvaluator nodeEvaluator = new NodeEvaluator();
        IExpressionEvaluator evaluator = new ExpressionEvaluator(tokenizer, parser, nodeEvaluator);

        Console.WriteLine($"Input: {input}");
        double result = evaluator.Evaluate(input);
        Console.WriteLine($"Result: {Math.Round(result, 2)}");
    }
}