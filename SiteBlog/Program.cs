using SiteBlog.Infrastructure.Attributes;
using SiteBlog.Infrastructure.Constants;
using SiteBlog.Repositories.Mongo;
using SiteBlog.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.AddControllers();

services.AddTransient<IPostService, PostService>();

services.AddTransient<ICommentService, CommentService>();

services.AddTransient<ITagService, TagService>();

services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));

services.AddSingleton<LocalizationAttribute>();

services.AddSingleton<BasicAuthenticationAttribute>();

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