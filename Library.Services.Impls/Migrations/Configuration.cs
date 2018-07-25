namespace Library.Services.Impls.Migrations
{
	using System.Data.Entity.Migrations;

	internal sealed class Configuration : DbMigrationsConfiguration<LibraryContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Library.Services.Impls.LibraryContext";
        }

        protected override void Seed(LibraryContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
