using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace WebApplication_MVC_2024C2.Models
{
    public class PeliculaButaca
    {
        public int Id { get; set; }
        public int IdPelicula { get; set; }
        public bool Disponible { get; set; }

    }

}