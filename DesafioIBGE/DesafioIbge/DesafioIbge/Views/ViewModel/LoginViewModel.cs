using System.ComponentModel.DataAnnotations;

namespace DesafioIbge.Views.ViewModel
{
    public class LoginViewModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "O email é obrigatório.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Senha { get; set; } = string.Empty;
    }
}
