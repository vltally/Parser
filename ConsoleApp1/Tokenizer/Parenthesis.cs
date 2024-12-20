namespace ConsoleApp1.Tokenizer;

public sealed class Parenthesis : Token
{
    private readonly Dictionary<char, ParenthesisDirection> _parenthesisMap = new()
    {
        { '(', ParenthesisDirection.Left },
        { ')', ParenthesisDirection.Right }
    };

    public ParenthesisDirection Direction { get; }

    public Parenthesis(char parenthesis)
    {
        if (!_parenthesisMap.TryGetValue(parenthesis, out var direction))
        {
            throw new ArgumentException($"Invalid parenthesis character: {parenthesis}");
        }

        Direction = direction;
    }

    private string GetValue()
    {
        return _parenthesisMap.FirstOrDefault(x => x.Value == Direction).Key.ToString();
    }
    
    public override string ToString()
    {
        return GetValue();
    }
}
