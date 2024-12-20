namespace ConsoleApp1.Parser_Utilities.Operations;

public interface IOperation
{
    OperationPriority OperationPriority { get; }
    Func<double, double, double> Execute { get; }
}
