using Mc2.CrudTest.Shared.Application.Common.Interfaces;
using Mc2.CrudTest.Shared.Domain.Enums;

namespace Mc2.CrudTest.Shared.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand : IRequest
{
    public int Id { get; init; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? BankAccountNumber { get; set; }
    public byte Gender { get; set; } = 0;
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Firstname = request.Firstname;
        entity.Lastname = request.Lastname;
        entity.DateOfBirth = request.DateOfBirth;
        entity.PhoneNumber = request.PhoneNumber;
        entity.Email = request.Email;
        entity.Gender = (GenderEnum)request.Gender;
        entity.BankAccountNumber = request.BankAccountNumber;

        await _context.SaveChangesAsync(cancellationToken);

    }
}
