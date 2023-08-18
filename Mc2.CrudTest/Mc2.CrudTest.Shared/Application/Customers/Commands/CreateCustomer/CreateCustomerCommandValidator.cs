using FluentValidation;
using IbanNet;
using IbanNet.FluentValidation;
using Mc2.CrudTest.Shared.Application.Common.Interfaces;
using PhoneNumbers;

namespace Mc2.CrudTest.Shared.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandValidator(IApplicationDbContext context, IIbanValidator ibanValidator)
    {
        _context = context;

        RuleFor(x => x.BankAccountNumber).NotNull().Iban(ibanValidator, strict: false)
            .WithMessage("BankAccountNumber is not valid.")
                .WithErrorCode("Valid");

        RuleFor(customer => customer.Email).EmailAddress()
                .WithMessage("Email is not valid.")
                .WithErrorCode("Valid");

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

        RuleFor(v => new CustomerUniqVm { Firstname = v.Firstname, Lastname = v.Lastname, DateOfBirth = v.DateOfBirth })
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

    public async Task<bool> BeUniqueFirstnameLastNameDateOfBirth(CustomerUniqVm uniqModel, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .AllAsync(l => l.Firstname != uniqModel.Firstname && l.Lastname != uniqModel.Lastname && l.DateOfBirth != uniqModel.DateOfBirth, cancellationToken);
    }

    public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
    {
        return await _context.Customers
            .AllAsync(l => l.Email != email, cancellationToken);
    }
    public async Task<bool> BeValidPhoneNumber(string? phone, CancellationToken cancellationToken)
    {
        if(phone == null)
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