using BLL.DTOS.Addresses;
using BLL.DTOS.Client;
using BLL.DTOS.Contacts;
using BLL.DTOS.Emails;
using BLL.DTOS.Order;
using FluentValidation;

public class OrderValidator : AbstractValidator<OrderCreateDTO>
{
    private readonly ApplicationDbContext _dbContext;

    public OrderValidator(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        RuleFor(OrderCreateDTO => OrderCreateDTO.OrderDescription).MaximumLength(200).WithMessage(GetErrorMessage(1)).NotNull().WithMessage(GetErrorMessage(2));
    }   
    private string GetErrorMessage(int errorId)
    {
        return _dbContext.Error_list.FirstOrDefault(e => e.EId == errorId)?.Message;
    }
}
