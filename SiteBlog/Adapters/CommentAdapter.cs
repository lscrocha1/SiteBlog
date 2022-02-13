using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Adapters;

public static class CommentAdapter
{
    public static Comment MapComment(AddCommentDto addCommentDto, PostId postId)
    {
        return new Comment
        {
            PostId = postId,
            CreatedAt = DateTime.Now,
            Content = addCommentDto.Content,
            UserName = addCommentDto.UserName
        };
    }
}