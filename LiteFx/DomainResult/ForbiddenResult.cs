using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiteFx.DomainResult
{
	public class ForbiddenResult: IDomainResult<string>
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
	}
}
