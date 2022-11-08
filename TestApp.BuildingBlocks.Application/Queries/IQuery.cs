﻿using MediatR;

namespace TestApp.BuildingBlocks.Application.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}