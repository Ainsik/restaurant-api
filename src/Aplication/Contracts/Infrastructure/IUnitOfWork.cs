namespace Application.Contracts.Infrastructure;
public interface IUnitOfWork : IDisposable
{
    IRestaurantRepository RestaurantRepository { get; }
    IDishRepository DishRepository { get; }
    IUserRepository UserRepository { get; }
    Task SaveAsync();
}