namespace ConsoleApp1.Parser_Utilities.Tokens;

public enum ParenthesisDirection : byte
{
    None,  // 0
    Left,  // 1
    Right  // 2
}

public class Paren : Token
{
    
    private readonly Dictionary<char, ParenthesisDirection> _parenthesisMap = new()
    {
        { '(', ParenthesisDirection.Left },
        { ')', ParenthesisDirection.Right }
    };

    public ParenthesisDirection Direction { get; }

    public Paren(char parenthesis)
    {
        if (!_parenthesisMap.TryGetValue(parenthesis, out var direction))
        {
            throw new ArgumentException($"Invalid parenthesis character: {parenthesis}");
        }

        Direction = direction;
    }

    public string GetValue()
    {
        return _parenthesisMap.FirstOrDefault(x => x.Value == Direction).Key.ToString();
    }
    
    public override string ToString()
    {
        return GetValue();
    }
}
