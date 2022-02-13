using Microsoft.EntityFrameworkCore;
using SiteBlog.Adapters;
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

    public async Task AddComment(int postId, AddCommentDto dto)
    {
        var post = await GetPostById(postId);

        if (post is null)
            throw new NotFoundException();

        if (post.Comments == null)
            post.Comments = new List<Comment>();

        post.Comments.Add(CommentAdapter.MapComment(dto, postId));

        await _blogContext.SaveChangesAsync();
    }

    private async Task<Post?> GetPostById(int postId)
    {
        return await _blogContext
            .Posts!
            .Where(e => e.Id == postId)
            .FirstOrDefaultAsync();
    }
}