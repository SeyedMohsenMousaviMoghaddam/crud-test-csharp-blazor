using Mc2.CrudTest.Shared.Application.Common.Interfaces;

namespace CleanArchitecture.Application.Customers.Commands.PurgeCustomers;


public record PurgeCustomersCommand : IRequest;

public class PurgeCustomersCommandHandler : IRequestHandler<PurgeCustomersCommand>
{
    private readonly IApplicationDbContext _context;

    public PurgeCustomersCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(PurgeCustomersCommand request, CancellationToken cancellationToken)
    {
        _context.Customers.RemoveRange(_context.Customers);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
