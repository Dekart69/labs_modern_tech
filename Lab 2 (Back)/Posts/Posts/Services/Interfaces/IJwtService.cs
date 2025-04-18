namespace Posts.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAsync(string username, string userId, CancellationToken cancellationToken);
    }
}
