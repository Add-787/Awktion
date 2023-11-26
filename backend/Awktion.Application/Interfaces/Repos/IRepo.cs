namespace Awktion.Application.Interfaces.IRepos;

/// <summary>
/// Base IRepository interface.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepo<T>
{
    public Task<IList<T>> GetAll();
    public Task<T> Get(int Id);
    public Task<T> Add(T t);
    public Task Update(T t);
}