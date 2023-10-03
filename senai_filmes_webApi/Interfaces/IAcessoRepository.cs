using senai_filmes_webApi.Domains;

namespace senai_filmes_webApi.Interfaces
{
    public interface IAcessoRepository
    {
        public AcessoDomain BuscaPorID(int ID);
    }
}
