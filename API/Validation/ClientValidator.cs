using BLL.DTOS.Client;
using FluentValidation;
using System.Text.RegularExpressions;


public class ClientValidator : AbstractValidator<ClientCreateDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public ClientValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;


        RuleFor(ClientCreateDTO => ClientCreateDTO.EmpName).Must(BeValidName).WithMessage(GetErrorMessage(3)).NotNull().WithMessage(GetErrorMessage(4)).MaximumLength(50).WithMessage(GetErrorMessage(5));
        RuleFor(ClientCreateDTO => ClientCreateDTO.EmpSurname).Must(BeValidName).WithMessage(GetErrorMessage(6)).NotNull().WithMessage(GetErrorMessage(7)).MaximumLength(50).WithMessage(GetErrorMessage(8));
        RuleFor(ClientCreateDTO => ClientCreateDTO.EmpSurname).Must(BeValidName).WithMessage(GetErrorMessage(9)).Length(1, 50).WithMessage(GetErrorMessage(10));
        RuleFor(ClientCreateDTO => ClientCreateDTO.EmpSex).Must(x => x == 1 || x == 2).WithMessage(GetErrorMessage(21));
    }

    private bool BeValidName(string name)
    {
        string pattern = @"^[A-Za-z\s]+$";
        return Regex.IsMatch(name, pattern);
    }
    private string GetErrorMessage(int errorId)
    {
        return _dbContext.Error_list.FirstOrDefault(e => e.EId == errorId)?.Message;
    }
}
