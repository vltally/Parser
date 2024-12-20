namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public class ModulusOperation : IOperation
{
    public OperationPriority OperationPriority => OperationPriority.Medium;
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => leftOperand % rightOperand;
}