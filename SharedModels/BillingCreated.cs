namespace SharedModels
{
    public interface BillingCreated
    {
        int Id { get; set; }
        string BillingName { get; set; }
        decimal Price { get; set; }
        int Quantity { get; set; }
    }
}
