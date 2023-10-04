using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_filmes_webApi.Domains;
using senai_filmes_webApi.Interfaces;
using senai_filmes_webApi.Repositories;

namespace senai_filmes_webApi.Controllers
{
    //Define que o tipo de resposta da api seja no formato Json
    [Produces("application/json")]

    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        /// <summary>
        /// Objeto _generoRepository que irá receber todos os metodos definidos na interface IGeneroRepository
        /// </summary>
        private IGeneroRepository _generoRepository { get; set; }

        /// <summary>
        /// Instancia o objeto _generoRepository para que haja a referencia aos métodos no repositorio
        /// </summary>
        public GeneroController()
        {
            _generoRepository = new GeneroRepository();
        }

        /// <summary>
        /// Lista todos os generos
        /// </summary>
        /// <returns>Retorna uma lista de genero e um status code</returns>
        /// dominio/api/generos
        /// o usuario precisa estar logado para listar todos os generos
        /// <response code="200">Retorna lista de Generos</response>
        [Authorize] //Verifica se o usuario esta logado.
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            //Cria um lista nomeada listaGeneros para receber os dados
            List<GeneroDomain> listaGeneros = _generoRepository.ListarTodos();

            //Retorna o status code 200(ok) com a lista de generos no formato JSON
            return Ok(listaGeneros);
        }

        /// <summary>
        /// Busca um genero atraves do seu id
        /// </summary>
        /// <param name="id">id do genero que sera buscado</param>
        /// <returns>Um genero buscado ou notfound caso nenhum genero seja encontrado</returns>
        /// Somente usuario administrador pode buscar um genero pelo id.
        /// ex de mais acessos [Authorize(Roles = "Administrador, Comum")]
        /// <response code="200">Genero encontrado, retornando objeto Genero</response>
        /// <response code="204">Genero não encontrado.</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetById(int id)
        {
            GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);
            if(generoBuscado == null)
            {
                return NotFound("Nenhum genero foi encontrado!");
            }
            return Ok(generoBuscado);
        }


        /// <summary>
        /// Cadastra um novo Genero
        /// </summary>
        /// <param name="novoGenero">Objeto novoGenero Recebido na requisição.</param>
        /// <returns>Um status code 201 - Created</returns>
        /// <response code="201">Genero cadastrado com sucesso.</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]

        public IActionResult Post(GeneroDomain novoGenero)
        {
            //Faz a chamada para o metodo .Cadastrar()
            _generoRepository.Cadastrar(novoGenero);

            //Retorna um status Code 201 - Created
            return StatusCode(201);
        }

        /// <summary>
        /// Atualiza um genero existente passando o id pela url
        /// </summary>
        /// <param name="id">Id do genero que sera atualizado</param>
        /// <param name="generoAtualizado">Objeto genero atualizado com as novas informações</param>
        /// <returns>Um srarus code</returns>
        /// <response code="204">Genero atualizado com sucesso.</response>
        /// <response code="404">Genero não encontrado.</response>
        /// <response code="400">Se Erro.</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult PutUrl(int id, GeneroDomain generoAtualizado)
        {
            GeneroDomain generoBuscado = _generoRepository.BuscarPorId(id);

            //Verificar se Genero Buscado é nulo
            if(generoBuscado == null)
            {
                //Caso seja nulo ele retorna um notfound onde retorna um json.
                return NotFound
                    //passando um json para statuscode NotFound
                    (new
                        {
                            mensagem = "Gênero não encontrado!",
                            erro = true
                        }
                    );
            }

            try
            {
                //Faz a chamada para o metodo .AtualizarIdUrl()
                _generoRepository.AtualizarIdUrl(id, generoAtualizado);
                //Retorna um status code 204 - no content
                return NoContent();
            }
            //Caso ocorra algumnum erro
            catch (Exception erro)
            {
                return BadRequest(erro);
            }
            

            
        }

        /// <summary>
        /// Atualiza gereno atraves de um objeto tipo GeneroDomain
        /// </summary>
        /// <param name="generoAtualizado">objeto tipo GeneroDomain</param>
        /// <returns>Retorna NoContent, caso contrario BadRequest. Caso contrario NotFound</returns>
        /// <response code="204">Genero atualizado com sucesso.</response>
        /// <response code="404">Genero não encontrado.</response>
        /// <response code="400">Se Erro.</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult PutCorpo(GeneroDomain generoAtualizado)
        {
            GeneroDomain generoBuscado =  _generoRepository.BuscarPorId(generoAtualizado.idGenero);

            if(generoBuscado != null)
            {
                try
                {
                    _generoRepository.AtualizarIdCorpo(generoAtualizado);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }

             return NotFound(
                 new
                 {
                     mensagem = "Gênero não encontrado!",
                     erro = true
                 });
        }


        /// <summary>
        /// Deleta um Genero
        /// </summary>
        /// <param name="id">Id utilizada para deletar um genero.</param>
        /// <returns>return status code 204.</returns>
        /// <response code="204">Filme deletado com sucesso</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _generoRepository.Deletar(id);
            return StatusCode(204);
        }
    }
}
