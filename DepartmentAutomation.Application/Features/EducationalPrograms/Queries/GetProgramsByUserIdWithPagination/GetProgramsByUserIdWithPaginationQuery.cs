using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using DepartmentAutomation.Application.Common.Extensions;
using DepartmentAutomation.Application.Common.Interfaces;
using DepartmentAutomation.Application.Common.Mappings;
using DepartmentAutomation.Application.Common.Models;
using DepartmentAutomation.Application.Contracts.Requests;
using DepartmentAutomation.Application.Contracts.Requests.Filters;
using DepartmentAutomation.Application.Contracts.Responses.BriefDtos;
using DepartmentAutomation.Application.PredicateFactories;
using DepartmentAutomation.Domain.Entities;
using MediatR;

namespace DepartmentAutomation.Application.Features.EducationalPrograms.Queries.GetProgramsByUserIdWithPagination
{
    public class GetProgramsByUserIdWithPaginationQuery : IRequest<PaginatedList<EducationalProgramBriefDto>>
    {
        public GetProgramsByUserIdWithPaginationQuery(PaginationRequest request,
            EducationalProgramsFilterDto filter)
        {
            Filter = filter;
            PageNumber = request.PageNumber;
            PageSize = request.PageSize;
        }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public EducationalProgramsFilterDto Filter { get; set; }
    }

    public class GetProgramsByUserIdWithPaginationHandle :
        IRequestHandler<GetProgramsByUserIdWithPaginationQuery, PaginatedList<EducationalProgramBriefDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExpressionBuilder<EducationalProgram, EducationalProgramsFilterDto> _expressionBuilder;

        public GetProgramsByUserIdWithPaginationHandle(IApplicationDbContext context, IMapper mapper,
            IExpressionBuilder<EducationalProgram, EducationalProgramsFilterDto> expressionBuilder)
        {
            _context = context;
            _mapper = mapper;
            _expressionBuilder = expressionBuilder;
        }

        public async Task<PaginatedList<EducationalProgramBriefDto>> Handle(
            GetProgramsByUserIdWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var predicate = _expressionBuilder.Build(request.Filter);

            var predicateQuery = predicate is not null
                ? _context.EducationalPrograms.Where(predicate)
                : _context.EducationalPrograms;

            return await predicateQuery
                .OrderByDynamic(request.Filter.PropertyName, request.Filter.SortDirection)
                .ProjectTo<EducationalProgramBriefDto>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);
        }
    }
}