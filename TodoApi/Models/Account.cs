using Newtonsoft.Json;

namespace CompanyApi.Models;

public class Account
{
    public int Id { get; set; }
    public string AccountName { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();

}