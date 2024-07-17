using System.ComponentModel.DataAnnotations;

namespace DesafioIbge.Views.ViewModel
{
    public class UsuarioViewModel
    { 
            public int Id { get; set; }

            [Required(ErrorMessage = "O nome é obrigatório.")]
            [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]
            [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres.")]
            public string Name { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email é obrigatório.")]
            [MaxLength(80, ErrorMessage = "Email deve conter no máximo 80 caracteres.")]
            [EmailAddress(ErrorMessage = "Email não é válido.")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "A senha é obrigatória")]
            [MaxLength(80, ErrorMessage = "A senha deve conter no máximo 80 caracteres.")]
            //[MinLength(8, ErrorMessage = "A senha deve conter no mínimo 8 caracteres.")]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "A senha deve conter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.")]
            public string Senha { get; set; } = string.Empty;

            public string? Role { get; set; }

        }
    }
