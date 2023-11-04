using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using YordleYelper.database.attributes;

namespace YordleYelper.database.sql_parser; 

public static class MySqlCommandExtensions {
    private static readonly Dictionary<Type, RecordConstructionInfo> RECORD_CONSTRUCTION_BY_TYPE = Assembly
        .GetAssembly(typeof(MySqlCommandExtensions))
        .GetTypes()
        .Where(type => type.GetProperties().Any(property => property.IsDefined(typeof(RecordParameter))))
        .ToDictionary(type => type, type => new RecordConstructionInfo(type));

    public static List<T> ParseToList<T>(this MySqlCommand command, ILogger logger) {
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