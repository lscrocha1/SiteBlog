using Microsoft.EntityFrameworkCore;
using SiteBlog.Adapters;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Context;

namespace SiteBlog.Services;

public class PostService : IPostService
{
    private readonly BlogContext _blogContext;

    public PostService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public async Task CreatePost(CreatePostDto postDto)
    {
        var post = PostAdapter.MapPost(postDto);

        var tagDtos = postDto.Tags;

        if (tagDtos != null && tagDtos.Any())
        {
            var tags = new List<Tag>();

            foreach (var tag in tagDtos)
            {
                if (!tag.Id.HasValue)
                {
                    tags.Add(new Tag
                    {
                        Name = tag.Name
                    });
                } else
                {
                    tags.Add(new Tag
                    {
                        Id = tag.Id.Value
                    });
                }
            }

            await _blogContext.AddRangeAsync(tags);

            await _blogContext.SaveChangesAsync();

            post.Tags = tags.Select(e => new PostTag
            {
                TagId = e.Id
            })
            .ToList();
        }

        await _blogContext.Posts!.AddAsync(post);

        await _blogContext.SaveChangesAsync();
    }

    public async Task<PostDto?> GetPost(int postId)
    {
        var result = await _blogContext
            .Posts!
            .AsNoTracking()
            .Include(e => e.Tags).ThenInclude(e => e.Tag)
            .Include(e => e.Images)
            .Include(e => e.Comments).ThenInclude(e => e.Replies)
            .Where(e => e.Id == postId)
            .FirstOrDefaultAsync();

        if (result is null)
            return null;

        return PostAdapter.MapPostDto(result);
    }

    public async Task<List<PostsDto>> GetPosts(
        string? search = null,
        int? tag = null,
        int page = 1,
        int limit = 10)
    {
        var query = _blogContext
            .Posts!
            .AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(e =>
                e.EnTitle.ToLower().Contains(search.ToLower())
                || e.PtTitle.ToLower().Contains(search.ToLower())
                || e.EnDescription.ToLower().Contains(search.ToLower())
                || e.PtDescription.ToLower().Contains(search.ToLower())
                || e.EnContent.ToLower().Contains(search.ToLower())
                || e.PtContent.ToLower().Contains(search.ToLower()));
        }

        if (tag.HasValue)
        {
            query = query.Where(e => e.Tags.Select(e => e.TagId).Contains(tag.Value));
        }

        var result = await query
            .AsNoTracking()
            .Skip((page - 1) * limit)
            .Take(limit)
            .Select(e => new PostsQueryDto
            {
                Id = e.Id,
                EnTitle = e.EnTitle,
                EnDescription = e.EnDescription,
                EnContent = e.EnContent,
                PtTitle = e.PtTitle,
                PtContent = e.PtContent,
                PtDescription = e.PtDescription,
                ImageDisplay = e.ImageDisplay,
                UpdatedAt = e.UpdatedAt,
                CreatedAt = e.CreatedAt,
                QuantityComments = (e.Comments.Count) + (e.Comments.SelectMany(g => g.Replies).Count()),
                Tags = e.Tags.Select(g => new TagDto
                {
                    Id = g.TagId,
                    Name = g.Tag == null ? string.Empty : g.Tag.Name
                })
                .ToList(),
            })
            .ToListAsync();

        return PostAdapter.MapPostsDto(result);
    }
}