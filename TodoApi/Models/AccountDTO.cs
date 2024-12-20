using Newtonsoft.Json;

namespace CompanyApi.Models;

public class AccountDTO
{
    public int Id { get; set; }
    public string AccountName { get; set; }
    public ICollection<OrderDTO>? Orders { get; set; }

    public static AccountDTO AccountToDTO(Account account, bool stop = false) =>
    new AccountDTO
    {
        Id = account.Id,
        AccountName = account.AccountName,
        Orders = !stop ? account.Orders.Select(x => OrderDTO.OrderToDTO(x, true)).ToList() : null,
    };

    public static Account DTOToAccount(AccountDTO dto) =>
        new Account
        {
            Id = dto.Id,
            AccountName = dto.AccountName,
            Orders = dto.Orders.Select(OrderDTO.DTOToOrder).ToList()
        };
}