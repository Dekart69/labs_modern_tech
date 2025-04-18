using System.Data;

namespace Posts.Data
{
    public interface IDapperDbContext
    {
        public IDbConnection CreateConnection();
    }
}