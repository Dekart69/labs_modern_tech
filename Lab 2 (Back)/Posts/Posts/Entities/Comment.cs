namespace Posts.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PublishedAt { get; set; }
        public int PostId { get; set; }
        public string AuthorId { get; set; }
    }
}
