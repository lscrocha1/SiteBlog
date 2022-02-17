using MongoDB.Bson;
using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Adapters;

public static class CommentAdapter
{
    public static Comment MapComment(AddCommentDto addCommentDto)
    {
        return new Comment
        { 
            Approved = false,
            Content = addCommentDto.Content,
            UserName = addCommentDto.UserName,
        };
    }

    public static Reply MapReply(ReplyCommentDto addCommentDto)
    {
        return new Reply
        {
            Approved = false,
            Content = addCommentDto.Content,
            UserName = addCommentDto.UserName,
            ReplyingToId = addCommentDto.ReplyingToId
        };
    }
}