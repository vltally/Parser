namespace ConsoleApp1.Parser_Utilities.Nodes;

public class NumberNode(double value) : Node
{
    public override double Evaluate()
    {
        return value;
    }
}