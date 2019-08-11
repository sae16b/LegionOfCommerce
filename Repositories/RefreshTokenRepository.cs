using Contracts.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Entities.Extensions;
using System.Linq.Expressions;
using System.Text;

namespace Repositories
{
	public class RefreshTokenRepository : Repository<RefreshToken>, IRefreshTokenRepository
	{
		public RefreshTokenRepository(LegionContext context)
			: base(context)
		{ }

	}
}
