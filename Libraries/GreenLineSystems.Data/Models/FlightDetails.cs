using System.ComponentModel.DataAnnotations.Schema;

namespace GreenLineSystems.Data.Models;

public class FlightDetails
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Flight { get; set; }

    public string Departure { get; set; }

    public string Arrival { get; set; }

    public string Terminal { get; set; }

    public string Aircraft { get; set; }

    public int Capacity { get; set; }

    public int Crew { get; set; }
}