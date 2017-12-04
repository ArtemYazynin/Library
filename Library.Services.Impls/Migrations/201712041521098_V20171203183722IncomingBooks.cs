namespace Library.Services.Impls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class V20171203183722IncomingBooks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.InvoiceBooks", "Invoice_Id", "dbo.Invoices");
            DropForeignKey("dbo.InvoiceBooks", "Book_Id", "dbo.Books");
            DropIndex("dbo.InvoiceBooks", new[] { "Invoice_Id" });
            DropIndex("dbo.InvoiceBooks", new[] { "Book_Id" });
            CreateTable(
                "dbo.IncomingBooks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        BookId = c.Long(nullable: false),
                        Count = c.Int(nullable: false),
                        InvoiceId = c.Long(nullable: false),
                        Version = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.InvoiceId);
            
            DropTable("dbo.InvoiceBooks");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InvoiceBooks",
                c => new
                    {
                        Invoice_Id = c.Long(nullable: false),
                        Book_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Invoice_Id, t.Book_Id });
            
            DropForeignKey("dbo.IncomingBooks", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.IncomingBooks", "BookId", "dbo.Books");
            DropIndex("dbo.IncomingBooks", new[] { "InvoiceId" });
            DropIndex("dbo.IncomingBooks", new[] { "BookId" });
            DropTable("dbo.IncomingBooks");
            CreateIndex("dbo.InvoiceBooks", "Book_Id");
            CreateIndex("dbo.InvoiceBooks", "Invoice_Id");
            AddForeignKey("dbo.InvoiceBooks", "Book_Id", "dbo.Books", "Id", cascadeDelete: true);
            AddForeignKey("dbo.InvoiceBooks", "Invoice_Id", "dbo.Invoices", "Id", cascadeDelete: true);
        }
    }
}
