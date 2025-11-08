using Galaxy.AcademicMagement.Domain.Entities;

namespace Galaxy.AcademicMagement.Application.Contracts.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity entity);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<ICollection<TEntity>> ListAsync();
    }
}
