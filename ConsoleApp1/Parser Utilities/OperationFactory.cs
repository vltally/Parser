using ConsoleApp1.Parser_Utilities.Operations;

namespace ConsoleApp1.Parser_Utilities;

internal sealed class OperationFactory
{
    private readonly Dictionary<char, Func<IOperation>> _operations = new()
    {
        { '+', () => new Addition() },
        { '-', () => new Subtraction() },
        { '*', () => new Multiplication() },
        { '/', () => new Division() },
        { '%', () => new Modulus() },
        { '^', () => new Power() },
    };

    public IOperation? GetOperation(char symbol) //
    {
        //_operations.Keys.exist //
        
        if (_operations.TryGetValue(symbol, out Func<IOperation>? value))
        {
            return value();
        }

        throw new Exception(); //
        
    }
}
