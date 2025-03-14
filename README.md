# MockPaymentsAndSales

## Version: 1.1
(Added Microsoft.Extensions.AI support for Ollama running locally)

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
```bash
git clone https://github.com/ggazzeta/MockPaymentsAndSales
```
### 2. Set Up Your API Key
- In the appsettings.json file, replace {YOUR_KEY_HERE} with your OpenAI API key:

### 3. Set Up Your URL and Model for running Ollama locally.
 - In the appsettings.json file, replace the URL and Model for the ones you're currently using.


```json
{
  "OpenAI": {
    "ApiKey": "{YOUR_KEY_HERE}"
  },
  "Ollama": {
    "URL": "http://localhost:11434",
    "Model": "llama3"
  }
}
```

PS: Ollama stil can't return a JSON consistently.
Deepseek has a problem of returning too many thoughts and writings. Llama can't consistently generate a working JSON.

If you manage to create a prompt that can return the JSON consistently, you can send a PR to this repo.

Have fun, and feel free to improve upon this code.
