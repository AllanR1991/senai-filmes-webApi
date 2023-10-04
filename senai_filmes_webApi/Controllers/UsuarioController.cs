using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using senai_filmes_webApi.Domains;
using senai_filmes_webApi.Interfaces;
using senai_filmes_webApi.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace senai_filmes_webApi.Controllers
{
    //Define que o tpo de resposta da API será no formato JSON
    [Produces("application/json")]
    
    //Define que a rota de uma requisição sera no formato dominio/api/nomeController
    //Ex: http://localhost:5000/api/Usuarios
    [Route("api/[controller]")]

    //Define que é um controlador de API
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Objeto _usuarioRepository que irá receber todos os métodos definidos na interface IUsuarioRepository
        /// </summary>
        private IUsuarioRepository _usuarioRepository {  get; set; }
        
        /// <summary>
        /// Objeto _acessoRepository que irá receber todos os métodos definidos na interface IAcessoRepository
        /// </summary>
        private IAcessoRepository _acessoRepository { get; set;}

        public UsuarioController()
        {
            _acessoRepository = new AcessoRepository();
            _usuarioRepository = new UsuarioRepository();
        }

        /// <summary>
        /// Efetua login de usuarios
        /// </summary>
        /// <param name="login">Objeto do tipo UsuarioDomain</param>
        /// <returns>Retorna token, caso contrario retorna NotFound</returns>
        /// <response code="200">Retorna token</response>
        /// <response code="404">E-mail ou senha inválidos!</response>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Login(UsuarioDomain login)
        {
            UsuarioDomain usuarioBuscado = _usuarioRepository.BuscarPorEmailSenha(login.email, login.senha);

            AcessoDomain buscaAcesso;
            /*
             IdentityModel.Tokens.Jwt
             Microsoft.AspNetCore.Authentication.JwtBear -> cuidado a versão dele depende da versao do aspDotNetCore.
             */
            if (usuarioBuscado != null)
            {
                buscaAcesso = _acessoRepository.BuscaPorID(usuarioBuscado.idAcesso);

                //Define os dados que serão fornecidos no token
                var claims = new[]
                {
                    //TipoDaClaim, ValorDaClaim
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.email),
                    //Claim trabalha com texto
                    new Claim(JwtRegisteredClaimNames.Jti,usuarioBuscado.idUsuario.ToString()),
                    new Claim(ClaimTypes.Role, buscaAcesso.acesso),
                    new Claim("Claim Personalizada", "Valor Teste")
                };

                //Define a chave de acesso ao token
                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("543532hkjdfggbfsdghsfdhgrtsht-3453456t45yghrts-hr45t4y2htsghe54"));

                //Define as credenciais do token - Header
                var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

                //Define a composição do token
                var token = new JwtSecurityToken(
                        issuer: "Filmes.webApi",                    //Emissor do Token
                        audience : "Filmes.webApi",                 //Destinatario do token
                        claims : claims,                            //dados definidos acima.
                        expires : DateTime.Now.AddMinutes(30),      //Tempo de expiração do token.
                        signingCredentials : creds                  //Credenciais do token
                    );

                //Retornar um status code 200 - OK com o token Criado
                return Ok(new
                {
                    //Gera o token
                    token = new JwtSecurityTokenHandler().WriteToken(token)

                    //https://jwt.io/ para testar o token
                });

            }

                return NotFound("E-mail ou senha inválidos!");
        }
    }
}
