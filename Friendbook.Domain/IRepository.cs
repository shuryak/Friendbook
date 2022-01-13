namespace Friendbook.Domain;

public interface IRepository<T> where T : class
{
    void Create(T entity);
    IEnumerable<T> GetList();
    T Get(int id);
    void Update(T entity);
    void Delete(int id);
}
