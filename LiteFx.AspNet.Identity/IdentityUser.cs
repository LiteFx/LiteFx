using Microsoft.AspNet.Identity;
using System;

namespace LiteFx.AspNet.Identity
{
	public abstract class IdentityUser<TId> : EntityBaseWithValidation<TId>, IUser<TId>
		where TId : IEquatable<TId>
	{
		public virtual string UserName { get; set; }
		public virtual string Email { get; set; }
		public virtual bool EmailConfirmed { get; set; }
		public virtual string PasswordHash { get; set; }
	}
}