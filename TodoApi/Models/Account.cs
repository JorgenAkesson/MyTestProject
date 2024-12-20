namespace CompanyApi.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsComplete { get; set; }
    public ICollection<Order>? Orders { get; set; }

}