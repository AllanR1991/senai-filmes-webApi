// Importar o namespace necessário para usar as classes do OpenAPI.
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

// Criação do objeto 'builder' para configurar e construir a aplicação web.
var builder = WebApplication.CreateBuilder(args);

// Adiciona serviços relacionados a controladores à coleção de serviços da aplicação.
builder.Services.AddControllers();

//Definindo a forma de autenticação
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

        //quem está recebendo
        ValidateAudience = true,

        //o tempo de expiração
        ValidateLifetime = true,

        //forma de criptografia e a chave de autenticação
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("543532hkjdfggbfsdghsfdhgrtsht-3453456t45yghrts-hr45t4y2htsghe54")),

        //tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(30),

        //nome do issuer, de onde está vindo
        ValidIssuer = "Filmes.webApi",

        //nome do audience, para onde esta indo.
        ValidAudience = "Filmes.webApi"

    };
});

// Adiciona serviços relacionados à exploração de endpoints à coleção de serviços da aplicação.
builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger para gerar documentação da API.
builder.Services.AddSwaggerGen(options =>
{
    // Define a versão e informações básicas sobre a API.
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Filmes",
        Description = "Api desenvolvida para testarmos as rotas e os Json Gerados.",
        // Comentado: TermsOfService não definido.
        //TermsOfService = new Uri("https://example.com/terms"),
        // Define informações de contato.
        Contact = new OpenApiContact
        {
            Name = "Contato",
            Url = new Uri("https://github.com/AllanR1991")
        }//,
        // Comentado: Informações de licença não definidas.
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



// Constrói a instância da aplicação web com base nas configurações fornecidas.
var app = builder.Build();

// Verifica se a aplicação está em modo de desenvolvimento.
if (app.Environment.IsDevelopment())
{
    // Habilita o Swagger para geração de documentação.
    app.UseSwagger();

    // Configura o Swagger UI para exibir a documentação gerada.
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

// Middleware de redirecionamento HTTPS.
app.UseHttpsRedirection();

//app.UseStaticFiles();

// Mapeia os controladores para lidar com solicitações HTTP.
app.MapControllers();

//Adiciona autenticação
app.UseAuthentication();

//Adiciona autorização
app.UseAuthorization();

// Inicia a execução da aplicação.
app.Run();
