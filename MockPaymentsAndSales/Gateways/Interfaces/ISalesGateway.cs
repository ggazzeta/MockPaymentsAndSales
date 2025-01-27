using MockPaymentsAndSales.ReturnObjects;

namespace MockPaymentsAndSales.Gateways.Interfaces
{
    public interface ISalesGateway
    {
        Task<List<ReturnSale>> ReturnAllSales(int salesAmount, DateTime startTime, DateTime endTime);
    }
}
