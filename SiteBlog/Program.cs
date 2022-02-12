using SiteBlog.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.AddControllers();

services.AddTransient<IPostService, PostService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(opts =>
{
    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    opts.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();