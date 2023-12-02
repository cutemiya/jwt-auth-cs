using fu.Models.Db;

namespace fu.Repository.Interface;

public interface IAccountRepository
{
    Task<int> CreateAccount(DbAccount user);
    Task<bool> CheckAccount(string email);
    Task<DbAccount?> GetAccountData(string email, string password);
    Task<bool> CheckRefreshToken(string token);
    Task<DbAccount?> GetAccountDataByToken(string token);
    Task UpdateRefresh(string token, DateTime tokenTime);
}