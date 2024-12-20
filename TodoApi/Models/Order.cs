namespace CompanyApi.Models;

public class Order
{
    public int Id { get; set; }
    public string OrderName { get; set; }
    public int AccountId { get; set; }
    public Account? Account { get; set; } = null;
}
