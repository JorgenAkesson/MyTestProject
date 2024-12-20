namespace CompanyApi.Models;

public class AccountDTO
{
    public int id { get; set; }
    public string name { get; set; }
    public bool isComplete { get; set; }
    public ICollection<OrderDTO>? orders { get; set; }

    public static AccountDTO AccountToDTO(Account account) =>
    new AccountDTO
    {
        id = account.Id,
        name = account.Name,
        orders = account.Orders.Select(OrderDTO.OrderToDTO).ToList(),
    };

    public static Account DTOToAccount(AccountDTO dto) =>
        new Account
        {
            Id = dto.id,
            Name = dto.name,
            IsComplete = dto.isComplete,
            Orders = dto.orders?.Select(OrderDTO.DTOToOrder).ToList()
        };
}