using System;

namespace TestApp.BuildingBlocks.Application
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string description) :base(description)
        {
        }
    }
}