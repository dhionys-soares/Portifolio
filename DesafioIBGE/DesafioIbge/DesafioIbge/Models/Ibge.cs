using System.ComponentModel.DataAnnotations;

namespace DesafioIbge.Models
{
    public class Ibge
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O estado é obrigatório.")]
        [MinLength(2, ErrorMessage = "O estado deve conter no mínimo 2 caracteres.")]
        [MaxLength(2,ErrorMessage = "O estado deve conter no máximo 2 caracteres.")]
        public string State { get; set; } = string.Empty;

        [Required(ErrorMessage = "A cidade é obrigatória.")]
        [MinLength(3, ErrorMessage = "A cidade deve conter no mínimo 3 caracteres.")]
        [MaxLength(80, ErrorMessage = "A cidade deve conter no máximo 80 caracteres.")]
        public string City { get; set; } = string.Empty;
    }
}
