using Mc2.CrudTest.Shared.Application.Common.Interfaces;
using Mc2.CrudTest.Shared.Domain.Entities;
using Mc2.CrudTest.Shared.Domain.Enums;

namespace Mc2.CrudTest.Shared.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : IRequest<int>
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? BankAccountNumber { get; set; }
    public byte Gender { get; set; } = 0;
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Customer();

        entity.Firstname = request.Firstname;
        entity.Lastname = request.Lastname;
        entity.DateOfBirth = request.DateOfBirth;
        entity.PhoneNumber = request.PhoneNumber;
        entity.Email = request.Email;
        entity.Gender = (GenderEnum)request.Gender;
        entity.BankAccountNumber = request.BankAccountNumber;

        _context.Customers.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
