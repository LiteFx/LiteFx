
namespace LiteFx.DomainResult
{
	public class OkResult : OkResult<string>
	{
		public OkResult()
		{
			body = string.Empty;
		}
	}

	public class OkResult<T> : IDomainResult<T>
	{
		protected T body;
		public T Body { get { return body; } }

		public OkResult() { }

		public OkResult(T body)
		{
			this.body = body;
		}

		public override bool Equals(object obj)
		{
			var newObj = obj as OkResult<T>;
			if (newObj == null)
				return false;

			return ReferenceEquals(this, obj) || Body.Equals(newObj.body);
		}
	}
}
