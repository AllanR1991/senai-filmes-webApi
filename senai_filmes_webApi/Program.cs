// Importar o namespace necess�rio para usar as classes do OpenAPI.
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

// Cria��o do objeto 'builder' para configurar e construir a aplica��o web.
var builder = WebApplication.CreateBuilder(args);

// Adiciona servi�os relacionados a controladores � cole��o de servi�os da aplica��o.
builder.Services.AddControllers();

//Definindo a forma de autentica��o
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //quem esta emitindo
        ValidateIssuer = true,

        //quem est� recebendo
        ValidateAudience = true,

        //o tempo de expira��o
        ValidateLifetime = true,

        //forma de criptografia e a chave de autentica��o
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("543532hkjdfggbfsdghsfdhgrtsht-3453456t45yghrts-hr45t4y2htsghe54")),

        //tempo de expira��o do token
        ClockSkew = TimeSpan.FromMinutes(30),

        //nome do issuer, de onde est� vindo
        ValidIssuer = "Filmes.webApi",

        //nome do audience, para onde esta indo.
        ValidAudience = "Filmes.webApi"

    };
});

// Adiciona servi�os relacionados � explora��o de endpoints � cole��o de servi�os da aplica��o.
builder.Services.AddEndpointsApiExplorer();

// Configura��o do Swagger para gerar documenta��o da API.
builder.Services.AddSwaggerGen(options =>
{
    // Define a vers�o e informa��es b�sicas sobre a API.
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Filmes",
        Description = "Api desenvolvida para testarmos as rotas e os Json Gerados.",
        // Comentado: TermsOfService n�o definido.
        //TermsOfService = new Uri("https://example.com/terms"),
        // Define informa��es de contato.
        Contact = new OpenApiContact
        {
            Name = "Contato",
            Url = new Uri("https://github.com/AllanR1991")
        }//,
        // Comentado: Informa��es de licen�a n�o definidas.
        //License = new OpenApiLicense
        //{
        //    Name = "Example License",
        //    Url = new Uri("https://example.com/license")
        //}
    });
  
    //Habilita o comentarios feito no codigo atraves do summary para o swagger
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Value: Bearer TokenJWT ",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});



// Constr�i a inst�ncia da aplica��o web com base nas configura��es fornecidas.
var app = builder.Build();

// Verifica se a aplica��o est� em modo de desenvolvimento.
if (app.Environment.IsDevelopment())
{
    // Habilita o Swagger para gera��o de documenta��o.
    app.UseSwagger();

    // Configura o Swagger UI para exibir a documenta��o gerada.
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// Middleware de redirecionamento HTTPS.
app.UseHttpsRedirection();

//app.UseStaticFiles();

// Mapeia os controladores para lidar com solicita��es HTTP.
app.MapControllers();

//Adiciona autentica��o
app.UseAuthentication();

//Adiciona autoriza��o
app.UseAuthorization();

// Inicia a execu��o da aplica��o.
app.Run();
