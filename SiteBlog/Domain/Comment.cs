﻿namespace SiteBlog.Domain;

public class Comment
{
    public Comment()
    {
        UserName = string.Empty;
        Content = string.Empty;
        Replies = new List<Reply>();
    }

    public int Id { get; set; }

    public string UserName { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public int PostId { get; set; }

    public Post? Post { get; set; }

    public IList<Reply> Replies { get; set; }
}