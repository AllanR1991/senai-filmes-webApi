using System.ComponentModel.DataAnnotations;

namespace senai_filmes_webApi.Domains
{
    /// <summary>
    /// Classe que representa a entidade Generos
    /// </summary>
    public class GeneroDomain
    {
        public int idGenero { get; set; }

        [Required(ErrorMessage = "Necessario preencher o nome do Genero, campo é obrigatório")]//Este Data Notation informa que o atributo é obrigatorio ser preenchido, caso não seja preenchido exibe a mensagem dentro do parenteses. 
        public string nomeGenero { get; set; }
    }
}
