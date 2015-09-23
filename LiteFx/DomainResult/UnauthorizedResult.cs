
namespace LiteFx.DomainResult
{
	public class UnauthorizedResult : IDomainResult<string>
	{
		private string body;
		public string Body
		{
			get { return body; }
		}

		public UnauthorizedResult(string message)
		{
			body = message;
		}

		public override bool Equals(object obj)
		{
			var newObj = obj as UnauthorizedResult;
			if (newObj == null)
				return false;

			return ReferenceEquals(this, obj) || Body.Equals(newObj.body);
		}
	}
}