namespace Library.Services.Impls.Migrations
{
	using System.Data.Entity.Migrations;
    
	public partial class AddIsDeletedPropertyInSubscriber : DbMigration
	{
		public override void Up()
		{
			AddColumn("dbo.Subscribers", "IsDeleted", c => c.Boolean(nullable: false, defaultValue: false));
		}
		public override void Down()
		{
			DropColumn("dbo.Subscribers", "IsDeleted");
		}
	}
}
