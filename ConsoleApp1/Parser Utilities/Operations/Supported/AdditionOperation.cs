﻿namespace ConsoleApp1.Parser_Utilities.Operations.Supported;

public sealed class AdditionOperation : IOperation
{
    public OperationPriority OperationPriority => OperationPriority.Low;
    public Func<double, double, double> Execute { get; } = (leftOperand, rightOperand) => leftOperand + rightOperand;
}