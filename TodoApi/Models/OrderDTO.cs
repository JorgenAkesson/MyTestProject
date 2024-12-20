namespace CompanyApi.Models;

public class OrderDTO
{
    public int id { get; set; }
    public string name { get; set; }
    public AccountDTO account { get; set; }

    public static OrderDTO OrderToDTO(Order order) =>
        new OrderDTO
        {
            id = order.Id,
            name = order.Name,
            account = AccountDTO.AccountToDTO(order.Account)
        };
    
    public static Order DTOToOrder(OrderDTO dto) =>
        new Order
        {
            Id = dto.id,
            Name = dto.name,
            Account = AccountDTO.DTOToAccount(dto.account)
        };

}