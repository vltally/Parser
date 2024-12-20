using ConsoleApp1.Parser_Utilities.Operations;
using ConsoleApp1.Parser_Utilities.Operations.Supported;

namespace ConsoleApp1.Parser_Utilities;

internal sealed class OperationFactory
{
    private readonly Dictionary<char, Func<IOperation>> _operations = new()
    {
        { '+', () => new AdditionOperation() },
        { '-', () => new Subtraction() },
        { '*', () => new MultiplicationOperation() },
        { '/', () => new DivisionOperation() },
        { '%', () => new ModulusOperation() },
        { '^', () => new PowerOperation() },
    };

    public IOperation? GetOperation(char symbol)
    {
        if (_operations.ContainsKey(symbol) is not true)
        {
            return null;
        }
        
        if (_operations.TryGetValue(symbol, out Func<IOperation>? value))
        {
            return value();
        }

        throw new Exception("Unknown operation symbol.");
    }
}
