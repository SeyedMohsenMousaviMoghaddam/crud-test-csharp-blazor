using IbanNet.FluentValidation;
using IbanNet;
using Mc2.CrudTest.Shared.Application.Common.Interfaces;

namespace Mc2.CrudTest.Shared.Application.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;
    public UpdateCustomerCommandValidator(IApplicationDbContext context, IIbanValidator ibanValidator)
    {
        _context = context;

        RuleFor(x => x.BankAccountNumber).NotNull().Iban(ibanValidator, strict: false);

        RuleFor(customer => customer.Email).EmailAddress()
                .WithMessage("Email is not valid.")
                .WithErrorCode("Valid"); ;
        RuleFor(x => x.PhoneNumber)
            .MustAsync(BeValidPhoneNumber)
            .WithMessage("PhoneNumber is not valid.")
            .WithErrorCode("Valid");

        RuleFor(v => v.Firstname)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.Lastname)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(v => v.DateOfBirth)
            .NotEmpty();

        RuleFor(v => new CustomerUniqVm { Firstname=v.Firstname,Lastname= v.Lastname ,DateOfBirth=v.DateOfBirth})
            .MustAsync(BeUniqueFirstnameLastNameDateOfBirth)
                .WithMessage("Firstname and Lastname and DateOfBirth must be unique.")
                .WithErrorCode("Unique");

        RuleFor(v => v.Email)
            .NotEmpty()
            .MaximumLength(200)
            .MustAsync(BeUniqueEmail)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique"); 

    }

    public async Task<bool> BeUniqueFirstnameLastNameDateOfBirth(UpdateCustomerCommand model, CustomerUniqVm uniqModel, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Firstname != uniqModel.Firstname && l.Lastname != uniqModel.Lastname && l.DateOfBirth != uniqModel.DateOfBirth, cancellationToken);
    }

    public async Task<bool> BeUniqueEmail(UpdateCustomerCommand model, string email, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .Where(l => l.Id != model.Id)
            .AllAsync(l => l.Email != email, cancellationToken);
    }
    public async Task<bool> BeValidPhoneNumber(string? phone, CancellationToken cancellationToken)
    {
        if (phone == null)
        {
            return true;
        }
        try
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(phone, null);
            return phoneNumber.HasNationalNumber;
        }
        catch
        {
            return false;
        }
    }
}
