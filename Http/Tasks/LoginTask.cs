using System.Net.Http.Headers;
using FocusTravelApp.Http.Responses;

namespace FocusTravelApp.Http.Tasks;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class LoginTask
{
    
    private static readonly string _apiUrl = "https://testprzemek.nl/api/user/login";
    
    public static void Run(string email, string password, Action<LoginResponse> callback)
    {
        var t = new Thread(async () =>
        {
            await Login(email, password, callback);
        });
        t.Start();
    }

    private static async Task Login(string email, string password, Action<LoginResponse> callback)
    {
        // Create a HttpClient instance
        using (HttpClient httpClient = new HttpClient())
        {
            // Set headers
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
            
            // Prepare request body
            var requestBody = new
            {
                email,
                password
            };

            // Convert request body to JSON string
            string jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

            // Create a StringContent with the JSON body
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            // Make the POST request
            HttpResponseMessage response = await httpClient.PostAsync(_apiUrl, content);

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Read and display the response content
                string responseData = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response: " + responseData);
            }
            else
            {
                Console.WriteLine("Error: " + response.StatusCode);
            }
        }
        
    }
    
}