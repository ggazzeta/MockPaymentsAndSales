using MockPaymentsAndSales.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MockPaymentsAndSales.Helpers
{
    public static class LLMFunctions
    {
        public static string ReturnPromptOfSales(int salesAmount, DateTime startDate, DateTime endDate) =>
            $"I need {salesAmount} JSON files that represent my SALES that has the following structure:\n{ReturnJsonFormattedOfType<Sale>()}\n" +
            $"Each SALE will have to be between the dates: {startDate.ToString("yyyyMMdd")} and {endDate.ToString("yyyyMMdd")}\n" +
            $"Then, for each of the sales, I need a CUSTOMER structure:\n{ReturnJsonFormattedOfType<Customer>()}\n" +
            $"Then I'll need you to come up with a random amount of Payments for each SALE. It needs to have at least ONE, but can't surpass TEN payments.\nThe payment methods are:\r\nCREDIT, DEBIT, CASH\nThe structure goes like this:\n" +
            $"{ReturnJsonFormattedOfType<Payments>()}. The payment date is optional. The Payment description should be something like \"VISA CREDIT\", \"MASTER DEBIT\" and so on and so forth " +
            $"\nRemember, RETURN ME JUST THE JSON, no comments. And don't use the Python Shell. Just create it in a generative fashion. For each sale, unite the JSON for the customers and payments on each sale instead of separting them.";

        /// <summary>
        /// Serializes a generic type <typeparamref name="T"/> into a JSON string, 
        /// including all properties with default or empty values.
        /// </summary>
        /// <remarks>
        /// Does not work with Lists or Collections
        /// </remarks>
        public static string ReturnJsonFormattedOfType<T>()
        {
            object obj = Activator.CreateInstance<T>();

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.Never
            };

            return JsonSerializer.Serialize(obj, options);
        }

        public static (bool IsValidJson, string? JsonString) TryExtractJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return (false, null);
            }

            try
            {
                var jsonDocument = JsonDocument.Parse(input);
                return (true, input); 
            }
            catch (JsonException)
            {
                return (false, null);
            }
        }
    }
}
