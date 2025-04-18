using FluentMigrator.Runner.VersionTableInfo;

namespace Posts.Migrations
{
    public class VersionTable
    {
        [VersionTableMetaData]
        public class CustomVersionTableMetaData : DefaultVersionTableMetaData
        {
            public override string TableName => "__PostsVersions";
        }
    }
}
