using Microsoft.Extensions.AI;
using MockPaymentsAndSales.Gateways.Interfaces;
using MockPaymentsAndSales.Helpers;
using MockPaymentsAndSales.ValueObjects;

namespace MockPaymentsAndSales.Gateways
{
    public class OllamaResponseGateway : ILLMResponseGateway
    {
        OllamaConfiguration _ollamaConfiguration = new OllamaConfiguration();
        public OllamaResponseGateway()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            _ollamaConfiguration.LocalHost = configuration["Ollama:URL"];
            _ollamaConfiguration.Model = configuration["Ollama:Model"];
        }

        public async Task<string> ReturnJsonFromLLMResponse(int salesAmount, DateTime startTime, DateTime endTime)
        {
            IChatClient chatClient =
                new OllamaChatClient(new Uri(_ollamaConfiguration.LocalHost), _ollamaConfiguration.Model);

            string formattedPrompt = LLMFunctions.ReturnPromptOfSales(salesAmount, startTime, endTime, LLMFunctions.MockSaleObject());
            var chatResponse = await chatClient.GetResponseAsync(formattedPrompt);
            var llmReturn = LLMFunctions.TryExtractJson(chatResponse.Message.Text);

            if (!llmReturn.IsValidJson)
                throw new InvalidOperationException("LLM did not return a valid JSON and could not be formatted.");

            return chatResponse.Message.Text;
        }
    }
}
