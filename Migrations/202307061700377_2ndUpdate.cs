namespace Books.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2ndUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Books", "Description", c => c.String(nullable: false, maxLength: 1024));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Description", c => c.String());
            AlterColumn("dbo.Books", "Author", c => c.String(maxLength: 128));
        }
    }
}
