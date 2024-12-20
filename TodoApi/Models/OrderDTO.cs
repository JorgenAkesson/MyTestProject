using Newtonsoft.Json;
using System.Diagnostics;

namespace CompanyApi.Models;

public class OrderDTO
{
    public int Id { get; set; }
    public string OrderName { get; set; }
    public int AccountId { get; set; }
    public AccountDTO? Account { get; set; }

    public static OrderDTO OrderToDTO(Order order, bool stop = false) =>
        new OrderDTO
        {
            Id = order.Id,
            OrderName = order.OrderName,
            Account = order.Account != null && !stop ? AccountDTO.AccountToDTO(order.Account, true) : null
        };
    
    public static Order DTOToOrder(OrderDTO dto) =>
        new Order
        {
            Id = dto.Id,
            OrderName = dto.OrderName,
            Account = dto.Account != null ? AccountDTO.DTOToAccount(dto.Account) : null
        };

}