﻿using Common;
using Dapper;
using fu.Data;
using fu.Models.Db;
using fu.Repository.Interface;

namespace fu.Repository;

public class AccountRepository : IAccountRepository
{
    private readonly DapperContext _dapperContext;

    public AccountRepository(DapperContext dapperContext)
    {
        this._dapperContext = dapperContext;
    }

    public async Task<bool> CheckAccount(string email)
    {
            return await _dapperContext.Connection.QueryFirstOrDefaultAsync<DbAccount>(
                @$"select id from accounts where email = '{email}'") != null;
    }

    [Obsolete("Obsolete")]
    public async Task<int> CreateAccount(DbAccount user)
    {
            var id = await _dapperContext.Connection.ExecuteAsync(
                @$"insert into accounts (email, password, username, role, refresh_token, refresh_token_expired_time)
                values ('{user.Email}',
                        '{Hash.GetHash(user.Password)}', 
                        '{user.Username}', 
                        '{user.Role}',
                        '{user.RefreshToken}', 
                        '{user.RefreshTokenExpiredTime}')
                         returning id");

            return id;
    }

    [Obsolete("Obsolete")]
    public async Task<DbAccount?> GetAccountData(string email, string password)
    {
            return await _dapperContext.Connection.QueryFirstOrDefaultAsync<DbAccount>(
                @$"select id, role, refresh_token as RefreshToken, username 
                from accounts where email = '{email}' and 
                password='{Hash.GetHash(password)}'");
    }

    public async Task<bool> CheckRefreshToken(string token)
    {
        return await _dapperContext.Connection.QueryFirstOrDefaultAsync<string>(
            $@"select refresh_token from accounts where refresh_token = '{token}'") == null;
    }

    public async Task<DbAccount?> GetAccountDataByToken(string token)
    {
        return await _dapperContext.Connection.QueryFirstOrDefaultAsync<DbAccount>(
            $@"select id, email, username, role from accounts where refresh_token = '{token}'");
    }

    public async Task UpdateRefresh(string token, DateTime tokenTime, int id)
    {
        await _dapperContext.Connection.ExecuteAsync($@"update accounts set 
                    refresh_token = '{token}', refresh_token_expired_time='{tokenTime}' where id={id}");
    }

    public async Task UpdateAccount(DbAccount user)
    {
        await _dapperContext.Connection.ExecuteAsync(
            $@"update accounts set email='{user.Email}', username='{user.Username}' where id={user.Id}");
    }

    public async Task DeleteAccount(int id)
    {
        await _dapperContext.Connection.ExecuteAsync($@"delete from accounts where id = {id}");
    }
}