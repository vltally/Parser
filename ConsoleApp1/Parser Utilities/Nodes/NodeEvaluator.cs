namespace ConsoleApp1.Parser_Utilities.Nodes;

public class NodeEvaluator : INodeEvaluator
{
    public double Evaluate(Node node) => node.Evaluate();
}