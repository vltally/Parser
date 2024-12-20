namespace ConsoleApp1.Parser_Utilities.Operations;

public class Addition : IOperation
{
    public Priority Priority { get; } = Priority.Low;
    public Func<double, double, double> Execute { get; } = (leftOperand, rightOperand) => leftOperand + rightOperand;

   
}