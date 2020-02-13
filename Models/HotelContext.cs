using Microsoft.EntityFrameworkCore;

namespace Hotel.Models
{
    public class HotelContext : DbContext
    {
        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public DbSet<Quarto> Quartos {get; set;}
        public DbSet<Reserva> Reservas {get; set;}
    }
}