﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using senai_filmes_webApi.Domains;
using senai_filmes_webApi.Interfaces;
using senai_filmes_webApi.Repositories;
namespace senai_filmes_webApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class FilmeController : ControllerBase
    {
        //Objeto que ira receber todos os metodos definidos na interface IFilmeRepository porem é somente isso, ele não sabe como funciona o metodo
        private IFilmeRepository _filmeRepository { get; set; }

        /// <summary>
        /// Criado um construtor onde sempre que houver uma requisição ou chamada para FilmeController ele automaticamente ja Instancia o objeto _filmeRepository para que haja a referencia aos métodos no repositorio
        /// </summary>
        public FilmeController()
        {
            _filmeRepository = new FilmeRepository();
        }

        /// <summary>
        /// O End-point tem como função listar todos os filmes registrados no sistema.
        /// </summary>
        /// <returns>Retornando um status code 200 juntamente com a listaDeFilmes obtida pela busca.</returns>
        /// <response code="200">Retorna lista de Filmes</response>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            List<FilmeDomain> listaFilme = _filmeRepository.ListarTodos();

            return Ok(listaFilme);
        }

        /// <summary>
        /// Este End-Point Cadastra um novo Filme.
        /// </summary>
        /// <param name="novoFilme">Objeto contento as informações do novo filme a ser cadastrado.</param>
        /// <returns>NoContent (204)</returns>
        /// <response code="201">Filme cadastrado com sucesso.</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post(FilmeDomain novoFilme)
        {
            _filmeRepository.Cadastrar(novoFilme);

            return StatusCode(201);
        }

        /// <summary>
        /// End-Point responsavel por efetuar a exclusão de um Filme, passando o ID pela Url do End-Point
        /// </summary>
        /// <param name="id">Id que será deletado do banco de dados.</param>
        /// <returns> Retorna um status code NoContent-204 </returns>
        /// <response code="204">Filme deletado com sucesso</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete(int id)
        {
            _filmeRepository.Deletar(id);

            return NoContent();
        }

        /// <summary>
        /// Busca Filme por id passado pela URL
        /// </summary>
        /// <param name="id">Id utilizado para pesquisar o filme.</param>
        /// <returns>retorna status code 200, caso contrario retorna NotFound</returns>
        /// <response code="200">Filme encontrado, retornando objeto Filme</response>
        /// <response code="204">Filme não encontrado.</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult BuscarId(int id)
        {
            FilmeDomain filme = _filmeRepository.BuscarPorId(id);

            if (filme != null)
            {
                return Ok(filme);
            }
            return NotFound($"Nenhum Filme foi encontrado com id = {id}.");

        }

        /// <summary>
        /// Atualiza o cadastro de filme pela url
        /// </summary>
        /// <param name="id">Id recebida pela URl para encontrar o filme</param>
        /// <param name="filme">Objeto do tipo FilmeDomain contendo os dados alterados.a</param>
        /// <returns>Retorna Nocontent, caso contrario retorna NotFound</returns>
        /// <response code="204">Filme atualizado com sucesso.</response>
        /// <response code="404">Filme não encontrado.</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizaIdUrl(int id, FilmeDomain filme)
        {
            FilmeDomain filmeExiste = _filmeRepository.BuscarPorId(id);

            if(filmeExiste != null)
            {
                _filmeRepository.AtualizarIdUrl(id, filme);
                return NoContent();
            }

            return NotFound(
                new
                {
                    mensagem = "Filme não encontrado!",
                    erro = true
                });
        }

        /// <summary>
        /// Atuiza cadastro de filme pelo corpo.
        /// </summary>
        /// <param name="filme">Objeto contendo os dados alterados.</param>
        /// <returns>Retorna Nocontent, caso contrario retorna NotFound</returns>
        /// <response code="204">Filme atualizado com sucesso.</response>
        /// <response code="404">Filme não encontrado.</response>
        [Authorize(Roles = "Adminitrador")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult AtualizaIdCorpo(FilmeDomain filme)
        {
            FilmeDomain filmeExiste = _filmeRepository.BuscarPorId(filme.idFilme);

            if (filmeExiste != null)
            {
                _filmeRepository.AtualizarIdCorpo(filme);
                return NoContent();
            }
            return NotFound(
                new
                {
                    mensagem = "Filme não encontrado!",
                    erro = true
                }
                );
        }
    }
}
