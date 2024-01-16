using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using FocusTravelApp.Http.Responses;
using Newtonsoft.Json;

namespace FocusTravelApp.Managers;

using Http.Tasks;

public class AuthManager
{
    
    private const string _apiUrl = "https://testprzemek.nl/api/";

    public AuthManager()
    {
    }

    public bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(AppSettings.AccountToken);
    }

    public void LoginAsync(string email, string password, Action<bool, string> callback)
    {
        var t = new Thread(async () =>
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
                var jsonBody = JsonConvert.SerializeObject(requestBody);

                // Create a StringContent with the JSON body
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Make the POST request
                const string url = $"{_apiUrl}user/login";
                var response = await httpClient.PostAsync(url, content);

                var responseData = await response.Content.ReadAsStringAsync();
                
                dynamic? responseJson = JsonConvert.DeserializeObject(responseData);
                if (responseJson == null)
                {
                    Application.Current?.Dispatcher.Dispatch(() =>
                    {
                        callback(false, "Unknown error");
                    });
                    return;
                }

                var isSuccess = response.IsSuccessStatusCode;
                string message = responseJson.message ?? "Unknown error";
                string token = responseJson.token ?? "";
                
                AppSettings.AccountToken = token;
                Application.Current?.Dispatcher.Dispatch(() =>
                {
                    callback(isSuccess, message);
                });
            }
        });
        t.Start();
    }

    public void RegisterAsync(string email, string password, string password_confirm, Action<bool, string> callback)
    {
        var t = new Thread(async () =>
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
                    password,
                    password_confirm
                };

                // Convert request body to JSON string
                var jsonBody = JsonConvert.SerializeObject(requestBody);

                // Create a StringContent with the JSON body
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                // Make the POST request
                const string url = $"{_apiUrl}user/register";
                var response = await httpClient.PostAsync(url, content);

                var responseData = await response.Content.ReadAsStringAsync();
                
                dynamic? responseJson = JsonConvert.DeserializeObject(responseData);
                if (responseJson == null)
                {
                    Application.Current?.Dispatcher.Dispatch(() =>
                    {
                        callback(false, "Unknown error");
                    });
                    return;
                }

                var isSuccess = response.IsSuccessStatusCode;
                string message = responseJson.message ?? "Unknown error";
                string token = responseJson.token ?? "";
                
                AppSettings.AccountToken = token;
                Application.Current?.Dispatcher.Dispatch(() =>
                {
                    callback(isSuccess, message);
                });
            }
        });
        t.Start();
    }

    public void LogoutAsync(Action<bool, string> callback)
    {
        var t = new Thread(async () =>
        {
            // Create a HttpClient instance
            using (HttpClient httpClient = new HttpClient())
            {
                var accountToken = AppSettings.AccountToken;
                
                // Set headers
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                httpClient.DefaultRequestHeaders.Add("X-Account-Token", accountToken);

                // Make the POST request
                const string url = $"{_apiUrl}user";
                var response = await httpClient.DeleteAsync(url);

                var responseData = await response.Content.ReadAsStringAsync();
                
                dynamic? responseJson = JsonConvert.DeserializeObject(responseData);
                if (responseJson == null)
                {
                    Application.Current?.Dispatcher.Dispatch(() =>
                    {
                        callback(false, "Unknown error");
                    });
                    return;
                }

                var isSuccess = response.IsSuccessStatusCode;
                string message = responseJson.message ?? "Unknown error";
                
                if (isSuccess)
                {
                    AppSettings.AccountToken = "";
                }
                Application.Current?.Dispatcher.Dispatch(() =>
                {
                    callback(isSuccess, message);
                });
            }
        });
        t.Start();
    }
    
}