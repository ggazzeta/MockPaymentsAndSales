using MockPaymentsAndSales.ReturnObjects;
using MockPaymentsAndSales.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MockPaymentsAndSales.Helpers
{
    public static class LLMFunctions
    {
        public static string ReturnPromptOfSales(int salesAmount, DateTime startDate, DateTime endDate) =>
            $"You will randomly generate me {salesAmount} JSON files that represent the SALES containing the following structure:\n{ReturnJsonFormattedOfType<Sale>()}\n" +
            $"Each SALE will have to be between the dates: {startDate.ToString("yyyyMMdd")} and {endDate.ToString("yyyyMMdd")}\n" +
            $"Then, for each of the sales, I need a CUSTOMER structure:\n{ReturnJsonFormattedOfType<Customer>()}\n" +
            $"Then I'll need you to come up with a random amount of Payments for each SALE. It needs to have at least ONE, but can't surpass TEN payments.\nThe payment methods are:\r\nCREDIT, DEBIT, CASH\nThe structure goes like this:\n" +
            $"{ReturnJsonFormattedOfType<Payments>()}. The payment date is optional. The Payment description should be something like \"VISA CREDIT\", \"MASTER DEBIT\" and so on and so forth " +
            $"\nRemember, only provide me the RFC8259 compliant JSON response, with no comments. And don't use the Python Shell. Just create it in a generative fashion. For each sale, unite the JSON for the customers and payments on each sale instead of separting them.";


        public static string ReturnPromptOfSales(int salesAmount, DateTime startDate, DateTime endDate, List<ReturnSale> mockSale) => $$"""
    You are an automated JSON generator. You MUST generate exactly {{salesAmount}} JSON objects following the structure below:
    {{JsonSerializer.Serialize(mockSale)}}
    
    - Each sale must have a date between {{startDate:yyyyMMdd}} and {{endDate:yyyyMMdd}}.
    - Each sale must have at least ONE and at most TEN payments.
    - Payment methods must be one of the following: "CREDIT", "DEBIT", or "CASH".
    - Payment descriptions should be in the format "VISA CREDIT", "MASTER DEBIT", etc.
    - The payment date is optional.

    # STRICT OUTPUT FORMAT REQUIREMENTS:
    - You MUST return **ONLY** a valid RFC8259-compliant JSON output.
    - NO explanations, introductions, or extra text—ONLY the JSON.
    - The JSON MUST be **valid and properly formatted**. It **must** close all brackets `{}` and square brackets `[]` correctly.
    - All sales must be inside **ONE SINGLE JSON ARRAY** like this:  
      ✅ `[ {...}, {...}, {...} ]`  
      ❌ **Do NOT** generate nested arrays like this: `[ [{...}], [{...}] ]`
    - Your response MUST be fully parseable by `JSON.parse()` without any syntax errors.
    - Do NOT wrap JSON in triple backticks, markdown, or any formatting.
    - Do NOT write "Here is your JSON" or any similar phrase.

    FAILURE TO FOLLOW THESE RULES WILL RESULT IN REJECTION.
    """;




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
                return (false, null);

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

        public static List<ReturnSale> MockSaleObject()
        {
            List<ReturnSale> returnSales = new List<ReturnSale>();
            List<Payments> payments = MockPaymentList();

            var returnSale = new ReturnSale
            {
                Sale = new Sale()
                {
                    id = "965f2df6-7f46-4da2-acd3-9e0970297ab3",
                    total_price = 249.99M,
                    customer_id = "f272baeb-4cd0-4556-96c7-71d4da651a15",
                    sale_date = DateTime.Now,
                    shipping_price = 5M,
                    status = 0
                },
                Customer = new Customer()
                {
                    customer_name = "John Doe",
                    social_number = "123 055 233",
                    address_line_1 = "John Street",
                    address_line_2 = "St John",
                    street_number = "123",
                    city = "Los Angeles",
                    country = "USA",
                    state = "California",
                    postal_code = "123333",
                    id = "f272baeb-4cd0-4556-96c7-71d4da651a15",
                },
                Payments = payments
            };

            returnSales.Add(returnSale);
            return returnSales;
        }

        public static List<Payments> MockPaymentList()
        {
            var payments = new List<Payments>();

            payments.Add(new Payments()
            {
                description = "VISA CREDIT",
                id = "1",
                payment_method = new PaymentMethod()
                {
                    type = "CREDIT",
                    description = "CREDIT CARD",
                    card_last_numbers = "1234",
                    card_network = "VISA"
                },
                sales_id = "965f2df6-7f46-4da2-acd3-9e0970297ab3",
                paid_price = 0,
                payment_date = DateTime.Now.AddDays(3),
                price = 83.33M
            });
            payments.Add(new Payments()
            {
                description = "MASTERCARD DEBIT",
                id = "2",
                payment_method = new PaymentMethod()
                {
                    type = "DEBIT",
                    description = "DEBIT CARD",
                    card_last_numbers = "1234",
                    card_network = "MASTERCARD"
                },
                sales_id = "965f2df6-7f46-4da2-acd3-9e0970297ab3",
                paid_price = 166.66M,
                payment_date = DateTime.Now,
                price = 166.66M
            });

            return payments;
        }
    }
}
