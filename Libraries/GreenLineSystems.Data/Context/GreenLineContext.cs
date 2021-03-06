using GreenLineSystems.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GreenLineSystems.Data.Context;

public class GreenLineContext:IdentityDbContext<ApplicationUser>
{
    public GreenLineContext(DbContextOptions options) : base(options)
    {
        
    }
    
    
    public DbSet<PassengerDetails> PassengerDetails { get; set; }
    public DbSet<Airports> Airports { get; set; }
    public DbSet<Airline> Airlines { get; set; }

    public DbSet<Flights> Flights { get; set; }
    public DbSet<FlightDetails> FlightDetails { get; set; }
    
    




    protected override void OnModelCreating(ModelBuilder builder)
    {



        builder.Entity<PassengerDetails>().HasIndex(p => new {p.Id, p.LastName});

        //builder.Entity<PassengerDetails>().Property(u => u.Id).HasDatabaseGeneratedOption();

        base.OnModelCreating(builder);



    }
    
}