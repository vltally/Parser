﻿using ConsoleApp1.Parser_Utilities.Operations;

namespace ConsoleApp1.Tokenizer;

public class OperationToken(IOperation operation) : Token
{
    public IOperation Operation { get; } = operation;
}