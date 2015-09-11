
namespace LiteFx.DomainResult
{
	public class ForbiddenResult : IDomainResult<string>
	{
		private string body;
		public string Body
		{
			get { return body; }
		}

		public ForbiddenResult(string message)
		{
			body = message;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is ForbiddenResult))
				return false;

			if (!this.body.Equals((obj as ForbiddenResult).body))
				return false;

			return true;
		}
	}
}