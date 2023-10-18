namespace Awktion.Domain.Repos;

/// <summary>
/// Base Repository interface.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface Repo<T>
{
    public List<T> GetAll();
    public T Get(int Id);
    public void Add(T t);
    public void Update(T t);

}
