using System.ComponentModel.DataAnnotations;

namespace SistemaVentaDeRopaOnline.Models.ViewModels
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Ingrese sus nombres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un correo válido"), EmailAddress]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).+$",
            ErrorMessage = "La contraseña debe contener al menos una minúscula, una mayúscula y un número.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debe confirmar la contraseña.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmarPassword { get; set; }
    }
}
