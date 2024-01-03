using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class EditorCategoryViewModel
{
    [Required(ErrorMessage = "O campo 'nome' e obrigatorio")] // O CAMPO ABAIXO E OBRIGATORIO
    [StringLength(40, MinimumLength = 2, ErrorMessage = "Este campo deve conter no minimo 2 letras")]
    public string Name { get; set; }
    public string Slug { get; set; }
    
}