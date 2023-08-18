using BLL.DTOS.Client;
using BLL.DTOS.Emails;
using FluentValidation;

public class ClientEmailValidator : AbstractValidator<ClientEmailCreateDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public ClientEmailValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(ClientEmailCreateDTO => ClientEmailCreateDTO.EmpEmail).EmailAddress().WithMessage(GetErrorMessage(11)).MaximumLength(50).WithMessage(GetErrorMessage(12));
        RuleFor(ClientEmailCreateDTO => ClientEmailCreateDTO.Email_Type).Must(x => x >= 1 || x <= 9).WithMessage(GetErrorMessage(13));
    }
    private string GetErrorMessage(int errorId)
    {
        return _dbContext.Error_list.FirstOrDefault(e => e.EId == errorId)?.Message;
    }
}
