﻿namespace TestApp.BuildingBlocks.Domain
{
    public interface IBusinessRule
    {
        bool IsBroken();
    
        string Message { get; }
    }
}