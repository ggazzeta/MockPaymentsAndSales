namespace MockPaymentsAndSales.ValueObjects
{
    public class Payments : BaseVOModel
    {
        public required string sales_id { get; set; }
        public required string description { get; set; }
        public required PaymentMethod payment_method { get; set; }
        public decimal? price { get; set; }
        public decimal? paid_price { get; set; }
        public DateTime? payment_date { get; set; }
    }
}
