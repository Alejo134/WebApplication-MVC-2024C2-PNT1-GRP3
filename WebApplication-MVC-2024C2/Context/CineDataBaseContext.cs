using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_MVC_2024C2.Models;
using System.Collections.Generic;


namespace WebApplication_MVC_2024C2.Context
{


    public class CineDataBaseContext : DbContext
    {

        public CineDataBaseContext(DbContextOptions<CineDataBaseContext> options) : base(options)
        {
            

        }

        public DbSet<PeliculaButaca> Butacas { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }






    }
}
