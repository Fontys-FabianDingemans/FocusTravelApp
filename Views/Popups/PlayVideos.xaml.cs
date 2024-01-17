using CommunityToolkit.Maui.Views;

namespace FocusTravelApp.Views;

public partial class PlayVideos : Popup
{
    private int Videonummer;
    public PlayVideos(int nummer)
    {
        InitializeComponent();
        Videonummer = nummer;
    }

    private void CloseVideo(object? sender, EventArgs e)
    {
        Video1.Stop();
        Close();
    }

    public void UpdateVideoPopUp()
    {
        Video1.Source = $"https://testprzemek.nl/videos/Video{Videonummer}.mp4";
        
        if (Videonummer == 1)
        {
            OefeningTekst.Text = "Wide grip Pull ups";
        }
        
        else if (Videonummer == 2)
        {
            OefeningTekst.Text = "Underhand grip Pull ups";
        }
        
        else if (Videonummer == 3)
        {
            OefeningTekst.Text = "Side Plank";
        }
        
        else if (Videonummer == 4)
        {
            OefeningTekst.Text = "Decline Sit ups";
        }
        
        else if (Videonummer == 5)
        {
            OefeningTekst.Text = "Incline Push ups";
        }
        
        
        
       
        
       
    }
}