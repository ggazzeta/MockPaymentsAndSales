namespace MockPaymentsAndSales.Gateways.Interfaces
{
    public interface ILLMResponseGateway
    {
        Task<string> ReturnJsonFromLLMResponse(int salesAmount, DateTime startTime, DateTime endTime);
    }
}
