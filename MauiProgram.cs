using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui;

namespace FocusTravelApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>().UseMauiCommunityToolkit().ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("Inter-Regular.ttf", "InterRegular");
            fonts.AddFont("Inter-SemiBold.ttf", "InterSemiBold");
        }).UseMauiCommunityToolkitMediaElement();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}