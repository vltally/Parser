namespace ConsoleApp1;

class Program
{
    static void Main()
    {
        ExpressionParser parser = new ExpressionParser();
        string input = "3%2+4*(20-5*(100+3)+16*2)^2-18/2^2";
       
        Console.WriteLine($"Input: {input}");
        double result = parser.ParseAndEvaluate(input);
        Console.WriteLine($"Result: {Math.Round(result, 2)}");
    }
}