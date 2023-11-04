using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using YordleYelper.database.attributes;

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
        QueryType queryType,
        ILogger logger
    ) {
        try {
            MySqlCommand command = new(queryData.GetStoredProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Transaction = transaction;
            command.Parameters.AddRange(GetQueryDataParameters(queryData));
            Console.WriteLine(command.ExecuteNonQuery());

            return queryType switch {
                QueryType.VOID => new List<T>(),
                QueryType.VALUE => ParseToBasicList<T>(command, logger),
                QueryType.RECORD => ParseToList<T>(command, logger),
                _ => throw new ArgumentException($"There is no query handling for {queryType}")
            };
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
    
    private static List<T> ParseToBasicList<T>(MySqlCommand command, ILogger logger) {
        try {
            return ConstructDataList(command, QueryDataConstructor.ConstructValue<T>);
        }
        catch (Exception e) {
            logger.LogError(e, "Unable to parse query to value list");
            throw;
        }
    }

    private static List<T> ParseToList<T>(MySqlCommand command, ILogger logger) {
        try {
            return ConstructDataList(command, QueryDataConstructor.ConstructRecord<T>);
        }
        catch (Exception e) {
            logger.LogError(e, "Unable to parse query to record list");
            throw;
        }
    }
    
    private static List<T> ConstructDataList<T>(MySqlCommand command, Func<MySqlDataReader, T> valueConstructor) {
        using MySqlDataReader reader = command.ExecuteReader();

        List<T> records = new();
        while (reader.Read()) {
            records.Add(valueConstructor.Invoke(reader));
        }

        return records;
    }
}