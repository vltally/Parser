using ConsoleApp1.Parser_Utilities.Operations;
using ConsoleApp1.Parser_Utilities.Tokens;

namespace ConsoleApp1.Tokenizer;

public class OperationToken(IOperation operation) : Token
{
    public IOperation Operation { get; } = operation;
}