using ConsoleApp1.Parser_Utilities.Operations;
using ConsoleApp1.Parser_Utilities.Operations.Supported;

namespace ConsoleApp1.Parser_Utilities;

/// <summary>
/// Provides a factory for creating <see cref="IOperation"/> instances based on a given operator symbol.
/// </summary>
/// <remarks>
/// The <see cref="OperationManager"/> maps single-character symbols (e.g., '+', '-', '*') 
/// to functions that construct corresponding <see cref="IOperation"/> instances. 
/// This allows the parser and tokenizer to remain generic and operation-agnostic, 
/// while enabling new operations to be added by simply extending the dictionary.
/// </remarks>
public sealed class OperationManager
{
    // private readonly Dictionary<char, Func<IOperation>> _operations = new()
    // {
    //     { '+', () => new AdditionOperation() },
    //     { '-', () => new SubtractionOperation() },
    //     { '*', () => new MultiplicationOperation() },
    //     { '/', () => new DivisionOperation() },
    //     { '%', () => new ModulusOperation() },
    //     { '^', () => new PowerOperation() }
    // };
    
    private readonly List<IOperation> _operations = new()
    {
        new AdditionOperation(),
        new SubtractionOperation(),
        new MultiplicationOperation(),
        new DivisionOperation(),
        new ModulusOperation(),
        new PowerOperation(),
    };

    public OperationManager()
    {
        
    }
    
    /// <summary>
    /// Retrieves an <see cref="IOperation"/> instance for the given operator symbol.
    /// </summary>
    /// <param name="symbol">A character representing a mathematical operator (e.g., '+', '*').</param>
    /// <returns>
    /// An <see cref="IOperation"/> instance corresponding to the specified symbol, 
    /// or <c>null</c> if the symbol is not recognized.
    /// </returns>
    /// <remarks>
    /// The method returns <c>null</c> if the provided symbol is not found in the dictionary, 
    /// enabling callers to handle unknown operators gracefully.
    /// </remarks>
   
    
    public IOperation? GetOperation(char symbol)
    {
        // Використовуємо LINQ для пошуку операції за символом.
        IOperation? operation = _operations
            .FirstOrDefault(op => op.Symbol == symbol); // Припустимо, що кожна операція має властивість Symbol

        if (operation == null)
        {
            // Якщо операцію не знайдено, повертаємо null
            return null;
        }

        return operation;
    }
    
    
    // public IOperation? GetOperation(char symbol)
    // {
    //     // First check if the symbol is recognized.
    //     if (!_operations.ContainsKey(symbol))
    //     {
    //         // Unknown operator symbol: return null, allowing caller to handle it as needed.
    //         return null;
    //     }
    //
    //     // Retrieve and invoke the factory method to create the operation.
    //     if (_operations.TryGetValue(symbol, out Func<IOperation>? factory))
    //     {
    //         return factory();
    //     }
    //
    //     // This scenario is theoretically unreachable given the ContainsKey check, 
    //     // but throwing an exception to indicate a coding error is safer.
    //     throw new InvalidOperationException("Operation factory failed to create an instance unexpectedly.");
    // }
    
}