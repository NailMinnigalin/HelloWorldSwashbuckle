namespace Core
{
	[AttributeUsage(AttributeTargets.Class)]
	public class SwaggerGroupAttribute : Attribute
	{
		public string GroupName { get; }

		public SwaggerGroupAttribute(string groupName)
		{
			GroupName = groupName;
		}
	}
}
