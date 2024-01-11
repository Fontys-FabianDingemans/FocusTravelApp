namespace FocusTravelApp.Classes;
using System;

public class User
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public void SetFullName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

}


    


