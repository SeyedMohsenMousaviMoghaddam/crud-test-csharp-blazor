using Mc2.CrudTest.Shared.Application.Common.Interfaces;
using Mc2.CrudTest.Shared.Application.Common.Models;
using Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomer;
using Mc2.CrudTest.Shared.Application.Helper;
using Mc2.CrudTest.Shared.Domain.Entities;
using Mc2.CrudTest.Shared.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomers;


public record GetCustomersQuery(int page, string? name) : IRequest<PagedResult<CustomerDto>>;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, PagedResult<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<CustomerDto>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        int pageSize = 5;

        if (request.name != null && request.name.Trim() != "")
        {
            return _context.Customers
                .Where(p => !p.IsDeleted && (p.Firstname!.Equals(request.name) ||
                p.Lastname!.Equals(request.name)))
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Id)
                .GetPaged(request.page, pageSize);
        }
        else
        {
            return _context.Customers
                .Where(p=> !p.IsDeleted)
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .OrderBy(p => p.Id)
                .GetPaged(request.page, pageSize);
        }
    }
}
