namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public sealed class MultiplicationOperation : IOperation
{
    public Priority Priority => Priority.Medium;
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => leftOperand * rightOperand;
}