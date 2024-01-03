using Blog.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers() // Adicionando os controllers a API
    .ConfigureApiBehaviorOptions(options => // "Configurar o comportamento da API"
    {
        options.SuppressModelStateInvalidFilter = true; 
        // vai bloquear a aplicacao de validacao automatica com o ModelState do ASP .NET
    });



builder.Services.AddDbContext<BlogDataContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
 