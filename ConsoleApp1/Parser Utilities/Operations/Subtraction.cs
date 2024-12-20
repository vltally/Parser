namespace ConsoleApp1.Parser_Utilities.Operations;

public class Subtraction : IOperation
{
    public Priority Priority { get; } = Priority.Low; 
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => leftOperand - rightOperand;
}