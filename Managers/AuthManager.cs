using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using FocusTravelApp.Http.Responses;
using Newtonsoft.Json;

namespace FocusTravelApp.Managers;

using Http.Tasks;

public class AuthManager
{
    private string _accountToken;
    
    private const string _apiUrl = "https://testprzemek.nl/api/";

    public AuthManager()
    {
        _accountToken = AppSettings.UserToken;
    }

    public bool IsLoggedIn()
    {
        return !string.IsNullOrEmpty(_accountToken);
    }

    public string GetToken()
    {
        return _accountToken;
    }

    private Action<bool, string> _callback;
    public void LoginAsync(string email, string password, Action<bool, string> callback)
    {
        _callback = callback;
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
                
                Debug.WriteLine($"Logging in user: {email}, {password}");

                // Convert request body to JSON string
                var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                
                Debug.WriteLine(jsonBody);

                // Create a StringContent with the JSON body
                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                Debug.WriteLine(content.ToString());

                // Make the POST request
                var url = $"{_apiUrl}user/login";
                Debug.WriteLine("Sending request to: " + url);
                var response = await httpClient.PostAsync(url, content);

                var responseData = await response.Content.ReadAsStringAsync();
                Debug.WriteLine("Response data: " + responseData);
                
                dynamic? responseJson = JsonConvert.DeserializeObject(responseData);
                if (responseJson == null)
                {
                    Debug.WriteLine("Got null response");
                    _callback(false, "Unknown error");
                    return;
                }

                var isSuccess = response.IsSuccessStatusCode;
                string message = responseJson.message ?? "Unknown error";
                string token = responseJson.token ?? "";
                
                Debug.WriteLine("Got response: " + message);
                Debug.WriteLine("Got token: " + token);
                
                if (isSuccess)
                {
                    SaveToken(token);
                }
                _callback(isSuccess, message);// Does not work - Exception has been thrown by the target of an invocation.
            }
        });
        t.Start();
    }

    public void RegisterAsync(string username, string password)
    {
        // TODO: Call API to register
    }

    public void LogoutAsync()
    {
        // TODO: Call API to logout

        SaveToken(string.Empty);
    }

    private void SaveToken(string token)
    {
        _accountToken = token;
        AppSettings.UserToken = token;
    }
}