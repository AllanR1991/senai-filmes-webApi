using senai_filmes_webApi.Domains;
using senai_filmes_webApi.Interfaces;
using System.Data.SqlClient;
using System.Globalization;

namespace senai_filmes_webApi.Repositories
{
    /// <summary>
    /// Classe responsalvel pelo repositorio dos generos.
    /// Conexão com bando de dados.
    /// </summary>  
    public class GeneroRepository : IGeneroRepository
    {
        /// <summary>
        /// String de Conexao com o bando de dados que recebe os seguintess parametros
        /// Data Source: Nome do servidor
        /// Initial Catalog: Nome do banco de dados
        /// Autenticacao:
        ///     - Windows: Integrate Security = true;
        ///     - SqlServer: User Id= sa; Pwd = Senha;
        /// Integrated Security = true (para conexao integrada com windows)
        /// private string stringConexao = "DataSource = NOTE01-S14; Initial Catalog = Filmes; User Id = sa; Pwd = Senai@134";
        /// </summary>
        //private string bdConexao = "Data Source = ALLANR1991-DESK\\SQLEXPRESS; initial catalog = Filme; user Id = SA; pwd = 123456";

        /// <summary>
        /// Atualiza um genero passando o id pelo corpo
        /// </summary>
        /// <param name="genero">Objeto genero com as novas informações</param>
        public void AtualizarIdCorpo(GeneroDomain genero)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Banco.StringConexao()))
            {
                string queryUpdateIdCorpo = "UPDATE Genero SET Genero = @Nome WHERE IdGenero = @id";

                using(SqlCommand cmd = new SqlCommand(queryUpdateIdCorpo, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@id", genero.idGenero);
                    cmd.Parameters.AddWithValue("@Nome", genero.nomeGenero);

                    sqlConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Atualiza um genero passando o id pelo recurso (URL)
        /// </summary>
        /// <param name="id"> id do genero que sera atualizado</param>
        /// <param name="genero">Objeto genero que tera as novas informações</param>
        public void AtualizarIdUrl(int id, GeneroDomain genero)
        {
            //Declara a sqlConnectio passando a string de conexao
            using(SqlConnection sqlConnection = new SqlConnection(Banco.StringConexao()))
            {
                string queryUpdateIdUrl = "UPDATE Genero SET Genero = @Nome WHERE IdGenero = @id";

                using(SqlCommand cmd = new SqlCommand(queryUpdateIdUrl, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Nome", genero.nomeGenero);

                    sqlConnection.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Busca um Genero através do seu ID
        /// </summary>
        /// <param name="idGenero"> IdGenero que sera buscado.</param>
        /// <returns>Retorna Genero buscado ou se não encontrar retorna null</returns>
        public GeneroDomain BuscarPorId(int id)
        {
            //Criado uma instancia de SqlConnection chamada sqlConnection passando como parametro para o construtor bdConexao.
            using (SqlConnection sqlConnection = new SqlConnection(Banco.StringConexao()))
            {
                //Declara a query que será execultada no banco de dados.
                string querySelectById = "SELECT IdGenero, Genero FROM Genero WHERE IdGenero = @id";

                //Criado uma variavel do tipo SqlDataReader, como um objeto do SqlDataRader
                SqlDataReader rdr;

                //Criado uma instancia de SqlCommand chamada cmd e instanciando ela passando querySelectById, e sqlConnection
                using (SqlCommand cmd = new SqlCommand(querySelectById, sqlConnection))
                {
                    //este comando altera o "@id" da query acima para o id passado como parametro.
                    cmd.Parameters.AddWithValue("@ID", id);
                    
                    //Abre a conexao com o banco de dados
                    sqlConnection.Open();

                    //Execulta o comando cdm e armazena o resultado em rdr.
                    rdr = cmd.ExecuteReader();

                    //condicional rdr , Read() avança o sqlDataReader para o proximo registro.
                    if(rdr.Read())
                    {
                        //Cria um objeto do tipo GeneroDomain
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //Convert o valor contido em rdr["IdGenero"] e armazena em idGenero
                            idGenero = Convert.ToInt32(rdr["IdGenero"]),
                            //Convert o valor contido em rdr["Genero"] e armazena em nomeGenero
                            nomeGenero = rdr["Genero"].ToString()
                        };
                        //Retorna o objeto Genero pra quem chamou.
                       return genero;   
                    }
                    //Retorna null caso não tenha nenhum dado em rdr.
                    return null;
                }
            }
        }

        /// <summary>
        /// Cadastra um novo Genero
        /// </summary>
        /// <param name="novoGenero">Pbjeto novoGenero com as informações que serão cadastradas</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Cadastrar(GeneroDomain novoGenero)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Banco.StringConexao()))
            {
                //Declara a query que será execultada.
                //Insert into Generos(Genero) Values('Joana D'Arc')
                //Insert into Generos(Genero) Values('')DROP TABLE Filmes--'); -> sql injection, deve-se evitar permitir issso.

                //string queryInsert = $"INSERT INTO Genero(Genero) VALUES('{novoGenero.nomeGenero}')"; Forma errada.

                string queryInsert = "INSERT INTO Genero(Genero) VALUES(@Nome)";

                //Declara o SqlComand cmd passando a query que será execultada e a conexao como parâmetros
                using (SqlCommand cmd = new SqlCommand(queryInsert, sqlConnection))
                {
                    //Passa o valor como parametro para @Nome
                    cmd.Parameters.AddWithValue("@Nome", novoGenero.nomeGenero);
                    
                    //Abre a conexâo com o banco de dados
                    sqlConnection.Open();

                    //Executa a query.
                    cmd.ExecuteNonQuery();

                }
            }
        }

        /// <summary>
        /// Deleta um genero através do seu id
        /// </summary>
        /// <param name="idGenero">id do genero que sera deletado</param>
        public void Deletar(int idGenero)
        {
            using(SqlConnection sqlConnection = new SqlConnection(Banco.StringConexao()))
            {
                string queryDelet = "DELETE FROM Genero WHERE Genero.idGenero = @IdGenero ";

                using(SqlCommand cmd = new SqlCommand(queryDelet, sqlConnection))
                {
                    cmd.Parameters.AddWithValue("@IdGenero", idGenero);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Lista todos os gêneros
        /// </summary>
        /// <returns>Uma lista de gêneros</returns>
        public List<GeneroDomain> ListarTodos()
        {
            //Cria uma lista listaGenero onde serão armazenados os dados
            List<GeneroDomain> listaGeneros = new List<GeneroDomain>();

            //Cria um nova instancia do SqlConnection chamada conexao, usando uma sobrecarga de SqlConnection onde passamos como parametro a string de conexão, dentro do using para que apos a execução ele desconect,  
            using (SqlConnection conexaoSql = new SqlConnection(Banco.StringConexao()))
            {
                //Declara a instrusão a ser execultada.
                string querySelectAll = "SELECT * FROM Genero";

                //Abre aconexão com o banco de dados;
                conexaoSql.Open();

                //Declara o SqlDataReader rdr para percorrer a tablea do banco de dados
                SqlDataReader rdr;

                //Declara o SqlCommand cmd passando a query que sera execultada e a conexão 
                using (SqlCommand cmd = new SqlCommand(querySelectAll, conexaoSql))
                {
                    //Execulta a query e armazena os dados no rdr
                    rdr = cmd.ExecuteReader();

                    //Enquanto houverregistors para serem lidos no rdr, o laço se repete.
                    while (rdr.Read())
                    {
                        //Instancia um objeto genero do tipo GeneroDomain
                        GeneroDomain genero = new GeneroDomain()
                        {
                            //Atribui a propriedade IdGenero o dalor da primeira coluna da tabela do banco de dados
                            idGenero = Convert.ToInt32(rdr[0]),
                            //Atribui a propriedade nome o valor da segunda coluna da tabela do banco de dados
                            nomeGenero = rdr[1].ToString()
                        };
                        //Adiciona o objeto genero à listaGenero
                        listaGeneros.Add(genero);
                    }
                }
            }
            //retorna a lista de generos
            return listaGeneros;
        }
    }
}
