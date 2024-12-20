namespace ConsoleApp1.Tokenizer;

public interface ITokenizer
{
    List<Token> Tokenize(string inputCharacters);
}