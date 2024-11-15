using Microsoft.EntityFrameworkCore;

namespace WebApplication_MVC_2024C2.Models
{
    public class Venta
    {
        public int Id { get; set; }
        public int IdPelicula { get; set; }
        public DateTime Fecha { get; set; }
        public int CantButacas { get; set; }
        public double Total { get; set; }

        //public Pelicula Pelicula { get; set; }


    }
}
