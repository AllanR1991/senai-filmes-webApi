using senai_filmes_webApi.Domains;

namespace senai_filmes_webApi.Interfaces
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Valida o usuário
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <param name="senha">Senha do Usuario</param>
        /// <returns>Um objeto do tipo UsarioDomain que foi buscado</returns>
        UsuarioDomain BuscarPorEmailSenha(string email, string senha);
    }
}
