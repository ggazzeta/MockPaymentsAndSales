using MockPaymentsAndSales.Gateways.Interfaces;
using MockPaymentsAndSales.ReturnObjects;
using System.Text.Json;

namespace MockPaymentsAndSales.Gateways
{
    public class SalesGateway : ISalesGateway
    {
        /// <inheritdoc cref="ISalesGateway.ReturnAllSales(int, DateTime, DateTime)"/>
        public async Task<IReadOnlyCollection<ReturnSale>> ReturnAllSales(int salesAmount, DateTime startTime, DateTime endTime)
        {
            ILLMResponseGateway lLMResponseGateway = new OllamaResponseGateway();
            string jsonString = await lLMResponseGateway.ReturnJsonFromLLMResponse(salesAmount, startTime, endTime);
            return DeserializeReturnJson(jsonString).ToList();
        }

        public IEnumerable<ReturnSale> DeserializeReturnJson(string jsonString)
        {
            IEnumerable<ReturnSale> returnSales = new List<ReturnSale>();

            try
            {
                returnSales = JsonSerializer.Deserialize<IEnumerable<ReturnSale>>(jsonString);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("LLM did not return a valid JSON and could not be formatted.");
            }

            return returnSales;
        }
    }
}
