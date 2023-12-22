using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;

namespace FocusTravelApp.Views;

public partial class DrinkReminderPopUp : Popup
{
    public DrinkReminderPopUp()
    {
        InitializeComponent();
    }

    private void CloseButtonDrinkReminderClicked(object? sender, EventArgs e)
    {
        Close();
    }
}