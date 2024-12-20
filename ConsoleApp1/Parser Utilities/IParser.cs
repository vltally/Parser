using ConsoleApp1.Parser_Utilities.Nodes;
using ConsoleApp1.Parser_Utilities.Tokens;

namespace ConsoleApp1.Parser_Utilities;

public interface IParser
{
    Node Parse(List<Token> tokens);
}