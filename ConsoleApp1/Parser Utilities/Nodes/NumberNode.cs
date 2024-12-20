namespace ConsoleApp1.Parser_Utilities.Nodes;

internal class NumberNode : Node
{
    public float Value { get; }
    public NumberNode(float value) => Value = value;
    public override double Evaluate() => Value;
}