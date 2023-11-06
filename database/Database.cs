using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using YordleYelper.database.config;

namespace YordleYelper.database; 

public class Database {
    private readonly DatabaseConfig _config;
    private readonly ILogger _logger;

    public Database(DatabaseConfig config, ILogger logger) {
        _config = config;
        _logger = logger;
    }

    public T ExecuteQuery<T>(IQueryData<T> queryData) {
        return ExecuteListQuery(queryData).First();
    }
    
    public List<T> ExecuteListQuery<T>(IQueryData<T> queryData) {
        return ExecuteAnyQuery(queryData, QueryType.RECORD);
    }
    
    public T ExecuteBasicQuery<T>(IQueryData<T> queryData) {
        return ExecuteBasicListQuery(queryData).First();
    }
    
    public List<T> ExecuteBasicListQuery<T>(IQueryData<T> queryData) {
        return ExecuteAnyQuery(queryData, QueryType.VALUE);
    }
    
    public void ExecuteVoidQuery(IQueryData<VoidRecord> queryData) {
        ExecuteAnyQuery(queryData, QueryType.VOID);
    }

    private MySqlConnection GetConnection() {
        try {
            MySqlConnection connection = new($"Server={_config.Server};User ID={_config.UserId};Password={_config.Password};Database={_config.Database}");
            connection.Open();
            return connection;
        } catch (Exception e) {
            _logger.LogError(e, "Unable to establish connection with database");
            throw;
        }
    }
    
    private List<T> ExecuteAnyQuery<T>(IQueryData<T> queryData, QueryType queryType) {
        using MySqlConnection connection = GetConnection();
        using MySqlTransaction transaction = connection.BeginTransaction();
        try {
            _logger.LogInformation($"Making database call: {queryData}, {queryType}");
            List<T> result = DatabaseUtil.ExecuteQuery(connection, transaction, queryData, queryType, _logger);
            
            transaction.Commit();
            return result;
        } catch (MySqlException e) {
            _logger.LogError(e, "Unable to execute query! Rolling back!");
            transaction.Rollback();
            throw;
        }
    }
}