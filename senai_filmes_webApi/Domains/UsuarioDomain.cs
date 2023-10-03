using System.ComponentModel.DataAnnotations;

namespace senai_filmes_webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade Usuario
    /// </summary>
    public class UsuarioDomain
    {
        public int idUsuario {  get; set; }

        //Define que o campo é obrigatório.
        [Required(ErrorMessage = "Informe o Id da permisão")]
        public int idAcesso { get; set; }
        
        //Define que o campo é obrigatório.
        [Required(ErrorMessage = "Informe o e-mail")]
        public string email { get; set; }
        
        //Difine que o campo é obrigatorio.
        [Required(ErrorMessage = "Informe a senha")]
        //Define que o campo deve ter no minimo 3 caracteres e no máximo 50.
        [StringLength(50,MinimumLength = 3, ErrorMessage = "O campo senha precisa ter no minimo 3 e no maximo 50 caracteres")]
        public string senha { get; set; }
    }
}
