using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using YordleYelper.database.attributes;
using YordleYelper.database.sql_parser;

namespace YordleYelper.database; 

public static class DatabaseUtil {
    private static readonly Dictionary<Type, List<QueryParameterField>> QUERY_DATA_FIELDS_BY_TYPE = Assembly.GetAssembly(typeof(DatabaseUtil))
        .GetTypes()
        .Where(type => typeof(IQueryData).IsAssignableFrom(type))
        .ToDictionary(type => type, type => {
            return type
                .GetFields()
                .Where(field => field.IsDefined(typeof(QueryParameter)))
                .Select(field => new QueryParameterField(field.GetCustomAttribute<QueryParameter>().name, field))
                .ToList();
        });

    public static List<T> ExecuteQuery<T>(
        MySqlConnection connection,
        MySqlTransaction transaction,
        IQueryData queryData,
        ILogger logger
    ) {
        try {
            MySqlCommand command = new(queryData.GetStoredProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;
            command.Parameters.AddRange(GetQueryDataParameters(queryData));
            command.ExecuteNonQuery();

            return command.ParseToList<T>(logger);
        } catch (Exception e) {
            logger.LogError(e, $"Unable to execute query for query data: {queryData}");
            throw;
        }
    }

    private static MySqlParameter[] GetQueryDataParameters(IQueryData queryData) {
        return QUERY_DATA_FIELDS_BY_TYPE[queryData.GetType()]
            .Select(info => new MySqlParameter(info.name, info.fieldInfo.GetValue(queryData)))
            .ToArray();
    }
}