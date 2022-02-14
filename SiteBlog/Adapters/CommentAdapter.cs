using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Adapters;

public static class CommentAdapter
{
    public static Comment MapComment(AddCommentDto addCommentDto, int postId)
    {
        return new Comment
        {
            PostId = postId,
            CreatedAt = DateTime.Now,
            Content = addCommentDto.Content,
            UserName = addCommentDto.UserName
        };
    }

    public static Reply MapReply(ReplyCommentDto addCommentDto)
    {
        return new Reply
        {
            CreatedAt = DateTime.Now,
            Content = addCommentDto.Content,
            UserName = addCommentDto.UserName,
            ReplyingToId = addCommentDto.ReplyingToId
        };
    }
}