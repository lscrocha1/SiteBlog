using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Adapters;

public static class CommentAdapter
{
    public static Comment MapComment(AddCommentDto addCommentDto, int postId)
    {
        return new Comment
        {
            CreatedAt = DateTime.Now,
            PostId = new PostId(postId),
            Content = addCommentDto.Content,
            UserName = addCommentDto.UserName
        };
    }
}