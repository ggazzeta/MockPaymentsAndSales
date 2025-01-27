namespace MockPaymentsAndSales.ValueObjects
{
    public class PaymentMethod
    {
        public required string type { get; set; }
        public string? description { get; set; }
        public string? card_network { get; set; }
        public string? card_last_numbers { get; set; }

    }
}
