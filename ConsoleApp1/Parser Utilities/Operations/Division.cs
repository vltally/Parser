namespace ConsoleApp1.Parser_Utilities.Operations;

public class Division : IOperation
{
    public Priority Priority { get; } = Priority.Medium;

    public Func<double, double, double> Execute => (leftOperand, rightOperand) =>
    {
        try
        {
            return leftOperand / rightOperand;
        }
        catch
        {
            throw new DivideByZeroException();
        }

    };
}