namespace Domain.Repositories.Sqlite;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    Task<bool> Complete();
}
