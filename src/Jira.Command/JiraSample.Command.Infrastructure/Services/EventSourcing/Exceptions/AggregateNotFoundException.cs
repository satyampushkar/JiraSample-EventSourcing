﻿namespace JiraSample.Infrastructure.Services.EventSourcing.Exceptions;

public class AggregateNotFoundException : Exception
{
    public AggregateNotFoundException(string message)
        : base(message)
    {        
    }
}
