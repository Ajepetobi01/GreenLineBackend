namespace GreenLineSystems.Core.ViewModels;

public class FlightsViewModel
{
    
}

public class FlightVM
{

    public int Id { get; set; }
    public string Flight { get; set; }

    public string Departure { get; set; }

    public string Arrival { get; set; }

    public string Terminal { get; set; }

    public string Aircraft { get; set; }

    public int FlightCapacity { get; set; }

    public int FlightCrew { get; set; }
}

public class FlightPassengerDetails
{

    public int Id { get; set; }
    
    public string FlightName { get; set; }

    public string PassengerSeat  { get; set; }

    public string PassengerForeName { get; set; }

    public string PassengerSurname { get; set; }

    public string PassportNumber { get; set; }

    public string CountryOfIssue { get; set; }
}