namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public sealed class DivisionOperation : IOperation
{
    public Priority Priority => Priority.Medium;

    public Func<double, double, double> Execute => (leftOperand, rightOperand) =>
    {
        if (rightOperand == 0)
            throw new DivideByZeroException();
        return leftOperand / rightOperand;
    };
}