namespace ConsoleApp1.Tokenizer;

public sealed class OperandToken(string value) : Token
{
    public string Value { get; } = value;
}