namespace Service
{
	public class Successful : BaseResponse
	{ }

	public class Failure : BaseResponse
	{ }

	public abstract class BaseResponse
	{
		public string Description;

		public override string ToString()
		{
			return $"{{ \"Status\" : \"{this.GetType().Name}\", \"Description\" : \"{this.Description}\" }}"; 
		}
	}
}