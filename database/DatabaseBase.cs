using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using YordleYelper.bot;

namespace YordleYelper.database; 

public class DatabaseBase {
    private readonly DiscordBotConfig _config = DiscordBot.Config;
    private readonly ILogger _logger = DiscordBot.Logger;

    public T ExecuteQuery<T>(IQueryData queryData) {
        return ExecuteListQuery<T>(queryData).First();
    }
    
    public List<T> ExecuteListQuery<T>(IQueryData queryData) {
        return ExecuteAnyQuery<T>(queryData);
    }

    private MySqlConnection GetConnection() {
        try {
            MySqlConnection connection = new($"Server={_config.MySqlServer};User ID={_config.MySqlUserId};Password={_config.MySqlPassword};Database={_config.MySqlDatabase}");
            connection.Open();
            return connection;
        } catch (Exception e) {
            _logger.LogError(e, "Unable to establish connection with database");
            throw;
        }
    }
    
    private List<T> ExecuteAnyQuery<T>(IQueryData queryData) {
        using MySqlConnection connection = GetConnection();
        using MySqlTransaction transaction = connection.BeginTransaction();
        try {
            List<T> result = DatabaseUtil.ExecuteQuery<T>(connection, transaction, queryData, _logger);
            
            transaction.Commit();
            return result;
        } catch (Exception e) {
            _logger.LogError(e, "Unable to execute query! Rolling back!");
            transaction.Rollback();
            throw;
        }
    }
}