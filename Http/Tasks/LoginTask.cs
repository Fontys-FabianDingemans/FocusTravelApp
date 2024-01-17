using System.Diagnostics;
using System.Net.Http.Headers;
using FocusTravelApp.Http.Responses;
using Newtonsoft.Json;

namespace FocusTravelApp.Http.Tasks;

using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class LoginTask
{
    private const string _apiUrl = "https://testprzemek.nl/api/user/login";

    public static void Run(string email, string password, Action<LoginResponse> callback)
    {
        Login(email, password, callback).Wait();
    }

    private static async Task Login(string email, string password, Action<LoginResponse> callback)
    {
        
    }
    
}