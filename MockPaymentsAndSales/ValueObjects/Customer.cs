namespace MockPaymentsAndSales.ValueObjects
{
    public class Customer : BaseVOModel
    {
        public string customer_name { get; set; }
        public string social_number { get; set; }
        public string? address_line_1 { get; set; }
        public string? address_line_2 { get; set; }
        public string? street_number { get; set; }
        public string? city { get; set; }
        public string? country { get; set; }
        public string? state { get; set; }
        public string? postal_code { get; set;}
    }
}
