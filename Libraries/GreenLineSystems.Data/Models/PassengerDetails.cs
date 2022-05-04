using System.ComponentModel.DataAnnotations.Schema;

namespace GreenLineSystems.Data.Models;

public class PassengerDetails
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }

    public string Nationality { get; set; }

    public string PassportNumber { get; set; }
    
    
    public double Terrorism { get; set; }

    public double Narcotics { get; set; }

    public double Smuggling { get; set; }

    public double IllegalImmigration { get; set; }

    public double Revenue { get; set; }
    public string Address { get; set; }
}