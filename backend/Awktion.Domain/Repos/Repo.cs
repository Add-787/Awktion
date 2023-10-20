namespace Awktion.Domain.Repos;

/// <summary>
/// Base Repository interface.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface Repo<T>
{
    public IList<T> GetAll();
    public T Get(int Id);
    public void Add(T t);
    public void Update(T t);

}

/// <summary>
/// Exception that is thrown when element not found in repo.
/// </summary>
/// <typeparam name="T"></typeparam>
public class NotFoundException<T> : Exception {}

/// <summary>
/// Exception thrown when element already exists in repo.
/// </summary>
/// <typeparam name="T"></typeparam>
public class AlreadyExistsException<T> : Exception {}


