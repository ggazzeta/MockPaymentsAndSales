using MockPaymentsAndSales.Gateways.Interfaces;
using MockPaymentsAndSales.ReturnObjects;
using System.Text.Json;

namespace MockPaymentsAndSales.Gateways
{
    public class SalesGateway : ISalesGateway
    {
        public async Task<List<ReturnSale>> ReturnAllSales(int salesAmount, DateTime startTime, DateTime endTime)
        {
            ILLMResponseGateway lLMResponseGateway = new OllamaResponseGateway();
            string jsonString = await lLMResponseGateway.ReturnJsonFromLLMResponse(salesAmount, startTime, endTime);
            return DeserializeReturnJson(jsonString);
        }

        public List<ReturnSale> DeserializeReturnJson(string jsonString)
        {
            List<ReturnSale> returnSales = new List<ReturnSale>();

            try
            {
                returnSales = JsonSerializer.Deserialize<List<ReturnSale>>(jsonString);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("LLM did not return a valid JSON and could not be formatted.");
            }

            return returnSales;
        }
    }
}
