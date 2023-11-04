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
    
    private static readonly Dictionary<Type, RecordConstructionInfo> RECORD_CONSTRUCTION_BY_TYPE = Assembly
        .GetAssembly(typeof(MySqlCommandExtensions))
        .GetTypes()
        .Where(type => type.GetProperties().Any(property => property.IsDefined(typeof(RecordParameter))))
        .ToDictionary(type => type, type => new RecordConstructionInfo(type));

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

            return ParseToList<T>(command, logger);
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

    private static List<T> ParseToList<T>(MySqlCommand command, ILogger logger) {
        try {
            RecordConstructionInfo constructionInfo = RECORD_CONSTRUCTION_BY_TYPE[typeof(T)];
            using MySqlDataReader reader = command.ExecuteReader();

            List<T> records = new();
            while (reader.Read()) {
                records.Add(constructionInfo.ConstructRecord<T>(reader));
            }

            return records;
        }
        catch (Exception e) {
            logger.LogError(e, "Unable to parse query to record list");
            throw;
        }
    }
}