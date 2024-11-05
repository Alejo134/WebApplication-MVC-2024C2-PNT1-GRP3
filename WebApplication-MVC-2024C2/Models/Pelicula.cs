using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Metadata;
namespace WebApplication_MVC_2024C2.Models
{
    public class Pelicula
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }       
        public string Clasificacion { get; set; }
        public int NroDeSala { get; set; }
        public DateTime Fecha { get; set; }
        public double Precio { get; set; }
        public int CantButacas { get; set; }

    }


  



}
