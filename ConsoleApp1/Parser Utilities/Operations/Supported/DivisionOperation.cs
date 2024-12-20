namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public sealed class DivisionOperation : IOperation
{
    public OperationPriority OperationPriority => OperationPriority.Medium;

    public Func<double, double, double> Execute => (leftOperand, rightOperand) =>
    {
        if (rightOperand == 0)
            throw new DivideByZeroException();
        return leftOperand / rightOperand;
    };
}