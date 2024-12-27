namespace SharedModels
{
    public class BillingCreatedRequest
    {
        int Id { get; set; }
        public string PatientName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
