using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace LiteFx
{
	public interface IDomainResult
	{
	}

	public class ErrorResult : IDomainResult
	{
		List<ValidationResult> messages;
		public IEnumerable<ValidationResult> Messages
		{
			get
			{
				return messages.AsEnumerable();
			}
		}

		public ErrorResult()
		{
			messages = new List<ValidationResult>();
		}

		public ErrorResult(string message)
			: this()
		{
			AddMessage(message);
		}

		public ErrorResult(string key, string message)
			: this()
		{
			AddMessage(key, message);
		}

		public ErrorResult AddMessage(string key, string message)
		{
			messages.Add(new ValidationResult(message, new string[] { key }));
			return this;
		}

		public ErrorResult AddMessage(string message)
		{
			messages.Add(new ValidationResult(message));
			return this;
		}

		public ErrorResult AddMessageIf(string key, string message, Func<bool> lambda)
		{
			if (lambda())
				messages.Add(new ValidationResult(message, new string[] { key }));
			return this;
		}

		public ErrorResult AddMessageIf(string message, Func<bool> lambda)
		{
			if (lambda())
				messages.Add(new ValidationResult(message));
			return this;
		}
	}

	public class OkResult : OkResult<string>
	{
		public OkResult()
		{
			body = string.Empty;
		}
	}

	public class OkResult<T> : IDomainResult
	{
		protected T body;
		public T Body { get { return body; } }

		public OkResult() { }

		public OkResult(T body)
		{
			this.body = body;
		}
	}
}
