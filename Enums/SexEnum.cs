namespace FocusTravelApp.Enums;

public class SexEnum
{
    public static readonly SexEnum Male = new SexEnum(0, "male");
    public static readonly SexEnum Female = new SexEnum(1, "female");
    public static readonly SexEnum Other = new SexEnum(2, "other");

    private int _value;
    private string _name;
    
    public SexEnum(int value, string name)
    {
        this._value = value;
        this._name = name;
    }
    
    public int GetValue()
    {
        return this._value;
    }
    
    public string GetName()
    {
        return this._name;
    }

    public static SexEnum FromName(string name) => name switch
    {
        "male" => SexEnum.Male,
        "female" => SexEnum.Female,
        _ => SexEnum.Other
    };
    
}