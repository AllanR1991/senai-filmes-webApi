using senai_filmes_webApi.Domains;
using senai_filmes_webApi.Interfaces;
using System.Data.SqlClient;

namespace senai_filmes_webApi.Repositories
{
    /// <summary>
    /// Classe responsável pelo repositório dos usuários
    /// </summary>
    public class UsuarioRepository : IUsuarioRepository
    {
        /// <summary>
        /// Valida usuario
        /// </summary>
        /// <param name="email">Email do usuário</param>
        /// <param name="senha">Senha do usuário</param>
        /// <returns>retorna um objeto do tipo UsuarioDomain</returns>
        public UsuarioDomain BuscarPorEmailSenha(string email, string senha)
        {
            //Define a conexao com banco de dados.
            using(SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelect = @"
                    SELECT *  FROM Usuario
                        WHERE Email = @email AND Senha = @senha;
";
                sqlConexao.Open();

                using(SqlCommand cmd = new SqlCommand(querySelect, sqlConexao)) 
                {
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("senha", senha);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if(rdr.Read())
                    {
                        UsuarioDomain usuario = new UsuarioDomain()
                        {
                            idUsuario = Convert.ToInt32(rdr["IdUsuario"]),
                            idAcesso = Convert.ToInt32(rdr["IdAcesso"]),
                            email = rdr["Email"].ToString(),
                            senha = rdr["Senha"].ToString()
                        };
                        return usuario;
                    }
                    return null;
                }
            }
        }
    }
}
