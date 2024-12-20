namespace ConsoleApp1.Parser_Utilities.Operations;

public class Power : IOperation
{
    public Priority Priority { get; } = Priority.High; 
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => Math.Pow(leftOperand, rightOperand);  
}

