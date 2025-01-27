namespace MockPaymentsAndSales.ValueObjects
{
    public class Sale : BaseVOModel
    {
        public decimal total_price { get; set; }
        public decimal shipping_price { get; set; }
        public string? customer_id { get; set; }
        public int status { get; set; }
        public DateTime sale_date { get; set; }

        public Sale()
        {
            
        }

        public Sale(string id)
        {
            this.id = id;
        }
    }
}
