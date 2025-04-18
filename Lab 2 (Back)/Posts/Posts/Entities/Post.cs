﻿namespace Posts.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public string AuthorId { get; set; }
    }
}
