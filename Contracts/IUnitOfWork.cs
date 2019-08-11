using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
	public interface IUnitOfWork : IDisposable
	{
		IUserRepository User { get; }
		IRefreshTokenRepository RefreshToken { get; }
		int Complete();
	}
}
