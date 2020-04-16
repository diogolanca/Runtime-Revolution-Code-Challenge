namespace CodeChallenge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class databaseupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.short_urls", "full_url", c => c.String(nullable: false, maxLength: 1000));
            DropColumn("dbo.short_urls", "long_url");
        }
        
        public override void Down()
        {
            AddColumn("dbo.short_urls", "long_url", c => c.String(nullable: false, maxLength: 1000));
            DropColumn("dbo.short_urls", "full_url");
        }
    }
}
