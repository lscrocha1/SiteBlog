using Microsoft.EntityFrameworkCore;
using SiteBlog.Domain;

namespace SiteBlog.Infrastructure.Context;

public class BlogContext : DbContext
{
    public BlogContext()
    {

    }

    public BlogContext(DbContextOptions<DbContext> opts) : base(opts)
    {

    }

    public virtual DbSet<Post>? Posts { get; set; }

    public virtual DbSet<PostTag>? PostTags { get; set; }

    public virtual DbSet<Tag>? Tags { get; set; }

    public virtual DbSet<Image>? Images { get; set; }

    public virtual DbSet<Comment>? Comments { get; set; }

    public virtual DbSet<Reply>? Replies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>(opts =>
        {
            opts.HasKey(e => e.Id);

            opts.HasMany(e => e.Images)
                .WithOne(e => e.Post)
                .OnDelete(DeleteBehavior.Cascade);

            opts.HasMany(e => e.Comments)
                .WithOne(e => e.Post)
                .OnDelete(DeleteBehavior.Cascade);

            opts.HasMany(e => e.Tags)
                .WithOne(e => e.Post)
                .OnDelete(DeleteBehavior.Restrict);

            opts.Property(e => e.ImageDisplay).HasMaxLength(500);
            opts.Property(e => e.EnDescription).HasMaxLength(500);
            opts.Property(e => e.PtDescription).HasMaxLength(500);
            opts.Property(e => e.EnTitle).HasMaxLength(255);
            opts.Property(e => e.PtTitle).HasMaxLength(255);
        });

        modelBuilder.Entity<Comment>(opts =>
        {
            opts.HasKey(e => e.Id);

            opts.HasMany(e => e.Replies)
                .WithOne(e => e.Comment)
                .OnDelete(DeleteBehavior.Cascade);

            opts.Property(e => e.UserName).HasMaxLength(100);
            opts.Property(e => e.Content).HasMaxLength(1000);
        });

        modelBuilder.Entity<Tag>(opts =>
        {
            opts.HasKey(e => e.Id);

            opts.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Image>(opts =>
        {
            opts.HasKey(e => e.Id);

            opts.Property(e => e.Link).HasMaxLength(500);
        });

        modelBuilder.Entity<Reply>(opts =>
        {
            opts.Property(e => e.UserName).HasMaxLength(100);
            opts.Property(e => e.Content).HasMaxLength(1000);
        });

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("BlogContext");

            optionsBuilder.UseSqlServer(connectionString, opts =>
            {
                opts.EnableRetryOnFailure(maxRetryCount: 3, maxRetryDelay: TimeSpan.FromSeconds(30), null);
            });
        }

        base.OnConfiguring(optionsBuilder);
    }
}