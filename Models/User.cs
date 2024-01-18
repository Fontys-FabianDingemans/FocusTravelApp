using System.Globalization;
using FocusTravelApp.Enums;

namespace FocusTravelApp.Models;

public class User
{
    
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public SexEnum? Sex { get; set; }
    private bool _AccountVerified;

    /** Find the user by id */
    public User(int id)
    {
        this.Id = id;
        
        // TODO: Get the user from the database
        this.FirstName = "John";
        this.MiddleName = "Doe";
        this.LastName = "Smith";
        this.Email = "john.d.smith@gmail.com";
        this.DateOfBirth = new DateTime(1990, 1, 1);
        this.ProfilePictureUrl = "https://placehold.co/64x64";
        this.Sex = SexEnum.Male;
        this._AccountVerified = false;
    }
    
    public User(
        string? firstName,
        string? middleName,
        string? lastName,
        string email,
        DateTime? dateOfBirth,
        string? profilePictureUrl,
        SexEnum? sex
    ) {
        this.Id = 0;
        this.FirstName = firstName;
        this.MiddleName = middleName;
        this.LastName = lastName;
        this.Email = email;
        this.DateOfBirth = dateOfBirth;
        this.ProfilePictureUrl = profilePictureUrl;
        this.Sex = sex;
        this._AccountVerified = false;
    }
    
    public User(
        string? firstName,
        string? middleName,
        string? lastName,
        string email,
        DateTime? dateOfBirth,
        string? profilePictureUrl,
        string? sex
    ) {
        this.Id = 0;
        this.FirstName = firstName;
        this.MiddleName = middleName;
        this.LastName = lastName;
        this.Email = email;
        this.DateOfBirth = dateOfBirth;
        this.ProfilePictureUrl = profilePictureUrl;
        this.Sex = SexEnum.FromName(sex);
        this._AccountVerified = false;
    }
    
    public User(
        int id,
        string? firstName,
        string? middleName,
        string? lastName,
        string email,
        DateTime? dateOfBirth,
        string? profilePictureUrl,
        string? sex
    )
    {
        this.Id = id;
        this.FirstName = firstName;
        this.MiddleName = middleName;
        this.LastName = lastName;
        this.Email = email;
        this.DateOfBirth = dateOfBirth;
        this.ProfilePictureUrl = profilePictureUrl;
        this.Sex = SexEnum.FromName(sex ?? "");
        this._AccountVerified = false;
    }
    
    public string GetFullName()
    {
        return string.IsNullOrEmpty(MiddleName) ? $"{FirstName} {LastName}" : $"{FirstName} {MiddleName} {LastName}";
    }
    
    public string GetInitials()
    {
        var firstName = FirstName ?? "";
        var middleName = MiddleName ?? "";
        var lastName = LastName ?? "";
        
        return string.IsNullOrEmpty(MiddleName) ? $"{firstName[0]}{lastName[0]}" : $"{firstName[0]}{middleName[0]}{lastName[0]}";
    }
    
    public int GetAge()
    {
        return DateTime.Now.Year - this.DateOfBirth?.Year ?? 0;
    }

    public string GetFormattedSex()
    {
        if(this.Sex == null) return SexEnum.Other.GetName();
        return new CultureInfo("en-US").TextInfo.ToTitleCase(this.Sex.GetName());
    }

    public bool IsOlderThen(int age)
    {
        return this.GetAge() >= age;
    }
    
    public bool IsVerified()
    {
        return this._AccountVerified;
    }
    
    public void SetAccountVerified()
    {
        this._AccountVerified = true;
    }

    public int GetTotalPoints()
    {
        // TODO: Get total points from database
        return 0;
    }

    public override string ToString()
    {
        return "User: " + this.GetFullName() + " (" + this.Email + ")";
    }
    
}