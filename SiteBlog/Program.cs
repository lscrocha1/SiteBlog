using Microsoft.EntityFrameworkCore;
using SiteBlog.Infrastructure.Context;
using SiteBlog.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services.AddDbContext<BlogContext>(optionsAction: opts =>
{
    opts.UseSqlServer(configuration.GetConnectionString("BlogContext"), 
        sqlOpts => sqlOpts.EnableRetryOnFailure(
            maxRetryCount: 3, 
            maxRetryDelay: 
            TimeSpan.FromSeconds(30), null));
});

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.AddControllers();

services.AddTransient<IPostService, PostService>();

services.AddTransient<ICommentService, CommentService>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI(opts =>
{
    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    opts.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();