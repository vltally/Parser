namespace ConsoleApp1.Parser_Utilities.Operations;

public interface IOperation
{
    Priority Priority { get; }
    Func<double, double, double> Execute { get; }
}
