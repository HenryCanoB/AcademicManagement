namespace Galaxy.AcademicMagement.Application.Contracts.Services
{
    public interface IAcademicManagementUnitOfWork
    {
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task RollbackTransactionAsync();
        Task CommitTransactionAsync();
    }
}
