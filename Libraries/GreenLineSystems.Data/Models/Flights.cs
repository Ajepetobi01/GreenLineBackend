using System.ComponentModel.DataAnnotations.Schema;

namespace GreenLineSystems.Data.Models;

public class Flights
{

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FlightName { get; set; }

    public string Seat  { get; set; }

    public string ForeName { get; set; }

    public string Surname { get; set; }

    public string PassportNumber { get; set; }

    public string CountryOfIssue { get; set; }
}