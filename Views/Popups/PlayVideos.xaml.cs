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
        
        if (Videonummer == 1)
        {
            OefeningTekst.Text = "Wide grip Pull ups";
            Video1.Source = "http://videos.testprzemek.nl/video1.mp4";
        }
        
        else if (Videonummer == 2)
        {
            OefeningTekst.Text = "Underhand grip Pull ups";
            Video1.Source = "http://videos.testprzemek.nl/video2.mp4";
        }
        
        else if (Videonummer == 3)
        {
            OefeningTekst.Text = "..";
        }
        
        else if (Videonummer == 4)
        {
            OefeningTekst.Text = "..";
        }
        
        else if (Videonummer == 5)
        {
            OefeningTekst.Text = "..";
        }
        
        
        
       
        
       
    }
}