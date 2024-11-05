using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
namespace WebApplication_MVC_2024C2.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Apellido { get; set; }
        public string DNI {  get; set; }
        public int Puntos { get; set; }

    }
}
