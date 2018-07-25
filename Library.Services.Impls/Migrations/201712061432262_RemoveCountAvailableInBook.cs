namespace Library.Services.Impls.Migrations
{
	using System.Data.Entity.Migrations;
    
	public partial class RemoveCountAvailableInBook : DbMigration
	{
		public override void Up()
		{
			DropColumn("dbo.Books", "CountAvailable");
		}
        
		public override void Down()
		{
			AddColumn("dbo.Books", "CountAvailable", c => c.Int(nullable: false));
		}
	}
}
