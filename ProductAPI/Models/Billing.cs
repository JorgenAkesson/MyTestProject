﻿namespace BillingAPI.Models
{
    public class Billing
    {
        public int Id { get; set; }
        public string BillingName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
