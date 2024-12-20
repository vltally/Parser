using ConsoleApp1.Parser_Utilities.Tokens;

namespace ConsoleApp1.Tokenizer;

public interface ITokenizer
{
    IEnumerable<Token> Tokenize(string inputCharacters);
}