using Microsoft.AspNetCore.Identity;

namespace Posts.Entities
{
    public class User
    {
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
}
