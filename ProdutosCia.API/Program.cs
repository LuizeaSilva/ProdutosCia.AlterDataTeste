using ProdutosCia.API.Providers;
using ProdutosCia.CrossCutting.IoC;
using ProdutosCia.CrossCutting.IoC.Middlewares;

var builder = WebApplication.CreateBuilder(args);
InjectorContainer.Register(builder);
builder.Services.AddMvcProvider();
builder.Services.AddAuthProvider(builder.Configuration);
builder.Services.AddSwaggerProvider(builder.Configuration);

var app = builder.Build();

app.UseErrorHandlingMiddleware();
app.UseMvcProvider();
app.UseSwaggerProvider();
app.UseAuthorization();

app.Run();
