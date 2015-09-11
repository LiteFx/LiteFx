using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
			if (!(obj is OkResult))
				return false;

			if (!this.body.Equals((obj as OkResult<T>).body))
				return false;

			return true;
		}
	}
}
