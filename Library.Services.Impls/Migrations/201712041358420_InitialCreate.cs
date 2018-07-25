namespace Library.Services.Impls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Lastname = c.String(nullable: false, maxLength: 255),
                        Firstname = c.String(nullable: false, maxLength: 255),
                        Middlename = c.String(maxLength: 255),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Lastname, t.Firstname }, unique: true, name: "UX_LastnameFirstname");
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Isbn = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 2000),
                        Count = c.Int(nullable: false),
                        CountAvailable = c.Int(nullable: false),
                        PublisherId = c.Long(nullable: false),
                        CoverId = c.Long(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Edition_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Editions", t => t.Edition_Id, cascadeDelete: true)
                .ForeignKey("dbo.Publishers", t => t.PublisherId, cascadeDelete: true)
                .Index(t => t.Isbn, unique: true, name: "UX_ISBN")
                .Index(t => t.PublisherId)
                .Index(t => t.Edition_Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        ContentType = c.String(),
                        Content = c.Binary(),
                        BookId = c.Int(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Editions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 300),
                        Year = c.Int(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Name, t.Year }, unique: true, name: "UX_EditionNameYear");
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255),
                        ParentId = c.Long(),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.ParentId)
                .Index(t => t.Name, unique: true, name: "UX_GenreName")
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 1000),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UX_PublisherName");
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BookId = c.Long(nullable: false),
                        SubscriberId = c.Long(nullable: false),
                        Count = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subscribers", t => t.SubscriberId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.SubscriberId);
            
            CreateTable(
                "dbo.Subscribers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Lastname = c.String(nullable: false, maxLength: 255),
                        Firstname = c.String(nullable: false, maxLength: 255),
                        Middlename = c.String(maxLength: 255),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => new { t.Lastname, t.Firstname }, unique: true, name: "UX_LastnameFirstname");
            
            CreateTable(
                "dbo.BookGenres",
                c => new
                    {
                        Book_Id = c.Long(nullable: false),
                        Genre_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Book_Id, t.Genre_Id })
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .Index(t => t.Book_Id)
                .Index(t => t.Genre_Id);
            
            CreateTable(
                "dbo.InvoiceBooks",
                c => new
                    {
                        Invoice_Id = c.Long(nullable: false),
                        Book_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Invoice_Id, t.Book_Id })
                .ForeignKey("dbo.Invoices", t => t.Invoice_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Invoice_Id)
                .Index(t => t.Book_Id);
            
            CreateTable(
                "dbo.AuthorBooks",
                c => new
                    {
                        Author_Id = c.Long(nullable: false),
                        Book_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Author_Id, t.Book_Id })
                .ForeignKey("dbo.Authors", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.Author_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AuthorBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.AuthorBooks", "Author_Id", "dbo.Authors");
            DropForeignKey("dbo.Rents", "BookId", "dbo.Books");
            DropForeignKey("dbo.Rents", "SubscriberId", "dbo.Subscribers");
            DropForeignKey("dbo.Books", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.InvoiceBooks", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.InvoiceBooks", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.BookGenres", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.BookGenres", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Genres", "ParentId", "dbo.Genres");
            DropForeignKey("dbo.Books", "Edition_Id", "dbo.Editions");
            DropForeignKey("dbo.Files", "Id", "dbo.Books");
            DropIndex("dbo.AuthorBooks", new[] { "Book_Id" });
            DropIndex("dbo.AuthorBooks", new[] { "Author_Id" });
            DropIndex("dbo.InvoiceBooks", new[] { "Book_Id" });
            DropIndex("dbo.InvoiceBooks", new[] { "Invoice_Id" });
            DropIndex("dbo.BookGenres", new[] { "Genre_Id" });
            DropIndex("dbo.BookGenres", new[] { "Book_Id" });
            DropIndex("dbo.Subscribers", "UX_LastnameFirstname");
            DropIndex("dbo.Rents", new[] { "SubscriberId" });
            DropIndex("dbo.Rents", new[] { "BookId" });
            DropIndex("dbo.Publishers", "UX_PublisherName");
            DropIndex("dbo.Genres", new[] { "ParentId" });
            DropIndex("dbo.Genres", "UX_GenreName");
            DropIndex("dbo.Editions", "UX_EditionNameYear");
            DropIndex("dbo.Files", new[] { "Id" });
            DropIndex("dbo.Books", new[] { "Edition_Id" });
            DropIndex("dbo.Books", new[] { "PublisherId" });
            DropIndex("dbo.Books", "UX_ISBN");
            DropIndex("dbo.Authors", "UX_LastnameFirstname");
            DropTable("dbo.AuthorBooks");
            DropTable("dbo.InvoiceBooks");
            DropTable("dbo.BookGenres");
            DropTable("dbo.Subscribers");
            DropTable("dbo.Rents");
            DropTable("dbo.Publishers");
            DropTable("dbo.Invoices");
            DropTable("dbo.Genres");
            DropTable("dbo.Editions");
            DropTable("dbo.Files");
            DropTable("dbo.Books");
            DropTable("dbo.Authors");
        }
    }
}
