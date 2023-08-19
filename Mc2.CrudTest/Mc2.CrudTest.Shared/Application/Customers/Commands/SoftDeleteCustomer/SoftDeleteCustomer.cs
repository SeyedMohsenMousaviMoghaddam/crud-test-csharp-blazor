using Mc2.CrudTest.Shared.Application.Common.Interfaces;

namespace Mc2.CrudTest.Shared.Application.Customers.Commands.DeleteCustomer;

public record SoftDeleteCustomerCommand(int Id) : IRequest;

public class SoftDeleteCustomerCommandHandler : IRequestHandler<SoftDeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public SoftDeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(SoftDeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.IsDeleted = true;
        entity.Deleted = DateTimeOffset.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
