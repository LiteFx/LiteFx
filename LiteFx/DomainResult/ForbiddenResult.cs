
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
			ForbiddenResult newObj = obj as ForbiddenResult;
			if ((object)newObj == null)
				return false;

			if (object.ReferenceEquals(this, obj))
				return true;

			var body = newObj.body;
			if (body.GetType().IsPrimitive || body is string)
				return this.Body.Equals(body);
			else
				return this.body.GetType().Equals(body.GetType());
		}
	}
}