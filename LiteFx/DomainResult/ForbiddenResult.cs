
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
			var newObj = obj as ForbiddenResult;
			if (newObj == null)
				return false;

			return ReferenceEquals(this, obj) || Body.Equals(newObj.body);
		}
	}
}