namespace senai_filmes_webApi
{
    public static class Banco
    {
        /// <summary>
        /// Metodo criado para acessar o banco de dados.
        /// </summary>
        /// <returns>Retorna a string com os dados para acessar o banco de dados.</returns>
        public static string StringConexao()
        {
            string bdConexao = " Data Source = ALLANR1991-DESK\\SQLEXPRESS;" +  //Servidor SQL
                               " initial catalog = Filme;" +    //Banco de dados a acessar
                               " user Id = SA;" +   //Usuario
                               " pwd = 123456";   //Password

            return bdConexao;
        }
    }
}
