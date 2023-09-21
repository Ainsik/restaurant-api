namespace Application.Contracts.Infrastructure;
public interface IUnitOfWork : IDisposable
{
    IRestaurantRepository RestaurantRepository { get; }
    Task SaveAsync();
}