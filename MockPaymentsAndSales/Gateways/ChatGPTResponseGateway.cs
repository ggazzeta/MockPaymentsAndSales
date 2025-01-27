using MockPaymentsAndSales.Gateways.Interfaces;
using MockPaymentsAndSales.Helpers;
using System.Text.Json;
using System.Text;

namespace MockPaymentsAndSales.Gateways
{
    public class ChatGPTResponseGateway : ILLMResponseGateway
    {
        private readonly string apiUrl = "https://api.openai.com/v1/chat/completions";
        private readonly string apiKey;

        public ChatGPTResponseGateway()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            apiKey = configuration["OpenAI:ApiKey"];

            if (string.IsNullOrEmpty(apiKey))
                throw new InvalidOperationException("API key not found in appsettings.json.");
        }

        public async Task<string> ReturnJsonFromLLMResponse(int salesAmount, DateTime startTime, DateTime endTime)
        {
            string formattedPrompt = LLMFunctions.ReturnPromptOfSales(salesAmount, startTime, endTime);
            string messageReturned = await GetChatCompletionAsync(formattedPrompt);
            var llmReturn = LLMFunctions.TryExtractJson(messageReturned);

            if (!llmReturn.IsValidJson)
                throw new InvalidOperationException("LLM did not return a valid JSON and could not be formatted.");

            return messageReturned;
        }

        public async Task<string> GetChatCompletionAsync(string userPrompt)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                        new { role = "system", content = "You are a JSON generator. Respond only with JSON." },
                        new { role = "user", content = userPrompt }
                    },
                    max_tokens = 2048,
                    temperature = 0.0
                    //seed = seed -- Seed didn't work properly, rather stick with temp 0. Stil in beta I guess.
                };

                string jsonPayload = JsonSerializer.Serialize(requestBody);
                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

                    using JsonDocument doc = JsonDocument.Parse(responseBody);
                    string assistantResponse = doc.RootElement
                        .GetProperty("choices")[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();

                    return assistantResponse ?? "";
                }
                catch (HttpRequestException ex)
                {
                    return $"Request error: {ex.Message}";
                }
            }
        }

    }
}
