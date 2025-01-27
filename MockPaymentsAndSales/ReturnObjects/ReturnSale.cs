using MockPaymentsAndSales.ValueObjects;

namespace MockPaymentsAndSales.ReturnObjects
{
    public class ReturnSale
    {
        public Sale Sale { get; set; }
        public Customer? Customer { get; set; }
        public List<Payments>? Payments { get; set; }
        public ReturnSale()
        {

        }

        public ReturnSale(Sale sale, Customer? customer, List<Payments>? payments)
        {
            Sale = sale;
            Customer = customer;
            Payments = payments;
        }
    }
}
