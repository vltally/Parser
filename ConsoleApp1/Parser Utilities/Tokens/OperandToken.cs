namespace ConsoleApp1.Parser_Utilities.Tokens;

public class OperandToken : Token
{
    public string Value { get; }

    public OperandToken(string value)
    {
        Value = value;
    }
    
}