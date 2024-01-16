using Swashbuckle.AspNetCore.Filters;

namespace ProductsModule
{
	public class MultiplyBy2Example : IExamplesProvider<int>
	{
		public int GetExamples()
		{
			return 5;
		}
	}
}
