namespace TestRira.Repo
{
    public interface IRepository<TEntity> where TEntity :class
    {
        Task InsertAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);

        Task<List<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(int Id);

        Task UpdateAsync(TEntity entity);
        Task RemoveAsync(TEntity entity);
    }
}
