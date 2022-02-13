using Microsoft.EntityFrameworkCore;
using SiteBlog.Domain;
using SiteBlog.Dto;
using SiteBlog.Infrastructure.Context;
using SiteBlog.Infrastructure.Exceptions;

namespace SiteBlog.Services;

public class CommentService : ICommentService
{
    private readonly BlogContext _blogContext;

    public CommentService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public async Task AddComment(PostId postId, AddCommentDto dto)
    {
        var post = await GetPostById(postId);

        if (post is null)
            throw new NotFoundException();
    }

    private async Task<Post?> GetPostById(PostId postId)
    {
        return await _blogContext
            .Posts!
            .Where(e => e.Id == postId)
            .FirstOrDefaultAsync();
    }
}