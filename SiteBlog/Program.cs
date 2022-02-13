using Microsoft.EntityFrameworkCore;
using SiteBlog.Infrastructure.Attributes;
using SiteBlog.Infrastructure.Constants;
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

services.AddTransient<ITagService, TagService>();

services.AddSingleton<LocalizationAttribute>();

const string corsName = "blog-cors-policy";

services.AddCors(opts =>
{
    opts.AddPolicy(corsName, corsOpts =>
    {
        var urls = configuration.GetSection("AppUrls").Get<string[]>();

        corsOpts.WithOrigins(urls).AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BlogContext>();

    db.Database.Migrate();
}

app.UseSwagger();

app.UseSwaggerUI(opts =>
{
    opts.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    opts.RoutePrefix = string.Empty;
});

var supportedCultures = new[] { Constants.Language.English, Constants.Language.Portuguese };

var localizationOptions = new RequestLocalizationOptions
    {
        ApplyCurrentCultureToResponseHeaders = true,
    }
    .SetDefaultCulture(supportedCultures[1])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures();

app.UseRequestLocalization(localizationOptions);

app.UseCors(corsName);

app.MapControllers();

app.Run();