namespace Library.Services.Impls.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BookEdition : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Books", name: "Edition_Id", newName: "EditionId");
            RenameIndex(table: "dbo.Books", name: "IX_Edition_Id", newName: "IX_EditionId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Books", name: "IX_EditionId", newName: "IX_Edition_Id");
            RenameColumn(table: "dbo.Books", name: "EditionId", newName: "Edition_Id");
        }
    }
}
