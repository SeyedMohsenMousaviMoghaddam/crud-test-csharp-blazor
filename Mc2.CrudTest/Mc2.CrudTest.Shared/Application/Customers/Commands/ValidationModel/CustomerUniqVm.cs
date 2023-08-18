using Mc2.CrudTest.Shared.Application.Common.Models;

namespace Mc2.CrudTest.Shared.Application.Customers.Commands;

public class CustomerUniqVm
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public DateTimeOffset DateOfBirth { get; set; }
}
