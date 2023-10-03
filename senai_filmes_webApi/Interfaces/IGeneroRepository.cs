using senai_filmes_webApi.Domains;

namespace senai_filmes_webApi.Interfaces
{
    /// <summary>
    /// Interface responsavel pelo repositório GeneroRepository
    /// Interface é como se fosse um contrato informando o que deve ser feito
    /// Contem os metodos de genero
    /// </summary>
    public interface IGeneroRepository
    {
        // TipoRetorno NomeMetodo(TipoParametro parametro);

        /// <summary>
        /// Retorna todos os Generos
        /// </summary>
        /// <returns>Uma lista de Gêneros</returns>
        List<GeneroDomain> ListarTodos();


        /// <summary>
        /// Busca um Genero através do seu ID
        /// </summary>
        /// <param name="idGenero"> Id do genero que sera buscado</param>
        /// <returns>Retorna um objeto de GeneroDomain</returns>
        GeneroDomain BuscarPorId(int idGenero);

        /// <summary>
        /// Cadastra um novo Gênero
        /// </summary>
        /// <param name="novoGenero">Objeto novoGenero que sera cadastrado.</param>
        void Cadastrar(GeneroDomain novoGenero);

        /// <summary>
        /// Atualiza um genero existente passando o id pelo corpo da requisição
        /// </summary>
        /// <param name="genero">Objeto genero que será atualizado</param>
        void AtualizarIdCorpo(GeneroDomain genero);

        /// <summary>
        /// Atualiza um genero existente passando o id pela URL da requisição
        /// </summary>
        /// <param name="idGenero">id do genero que será atualizado.</param>
        /// <param name="genero">Objeto genero com as novas informações</param>
        void AtualizarIdUrl(int idGenero, GeneroDomain genero);


        /// <summary>
        /// Geketa un genero
        /// </summary>
        /// <param name="idGenero">id do genero que sera deletado</param>
        void Deletar(int idGenero);
    }
}
