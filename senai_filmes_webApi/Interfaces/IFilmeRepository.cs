using senai_filmes_webApi.Domains;

namespace senai_filmes_webApi.Interfaces
{
    /// <summary>
    /// Interface diz o que vai ser feito contendo todos os metodos, mas não como.
    /// </summary>
    public interface IFilmeRepository
    {
        /// <summary>
        /// Retorna uma lista de Filmes.
        /// </summary>
        /// <returns>Uma lista de Filmes</returns>
        List<FilmeDomain> ListarTodos();

        /// <summary>
        /// Busca filme atraves do idFilme
        /// </summary>
        /// <param name="idFilme">Id do Filme que sera buscado</param>
        /// <returns>Retorna um Objeto de FilmeDomain</returns>
        FilmeDomain BuscarPorId(int idFilme);

        /// <summary>
        /// Cadastra um novo filme 
        /// </summary>
        /// <param name="novoFilme">Objeto Filme que sera cadastrado</param>
        void Cadastrar(FilmeDomain novoFilme);

        /// <summary>
        /// Atualiza um Filme passando o id pelo corpo da requisicao
        /// </summary>
        /// <param name="filme">Ojeto que sera atualizado</param>
        void AtualizarIdCorpo(FilmeDomain filme);

        /// <summary>
        /// Atualiza um Filme existente passando o id pela URL da requisição
        /// </summary>
        /// <param name="idFilme">Id do filme que será atualizado.</param>
        /// <param name="filme">Objeto Filme com as novas informaçoes</param>
        void AtualizarIdUrl(int idFilme, FilmeDomain filme);

        /// <summary>
        /// Deletar um Filme
        /// </summary>
        /// <param name="idFilme">id do genero que sera deletado</param>
        void Deletar(int idFilme);
    }
}
