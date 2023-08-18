using BLL.DTOS.Client;
using BLL.DTOS.Contacts;
using BLL.DTOS.Emails;
using FluentValidation;
using System.Text.RegularExpressions;

public class ClientContactValidator : AbstractValidator<ClientContactCreateDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public ClientContactValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(ClientContactCreateDTO => ClientContactCreateDTO.EmpContact).NotNull().WithMessage(GetErrorMessage(14)).Must(ValidNumber).WithMessage(GetErrorMessage(15));
        RuleFor(ClientContactCreateDTO => ClientContactCreateDTO.Contact_type).Must(x => x >= 1 || x <= 9).WithMessage(GetErrorMessage(16));
    }
    private bool ValidNumber(string empContact)
    {
        string pattern = @"^0\d{8}$"; 
        return Regex.IsMatch(empContact, pattern);
    }

    private string GetErrorMessage(int errorId)
    {
        return _dbContext.Error_list.FirstOrDefault(e => e.EId == errorId)?.Message;
    }
}
