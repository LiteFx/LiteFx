using LiteFx.Repository;
using System;
using System.Threading.Tasks;

namespace LiteFx.AspNet.Identity.Repositories
{
	public interface IIdentityUserRepository<TUser, TId> : IAsyncRepository<TUser, TId>
		where TUser : IdentityUser<TId>
		where TId : IEquatable<TId>
	{
		Task<TUser> GetByNameAsync(string userName);
		Task<string> GetPasswordHashAsync(TId userId);
		Task<TUser> GetByEmailAsync(string email);
	}
}
