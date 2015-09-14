using System;

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

		public override bool Equals(Object obj)
		{
			OkResult<T> newObj = obj as OkResult<T>;
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
