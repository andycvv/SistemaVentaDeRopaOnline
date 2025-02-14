using System.ComponentModel.DataAnnotations;

namespace SistemaVentaDeRopaOnline.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingrese un correo válido"), EmailAddress]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
    ErrorMessage = "La contraseña debe contener al menos una minúscula, una mayúscula y un número.")]
        public string Password { get; set; }

        public bool Recordarme { get; set; }
    }
}
