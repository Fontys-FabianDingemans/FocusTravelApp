using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;

namespace FocusTravelApp.Views;

public partial class PauzeReminderPopUp : Popup
{
    public PauzeReminderPopUp()
    {
        InitializeComponent();
    }

    private void CloseButtonPauzeReminderClicked(object? sender, EventArgs e)
    {
        Close();
    }
}