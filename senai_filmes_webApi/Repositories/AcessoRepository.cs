using senai_filmes_webApi.Domains;
using senai_filmes_webApi.Interfaces;
using System.Data.SqlClient;

namespace senai_filmes_webApi.Repositories
{
    public class AcessoRepository : IAcessoRepository
    {
        public AcessoDomain BuscaPorID(int ID)
        {
            using (SqlConnection sqlConexao = new SqlConnection(Banco.StringConexao()))
            {
                string querySelectID = @"
                    SELECT * FROM Acesso
                        WHERE IdAcesso = @idBuscado;
";
                sqlConexao.Open();

                using (SqlCommand cmd = new SqlCommand(querySelectID, sqlConexao))
                {
                    cmd.Parameters.AddWithValue("idBuscado", ID);

                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        AcessoDomain acessoEncontrado = new AcessoDomain()
                        {
                            acesso = rdr["Acesso"].ToString(),
                            idAcesso = Convert.ToInt32(rdr["IdAcesso"])
                        };
                        return acessoEncontrado;
                    }
                    return null;
                }
            }
        }
    }
}
