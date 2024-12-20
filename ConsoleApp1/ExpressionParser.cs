using ConsoleApp1.Parser_Utilities;
using ConsoleApp1.Parser_Utilities.Nodes;
using ConsoleApp1.Parser_Utilities.Operations;
using ConsoleApp1.Parser_Utilities.Tokens;

namespace ConsoleApp1;

class ExpressionParser
{
    private readonly OperationFactory _operationFactory = new OperationFactory();
    
    
    
    public double ParseAndEvaluate(string inputCharacters)
    {
        List<Token> tokens = Tokenize(inputCharacters);
        Node rootNode = Parse(tokens);
        return rootNode.Evaluate();
    }

    OperationToken? CreateOperationToken(char currentCharacter)
    {
        IOperation operation;  
        operation =  _operationFactory.GetOperation(currentCharacter);
        if (operation != null)
        {
            return new OperationToken(operation);
        }

        return null;
    }

    OperandToken? CreateOperandToken(ref char currentCharacter, ref string inputCharacters, ref int currentIndex)
    {
        if (!char.IsDigit(currentCharacter)) 
            return null;
            
        string number = "";
        bool isNextNumber = char.IsDigit(inputCharacters[currentIndex]);
        bool isInRange = currentIndex < inputCharacters.Length;
        while (isInRange && isNextNumber)
        {
            number += inputCharacters[currentIndex];
            currentIndex++;
        }
        return new OperandToken(number);
            
        

       
    }

    private List<Token> Tokenize(string inputCharacters)
    {
        List<Token> tokens = new List<Token>();
        inputCharacters = inputCharacters.Replace(" ", "");
        
        int currentIndex = 0;
        while (currentIndex < inputCharacters.Length)
        {
            char currentCharacter = inputCharacters[currentIndex];

            Token? operation = CreateOperationToken(currentCharacter);
            if (operation != null)
            {
                tokens.Add(operation);
                currentIndex++;
                continue;
            }
            
            
            //IOperation operation;  
            //operation =  _operationFactory.GetOperation(currentCharacter);
            //if (operation != null)
            //{
            //    tokens.Add(new OperationToken(operation));
            //    currentIndex++;
            //    continue;
            //}
            
            //  Q
            Token? operand = CreateOperandToken(ref currentCharacter, ref inputCharacters, ref currentIndex);
            if (operand != null)
            {
                tokens.Add(operand);
                continue;
            }
            
            
            
            
            
            //if (char.IsDigit(currentCharacter)) 
            //{
            //    string number = "";
            //    while (currentIndex < inputCharacters.Length && char.IsDigit(inputCharacters[currentIndex]))
            //    {
            //       number += inputCharacters[currentIndex];
            //        currentIndex++;
            //    }
            //    tokens.Add(new OperandToken(number));
            //    continue;
            //}
            
            

            Paren paren = new Paren(currentCharacter);
            if (paren.Direction != ParenthesisDirection.None)
            {
                tokens.Add(paren);
                currentIndex++;
            }

            
        }
        return tokens;
    }

    private Node Parse(List<Token> tokens)
    {
    int currentTokenIndex = 0; // index of current token
    return ParseLowestPriority();

    Node ParseLowestPriority()
    {
        return ParseBinaryOperation(ParseMediumPriority, Priority.Low);
    }

    Node ParseMediumPriority()
    {
        return ParseBinaryOperation(ParseHigherPriority, Priority.Medium);
    }

    Node ParseHigherPriority()
    {
        return ParseBinaryOperation(ParsePrimary, Priority.High);
    }

    Node ParseBinaryOperation(Func<Node> parseNextLevel, Priority priority)
    {
        Node left = parseNextLevel();
        while (IsOperationWithPriority(priority))
        {
            OperationToken opToken = GetOperationToken();
            Func<double, double, double> operationFunc = opToken.Operation.Execute;
            currentTokenIndex++; 
            Node right = parseNextLevel();
            left = new OperatorNode(left, right, operationFunc);
        }
        return left;
    }


    Node ParsePrimary()
    {
        if (tokens[currentTokenIndex] is OperandToken operandToken)
        {
            int value = int.Parse(operandToken.Value);
            currentTokenIndex++; 
            return new NumberNode(value);
        }
        if (tokens[currentTokenIndex] is Paren paren && paren.Direction == ParenthesisDirection.Left)
        {
            currentTokenIndex++; // skipping paren operator '('
            Node expresion = ParseLowestPriority();
            if (tokens[currentTokenIndex] is Paren closeParen && closeParen.Direction == ParenthesisDirection.Right)
            {
                currentTokenIndex++; // skipping paren operator ')'
                return expresion;
            }
            
            return null;
            
        }

        throw new Exception("Unexpected token");
    }
    
    OperationToken GetOperationToken()
    {
        if (tokens[currentTokenIndex] is not OperationToken opToken)
        {
            throw new InvalidOperationException("Current token is not an OperationToken");
        }
        return opToken;
    }
    
    bool IsOperationWithPriority(Priority priority)
    {
        return currentTokenIndex < tokens.Count &&
               tokens[currentTokenIndex] is OperationToken opToken &&
               opToken.Operation.Priority == priority;
    }

    }
    
    

}

