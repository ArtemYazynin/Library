namespace Library.Services.Impls.Services.Builders
{
	class ExpressionBuilderDirector
	{
		public void Construct(AbstractExpressionBuilder builder)
		{
			builder.FilterByName();
			builder.FilterByAuthor();
			builder.FilterByAuthors();
			builder.FilterByAll();
		}
	}
}