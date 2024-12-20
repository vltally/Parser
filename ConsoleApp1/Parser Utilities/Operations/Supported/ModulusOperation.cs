namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public class ModulusOperation : IOperation
{
    public Priority Priority => Priority.Medium;
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => leftOperand % rightOperand;
}