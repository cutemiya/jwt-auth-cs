using System.Security.Claims;
using fu.Models.Db;

namespace fu.Services.Interface;

public interface IAccountService
{
    Task<int> CreateAccount(DbAccount account);
    Task<DbAccount?> GetAccountData(string email, string password);
    Task UpdateAccount(DbAccount user);
    Task DeleteAccount(int id);
}