using System.ComponentModel.DataAnnotations;

namespace senai_filmes_webApi.Domains
{
    public class AcessoDomain
    {
        public int idAcesso {  get; set; }

        [Required(ErrorMessage = "Necessario preencher o campo de acesso.")]
        public string acesso {  get; set; } 
    }
}
