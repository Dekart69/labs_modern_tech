using FluentMigrator;

namespace Posts.Migrations
{
    [Migration(202504140001)]
    public class InitialMigration_202504140001 : Migration
    {
        public override void Down()
        {
            throw new NotImplementedException();
        }

        public override void Up()
        {
            Create.Table("Users")
               .WithColumn("Id").AsString(40).NotNullable()
               .WithColumn("Created").AsDate().Nullable()
               .WithColumn("Name").AsString(50).Nullable()
               .WithColumn("Password").AsString(50).Nullable();

            Create.Table("Posts")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Title").AsString(100).Nullable()
                .WithColumn("Content").AsString(1000).Nullable()
                .WithColumn("PublishedAt").AsDate().Nullable()
                .WithColumn("AuthorId").AsString(40).NotNullable();

            Create.Table("Comments")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Content").AsString(1000).Nullable()
                .WithColumn("PublishedAt").AsDate().Nullable()
                .WithColumn("PostId").AsInt32().NotNullable()
                .WithColumn("AuthorId").AsString(40).NotNullable();
        }
    }
}
