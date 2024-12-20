using ConsoleApp1.Parser_Utilities.Tokens;

namespace ConsoleApp1.Tokenizer;

public interface ITokenizer
{
    List<Token> Tokenize(string inputCharacters);
}