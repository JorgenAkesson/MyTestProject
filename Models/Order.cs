namespace MyTestProject.Models
{
    public class Order
    {
        public int ID { get; set; }
        public DateOnly Date { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
