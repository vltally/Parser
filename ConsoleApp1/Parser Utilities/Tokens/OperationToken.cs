using ConsoleApp1.Parser_Utilities.Operations;

namespace ConsoleApp1.Parser_Utilities.Tokens;

public class OperationToken : Token
{
    public IOperation Operation { get; }

    public OperationToken(IOperation operation)
    {
        Operation = operation;
    }
    
}