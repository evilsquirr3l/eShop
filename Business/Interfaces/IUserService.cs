using Business.Records;

namespace Business.Interfaces;

public interface IUserService
{
    Task<UserRecord> LoginAsync(LoginRecord loginRecord);
}