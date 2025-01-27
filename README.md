# MockPaymentsAndSales

## Version: 1.0

This is a simple Web API built in C# that generates mock data for sales, including a customer for each sale and payments. The mock data is generated dynamically using the ChatGPT completions API.

## Features

- Generates a list of sales.
- Each sale includes customer information and associated payments.
- Uses OpenAI's GPT-3 (ChatGPT) API to generate data.

## Getting Started

Follow the steps below to get the API up and running locally:

### Prerequisites

- .NET 6 or higher
- An OpenAI API Key (to use ChatGPT completions API)

### 1. Clone the Repository
### 2. Set Up Your API Key
- In the appsettings.json file, replace {YOUR_KEY_HERE} with your OpenAI API key:

```json
{
  "OpenAI": {
    "ApiKey": "{YOUR_KEY_HERE}"
  }
}
```

Have fun, and feel free to improve upon this code.
