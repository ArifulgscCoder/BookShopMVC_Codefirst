namespace BookShopMVCmidExam_1280425.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        BookName = c.String(nullable: false, maxLength: 30),
                        AuthorName = c.String(nullable: false, maxLength: 30),
                        PublishDate = c.DateTime(nullable: false),
                        EmailAddress = c.String(),
                        Picture = c.String(),
                        InStock = c.Boolean(nullable: false),
                        Price = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.GenreEntries",
                c => new
                    {
                        GenreEntriesId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GenreEntriesId)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.GenreId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false),
                        GenreTitle = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GenreEntries", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.GenreEntries", "BookId", "dbo.Books");
            DropIndex("dbo.GenreEntries", new[] { "GenreId" });
            DropIndex("dbo.GenreEntries", new[] { "BookId" });
            DropTable("dbo.Genres");
            DropTable("dbo.GenreEntries");
            DropTable("dbo.Books");
        }
    }
}
