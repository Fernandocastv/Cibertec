namespace APP_LIBROS_CRUD.Repositorios.Contrato
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> FindAll();
        Task<bool> Save(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
    }
}
