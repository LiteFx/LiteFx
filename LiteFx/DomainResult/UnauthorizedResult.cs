using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	}
}
