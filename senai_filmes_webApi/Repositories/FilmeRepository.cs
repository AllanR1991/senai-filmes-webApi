using senai_filmes_webApi.Domains;
using senai_filmes_webApi.Interfaces;
using System.Collections.Specialized;
using System.Data.SqlClient;

namespace senai_filmes_webApi.Repositories
{
    public class FilmeRepository : IFilmeRepository
    {
        /// <summary>
        /// String que contem o servidor nome do banco de dados, usuario e 
        /// </summary>
        //private string bdConexao = "Data Source = ALLANR1991-DESK\\SQLEXPRESS; initial catalog = Filme; user Id = SA; pwd = 123456";
        public void AtualizarIdCorpo(FilmeDomain filme)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao())) 
            {
                string queryUpdateCorpo = "UPDATE Filme SET IdGenero = @idGenero, Filme = @nomeFilme Where IdFilme = @idFilme";
                
                sqlConexao.Open();
                
                using(SqlCommand cmd = new SqlCommand(queryUpdateCorpo,sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idFilme", filme.idFilme);
                    cmd.Parameters.AddWithValue("idGenero", filme.idGenero);
                    cmd.Parameters.AddWithValue("nomeFilme", filme.nomeFilme);

                    cmd.ExecuteNonQuery();

                    /*
                     Uma instrução Transact-SQL (T-SQL) é uma linguagem de programação utilizada para gerenciar e manipular dados em um banco de dados relacional que suporta o Microsoft SQL Server, como SELECT, INSERT, UPDATE, DELETE, CREATE, ALTER e DROP. A T-SQL é uma extensão do SQL (Structured Query Language) padrão e inclui recursos específicos da Microsoft para interagir com o SQL Server.
                     */
                }
            }
        }

        public void AtualizarIdUrl(int idFilme, FilmeDomain filme)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryUpdate = "UPDATE Filme SET IdGenero = @idGenero, Filme = @nomeFilme WHERE IdFilme = @idFilme";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryUpdate, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idGenero", filme.idGenero);
                    cmd.Parameters.AddWithValue("nomeFilme", filme.nomeFilme);
                    cmd.Parameters.AddWithValue("idFilme", idFilme);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Método para buscar um filme cadastrado no Banco de dados.
        /// </summary>
        /// <param name="idFilme">Parametro idFilme que sera passado para buscarmos no sistema.</param>
        /// <returns>Retorna um Objeto FilmeDomain</returns>
        public FilmeDomain BuscarPorId(int idFilme)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {

                string querySelect = "SELECT * FROM Filme WHERE IdFilme = @idFilme";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelect, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("@idFilme", idFilme);

                    cmd.ExecuteNonQuery();

                    SqlDataReader reader;

                    reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            idFilme = Convert.ToInt32(reader["IdFilme"]),
                            idGenero = Convert.ToInt32(reader["IdGenero"]),
                            nomeFilme = reader["Filme"].ToString()
                        };
                        return filme;
                    }
                    return null;
                };
            };
        }

        /// <summary>
        /// Metodo para cadastrar um novoFilme.
        /// </summary>
        /// <param name="novoFilme">Objeto recebido como parametro contendo os dados do novo filme a ser inserido no sistema.</param>
        public void Cadastrar(FilmeDomain novoFilme)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryInsert = "INSERT INTO Filme (IdGenero,Filme) VALUES (@IdGenero,@NomeFilme)";

                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert,sqlConexao))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", novoFilme.idGenero);
                    cmd.Parameters.AddWithValue("@NomeFilme", novoFilme.nomeFilme);

                    cmd.ExecuteNonQuery();
                };
            };
        }

        public void Deletar(int idFilme)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string queryDelete = "DELETE FROM Filme WHERE IdFilme = @idFilme";

                sqlConexao.Open() ;
                
                using(SqlCommand cmd = new SqlCommand(queryDelete,sqlConexao))
                {
                    cmd.Parameters.AddWithValue("@idFilme", idFilme);

                    cmd.ExecuteNonQuery();
                }
            };
        }

        /// <summary>
        /// Metodo que lista todos os Filmes do banco de dados
        /// </summary>
        /// <returns>Retorna uma lista de Filmes para quem chamou</returns>
        public List<FilmeDomain> ListarTodos()
        {
            List<FilmeDomain> listaFilme = new List<FilmeDomain>(); 


            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectAll = "SELECT * FROM Filme";

                sqlConexao.Open();

                SqlDataReader reader;

                using (SqlCommand cmd = new SqlCommand(querySelectAll,sqlConexao)) 
                {
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            idFilme = Convert.ToInt32(reader["IdFilme"]),
                            idGenero = Convert.ToInt32(reader["IdGenero"]),
                            nomeFilme = reader["Filme"].ToString()
                        };
                        listaFilme.Add(filme);
                    }
                    
                }
            }
            return listaFilme;
        }
    }
}
