using Mc2.CrudTest.Shared.Application.Common.Interfaces;
using Mc2.CrudTest.Shared.Application.Common.Models;
using Mc2.CrudTest.Shared.Domain.Enums;

namespace Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomers;


public record GetCustomersQuery : IRequest<CustomersVm>;

public class GetCustomersQueryHandler : IRequestHandler<GetCustomersQuery, CustomersVm>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomersVm> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        return new CustomersVm
        {
            Genders = Enum.GetValues(typeof(GenderEnum))
                .Cast<GenderEnum>()
                .Select(p => new LookupDto { Id = (int)p, Title = p.ToString() })
                .ToList(),

            Lists = await _context.Customers
                .AsNoTracking()
                .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
                .OrderBy(t => t.Lastname)
                .ToListAsync(cancellationToken)
        };
    }
}
