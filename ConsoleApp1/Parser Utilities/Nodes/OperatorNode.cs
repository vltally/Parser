using ConsoleApp1.Parser_Utilities.Nodes;

namespace ConsoleApp1.Parser_Utilities;

internal class OperatorNode : Node
{
    private readonly Node _left;
    private readonly Node _right;
    private readonly Func<double, double, double> _operation;

    public OperatorNode(Node left, Node right, Func<double, double, double> operation)
    {
        _left = left;
        _right = right;
        _operation = operation;
    }

    public override double Evaluate() => _operation(_left.Evaluate(), _right.Evaluate());
}