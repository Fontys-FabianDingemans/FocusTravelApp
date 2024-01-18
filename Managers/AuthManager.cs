using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;
using FocusTravelApp.Models;
using Newtonsoft.Json;

namespace FocusTravelApp.Managers;

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
    
    public void GetUserAsync(Action<bool, string, User?> callback)
    {
        var t = new Thread(async () =>
        {
            // Create a HttpClient instance
            using (HttpClient httpClient = new HttpClient())
            {
                //var accountToken = AppSettings.AccountToken;
                var accountToken = "gel9w8pcv5iydbxya7kntosveo07g2jw";
                
                // Set headers
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                httpClient.DefaultRequestHeaders.Add("X-Account-Token", accountToken);
                
                // Make the POST request
                const string url = $"{_apiUrl}user/me";
                var response = await httpClient.GetAsync(url);

                var responseData = await response.Content.ReadAsStringAsync();
                
                dynamic? responseJson = JsonConvert.DeserializeObject(responseData);
                if (responseJson == null)
                {
                    Application.Current?.Dispatcher.Dispatch(() =>
                    {
                        callback(false, "Unknown error", null);
                    });
                    return;
                }

                var isSuccess = response.IsSuccessStatusCode;
                string message = responseJson.message ?? "Unknown error";
                
                if (isSuccess)
                {
                    dynamic? user = responseJson.user;
                    if (user == null)
                    {
                        Application.Current?.Dispatcher.Dispatch(() =>
                        {
                            callback(false, "Could not find user", null);
                        });
                        return;
                    }
                    
                    int id = user.id ?? "";
                    string firstName = user.firstname ?? "";
                    string middleName = user.middleName ?? "";
                    string surName = user.surname ?? "";
                    string email = user.email ?? "";
                    string dateOfBirth = user.date_of_birth;
                    string profilePictureUrl = user.profile_picture_url ?? "";
                    string sex = user.sex ?? "";

                    DateTime? dateOfBirthDateTime = dateOfBirth != null ? DateTime.Parse(dateOfBirth) : null;

                    AppSettings.UserId = id;
                    var userInstance = new User(id, firstName, middleName, surName, email, dateOfBirthDateTime, profilePictureUrl, sex);
                    Application.Current?.Dispatcher.Dispatch(() =>
                    {
                        callback(isSuccess, message, userInstance);
                    });
                }
            }
        });
        t.Start();
    }
    
    
    
}