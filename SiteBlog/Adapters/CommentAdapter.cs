using SiteBlog.Domain;
using SiteBlog.Dto;

namespace SiteBlog.Adapters;

public static class CommentAdapter
{
    public static Comment MapComment(AddCommentDto addCommentDto)
    {
        return new Comment
        {
            Approved = null,
            Content = addCommentDto.Content,
            UserName = addCommentDto.UserName,
        };
    }

    public static Reply MapReply(ReplyCommentDto addCommentDto)
    {
        return new Reply
        {
            Approved = null,
            Content = addCommentDto.Content,
            UserName = addCommentDto.UserName,
            ReplyingToId = addCommentDto.ReplyingToId
        };
    }

    public static CommentToBeApprovedDto[] MapCommentToBeApproved(List<Comment> comments)
    {
        var replies = comments
            .SelectMany(e => e.Replies)
            .Where(e => e.Approved == null)
            .Select(e => new CommentToBeApprovedDto
            {
                Id = e.Id,
                IsReply = true,
                Content = e.Content,
                Username = e.UserName
            })
            .ToList();

        var result = comments.Select(e => new CommentToBeApprovedDto
        {
            Id = e.Id,
            Content = e.Content,
            Username = e.UserName
        })
        .ToList();

        result.AddRange(replies);

        return result.ToArray();
    }
}