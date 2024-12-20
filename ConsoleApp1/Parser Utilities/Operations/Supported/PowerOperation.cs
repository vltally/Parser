namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public sealed class PowerOperation : IOperation
{
    public Priority Priority => Priority.High;
    public Func<double, double, double> Execute => Math.Pow;  
}

