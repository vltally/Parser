namespace ConsoleApp1.Parser_Utilities.Nodes;

public class BinaryOperationNode(Node left, Node right, Func<double, double, double> operation)
    : Node
{
    public override double Evaluate()
    {
        double leftVal = left.Evaluate();
        double rightVal = right.Evaluate();
        return operation(leftVal, rightVal);
    }
}