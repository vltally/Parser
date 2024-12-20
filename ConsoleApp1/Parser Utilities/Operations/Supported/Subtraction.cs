namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public class Subtraction : IOperation
{
    public Priority Priority => Priority.Low;
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => leftOperand - rightOperand;
}