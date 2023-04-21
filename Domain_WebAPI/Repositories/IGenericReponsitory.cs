namespace Domain_WebAPI.Repositories
{
    public interface IGenericRepository<T>
    {
        public List<T> GetAll();
        public T GetById(int id);
        public T Add(T entity);
        public T UpdateById(int id, T entity);
        public T DeteleById(int id);

    }
}
