namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public class SubtractionOperation : IOperation
{
    public OperationPriority OperationPriority => OperationPriority.Low;
    public Func<double, double, double> Execute => (leftOperand, rightOperand) => leftOperand - rightOperand;
}