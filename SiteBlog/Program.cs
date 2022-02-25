using Microsoft.Extensions.Options;
using SiteBlog.Configuration;
using SiteBlog.Infrastructure.Attributes;
using SiteBlog.Infrastructure.Constants;
using SiteBlog.Repositories.Mongo;
using SiteBlog.Services.Comment;
using SiteBlog.Services.File;
using SiteBlog.Services.Image;
using SiteBlog.Services.Login;
using SiteBlog.Services.Post;
using SiteBlog.Services.Tag;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var configuration = builder.Configuration;

services.AddEndpointsApiExplorer();

services.AddSwaggerGen();

services.AddControllers();

builder.Services.AddResponseCaching();

services.AddTransient<IPostService, PostService>();

services.AddTransient<ICommentService, CommentService>();

services.AddTransient<ITagService, TagService>();

services.AddTransient<IImageService, ImageService>();

services.AddSingleton(typeof(IMongoRepository<>), typeof(MongoRepository<>));

services.AddSingleton<ILoginService, LoginService>();

services.AddSingleton<LocalizationAttribute>();

services.AddSingleton<BasicAuthenticationAttribute>();

services.AddSingleton<IFileService, FileService>();

services.Configure<MongoConfiguration>(configuration.GetSection("MongoConfiguration"));

services.AddSingleton(provider => provider.GetRequiredService<IOptions<MongoConfiguration>>().Value);

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

app.UseResponseCaching();

app.MapControllers();

app.Run();