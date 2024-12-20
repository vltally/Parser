namespace ConsoleApp1.Parser_Utilities.Operations;

public class Multiplication : IOperation
{
    public Priority Priority { get; } = Priority.Medium; 
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => leftOperand * rightOperand;
}