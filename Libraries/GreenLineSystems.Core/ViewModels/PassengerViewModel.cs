using Microsoft.AspNetCore.Http;

namespace GreenLineSystems.Core.ViewModels;

public class PassengerViewModel
{
    public IFormFile passengerFile { get; set; }
}

public class NewPassengerDetails
{
    public string firstName { get; set; }
    public string surname { get; set; }
    public DateTime dob { get; set; }
    public string address { get; set; }
    
    public string Gender { get; set; }

    public string Nationality { get; set; }

    public string PassportNumber { get; set; }
    
    
    public double Terrorism { get; set; }

    public double Narcotics { get; set; }

    public double Smuggling { get; set; }

    public double IllegalImmigration { get; set; }

    public double Revenue { get; set; }
}


public class PassengerDetailsModel
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public DateTime passengerDob { get; set; }
    public string passengerAddress { get; set; }
    
    public string Gender { get; set; }

    public string Nationality { get; set; }

    public string Passport{ get; set; }
    
    
    public double Terrorism { get; set; }

    public double Narcotics { get; set; }

    public double Smuggling { get; set; }

    public double IllegalImmigration { get; set; }

    public double Revenue { get; set; }
}

