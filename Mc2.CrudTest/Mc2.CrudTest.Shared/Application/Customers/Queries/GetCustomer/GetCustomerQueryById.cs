using Mc2.CrudTest.Shared.Application.Common.Interfaces;
using Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomers;

namespace Mc2.CrudTest.Shared.Application.Customers.Queries.GetCustomer;
public record GetCustomerQueryById(int Id) : IRequest<CustomerDto>;

public class GetCustomerQueryByIdHandler : IRequestHandler<GetCustomerQueryById,CustomerDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerQueryByIdHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(GetCustomerQueryById request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
                .FirstOrDefaultAsync(p => p.Id == request.Id,cancellationToken);
        Guard.Against.NotFound(request.Id, entity);

        var mapResult = _mapper.Map<CustomerDto>(entity);
        return mapResult;
    }
}
