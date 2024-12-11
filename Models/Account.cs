namespace MyTestProject.Models
{
    public class Account
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
