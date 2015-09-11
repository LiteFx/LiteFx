
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
			if (!(obj is UnauthorizedResult))
				return false;

			if (!this.body.Equals((obj as UnauthorizedResult).body))
				return false;

			return true;
		}
	}
}
