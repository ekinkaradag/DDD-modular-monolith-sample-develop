using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using IoCore.SharedReadKernel;
using IoCore.SharedReadKernel.RequestForChange;
using Microsoft.EntityFrameworkCore;
using TestApp.BuildingBlocks.Application.Queries;

namespace TestApp.Module.Rfc.Application.GetRfcOverview
{
    internal class GetAllRfcsQueryHandler : IQueryHandler<GetAllRfcsQuery, IEnumerable<RfcOverviewDto>>
    {
        private static readonly IMapper Mapper;
    
        private readonly IReadModelAccess _readModelAccess;

        static GetAllRfcsQueryHandler()
        {
            Mapper = ConfigureMapper();
        }
    
        public GetAllRfcsQueryHandler(IReadModelAccess readModelAccess)
        {
            _readModelAccess = readModelAccess;
        }

        private static IMapper ConfigureMapper()
        {
            var configuration = new MapperConfiguration(configurationExpression =>
                configurationExpression.CreateMap<RequestForChange, RfcOverviewDto>());
        
            return configuration.CreateMapper();
        }

        public async Task<IEnumerable<RfcOverviewDto>> Handle(GetAllRfcsQuery request,
            CancellationToken cancellationToken)
        {
            var requestForChangeSummaryDtos =
                await _readModelAccess
                    .Get<RequestForChange>()
                    .ProjectTo<RfcOverviewDto>(Mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken: cancellationToken);

            return requestForChangeSummaryDtos;
        }
    }
}