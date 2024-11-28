using System.ComponentModel.DataAnnotations;

namespace WebApplication_MVC_2024C2.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "El usuario es obligatorio")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [DataType(DataType.Password)]

        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }


        public int IDUsuario {  get; set; }
    }
}
