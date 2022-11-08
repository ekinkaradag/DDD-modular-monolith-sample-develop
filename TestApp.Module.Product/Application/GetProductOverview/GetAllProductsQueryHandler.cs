using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using IoCore.SharedReadKernel;
using Microsoft.EntityFrameworkCore;
using TestApp.BuildingBlocks.Application.Queries;

namespace TestApp.Module.Product.Application.GetProductOverview
{
    internal class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, IEnumerable<ProductOverviewDto>>
    {
        private static readonly IMapper Mapper;
    
        private readonly IReadModelAccess _readModelAccess;

        static GetAllProductsQueryHandler()
        {
            Mapper = ConfigureMapper();
        }
    
        public GetAllProductsQueryHandler(IReadModelAccess readModelAccess)
        {
            _readModelAccess = readModelAccess;
        }

        private static IMapper ConfigureMapper()
        {
            var configuration = new MapperConfiguration(configurationExpression =>
                configurationExpression.CreateMap<IoCore.SharedReadKernel.Product.Product, ProductOverviewDto>());
        
            return configuration.CreateMapper();
        }

        public async Task<IEnumerable<ProductOverviewDto>> Handle(GetAllProductsQuery request,
            CancellationToken cancellationToken)
        {
            var productSummaryDtos =
                await _readModelAccess
                    .Get<IoCore.SharedReadKernel.Product.Product>()
                    .ProjectTo<ProductOverviewDto>(Mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken: cancellationToken);

            return productSummaryDtos;
        }
    }
}