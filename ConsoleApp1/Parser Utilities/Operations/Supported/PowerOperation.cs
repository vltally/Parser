namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public sealed class PowerOperation : IOperation
{
    public OperationPriority OperationPriority => OperationPriority.High;
    public Func<double, double, double> Execute => Math.Pow;  
}

