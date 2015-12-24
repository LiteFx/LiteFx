using LiteFx.AspNet.Identity.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteFx.AspNet.Identity
{
	public class UserStore<TUser, TId> : IUserStore<TUser, TId>, IUserPasswordStore<TUser, TId>, IUserEmailStore<TUser, TId>
		where TUser : IdentityUser<TId>
		where TId : IEquatable<TId>
	{
		private bool _disposed;
		protected IIdentityUserRepository<TUser, TId> IdentityUserRepository { get; set; }

		public UserStore(IIdentityUserRepository<TUser, TId> identityUserRepository)
		{
			IdentityUserRepository = identityUserRepository;
		}

		public Task CreateAsync(TUser user)
		{
			return IdentityUserRepository.SaveAsync(user);
		}

		public Task DeleteAsync(TUser user)
		{
			return IdentityUserRepository.DeleteAsync(user);
		}

		public Task<TUser> FindByIdAsync(TId userId)
		{
			return IdentityUserRepository.GetByIdAsync(userId);
		}

		public Task<TUser> FindByNameAsync(string userName)
		{
			return IdentityUserRepository.GetByNameAsync(userName);
		}

		public Task UpdateAsync(TUser user)
		{
			return IdentityUserRepository.SaveAsync(user);
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			this._disposed = true;
			this.IdentityUserRepository = null;
		}

		public Task<string> GetPasswordHashAsync(TUser user)
		{
			return IdentityUserRepository.GetPasswordHashAsync(user.Id);
		}

		public Task<bool> HasPasswordAsync(TUser user)
		{
			return Task.FromResult<bool>(string.IsNullOrWhiteSpace(user.PasswordHash));
		}

		public Task SetPasswordHashAsync(TUser user, string passwordHash)
		{
			user.PasswordHash = passwordHash;
			return IdentityUserRepository.SaveAsync(user);
		}

		public Task<TUser> FindByEmailAsync(string email)
		{
			throw new NotImplementedException();
		}

		public Task<string> GetEmailAsync(TUser user)
		{
			string email = IdentityUserRepository.GetById(user.Id).Email;
			return Task.FromResult(email);
		}

		public Task<bool> GetEmailConfirmedAsync(TUser user)
		{
			bool emailConfirmed = IdentityUserRepository.GetById(user.Id).EmailConfirmed;
			return Task.FromResult(emailConfirmed);
		}

		public Task SetEmailAsync(TUser user, string email)
		{
			user.Email = email;
			return IdentityUserRepository.SaveAsync(user);
		}

		public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
		{
			user.EmailConfirmed = confirmed;
			return IdentityUserRepository.SaveAsync(user);
		}
	}
}
