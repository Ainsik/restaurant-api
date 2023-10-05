using Application.Contracts.Infrastructure;
using Core.Entities;

namespace Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
    {
    }
}