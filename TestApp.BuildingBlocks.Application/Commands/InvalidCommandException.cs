using System;
using System.Collections.Generic;

namespace TestApp.BuildingBlocks.Application.Commands
{
    public class InvalidCommandException : Exception
    {
        public List<string> Errors { get; }

        public InvalidCommandException(List<string> errors)
        {
            Errors = errors;
        }
    }
}