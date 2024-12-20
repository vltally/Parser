using ConsoleApp1.Parser_Utilities.Nodes;
using ConsoleApp1.Tokenizer;

namespace ConsoleApp1.Parser_Utilities;

public interface IParser
{
    Node Parse(List<Token> tokens);
}