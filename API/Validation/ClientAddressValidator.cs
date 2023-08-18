using BLL.DTOS.Addresses;
using BLL.DTOS.Client;
using BLL.DTOS.Contacts;
using BLL.DTOS.Emails;
using FluentValidation;
using System.Text.RegularExpressions;

public class ClientAddressValidator : AbstractValidator<ClientAddressCreateDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public ClientAddressValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(ClientAddressCreateDTO => ClientAddressCreateDTO.EmpAddress).Must(BeValidAddress).WithMessage(GetErrorMessage(17)).MaximumLength(50).WithMessage(GetErrorMessage(18)).NotNull().WithMessage(GetErrorMessage(19));
        RuleFor(ClientAddressCreateDTO => ClientAddressCreateDTO.Address_type).Must(x => x >= 1 || x <= 9).WithMessage(GetErrorMessage(20));
    }

    private bool BeValidAddress(string empAddress)
    {
        string pattern = @"^[A-Za-z]{2}.*\d$";
        return Regex.IsMatch(empAddress, pattern);
    }

    private string GetErrorMessage(int errorId)
    {
        return _dbContext.Error_list.FirstOrDefault(e => e.EId == errorId)?.Message;
    }
}
