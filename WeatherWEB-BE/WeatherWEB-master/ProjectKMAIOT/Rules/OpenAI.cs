using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAI_API;
using OpenAI_API.Completions;
using System.Text;

namespace ProjectKMAIOT.Rules
{
    public class OpenAI
    {
        private static HttpClient Http = new HttpClient();
        public async Task<string> GenerateLoremIpsum()
        {
            var apiKey = "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";
            Http.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            // JSON content for the API call
            var jsonContent = new
            {
                prompt = "Give me some lorem ipsum text",
                model = "text-davinci-003",
                max_tokens = 1000
            };

            // Make the API call
            var responseContent = await Http.PostAsync("https://api.openai.com/v1/completions", new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json"));

            // Read the response as a string
            var resContext = await responseContent.Content.ReadAsStringAsync();

            // Deserialize the response into a dynamic object
            var data = JsonConvert.DeserializeObject<dynamic>(resContext);
            return data.choices[0].text;
        }
    }
}
