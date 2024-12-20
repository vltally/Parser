namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public sealed class AdditionOperation : IOperation
{
    public Priority Priority => Priority.Low;
    public Func<double, double, double> Execute { get; } = (leftOperand, rightOperand) => leftOperand + rightOperand;
}