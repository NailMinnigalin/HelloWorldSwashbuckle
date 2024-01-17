namespace Core
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
	public class SwaggerTagAttribute : Attribute
	{
		public string[] Tags { get; private set; }

		public SwaggerTagAttribute(params string[] tags)
		{
			Tags = tags;
		}
	}
}
