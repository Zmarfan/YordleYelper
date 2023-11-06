using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MySqlConnector;
using YordleYelper.bot.data_fetcher.league_api.data;
using YordleYelper.database.attributes;

namespace YordleYelper.database; 

public static class QueryDataConstructor {
    private static readonly Dictionary<Type, Func<MySqlDataReader, int, object>> CUSTOM_PARSING = new() {
        { typeof(Puuid), (reader, ordinal) => new Puuid((string)GetValueGetterFromType(reader, typeof(string)).Invoke(reader, ordinal)) }
    };

    private static readonly Dictionary<Type, List<(PropertyInfo, string)>> RECORD_PROPERTIES = Assembly
        .GetAssembly(typeof(QueryDataConstructor))
        .GetTypes()
        .Where(type => type.GetProperties().Any(property => property.IsDefined(typeof(RecordParameter))))
        .ToDictionary(type => type, type => {
            return type
                .GetProperties()
                .Where(property => property.IsDefined(typeof(RecordParameter)))
                .Select(property => (property, property.GetCustomAttribute<RecordParameter>().name))
                .ToList();
        });
    
    private static readonly Dictionary<Type, Func<MySqlDataReader, int, object>> READER_GET_FIELD_VALUE_BY_TYPE = new();

    public static T ConstructRecord<T>(MySqlDataReader reader) {
        T record = Activator.CreateInstance<T>();

        foreach ((PropertyInfo, string) property in RECORD_PROPERTIES[typeof(T)]) {
            int ordinal = reader.GetOrdinal(property.Item2);
            object value = CUSTOM_PARSING.TryGetValue(property.Item1.PropertyType, out Func<MySqlDataReader, int, object> customParser) 
                ? customParser.Invoke(reader, ordinal) 
                : GetValueGetterFromType(reader, property.Item1.PropertyType).Invoke(reader, ordinal);
            property.Item1.SetValue(record, value);
        }

        return record;
    }

    public static T ConstructValue<T>(MySqlDataReader reader) {
        return (T)(CUSTOM_PARSING.TryGetValue(typeof(T), out Func<MySqlDataReader, int, object> customParser) 
            ? customParser.Invoke(reader, 0) 
            : GetValueGetterFromType(reader, typeof(T)).Invoke(reader, 0));
    }

    private static Func<MySqlDataReader, int, object> GetValueGetterFromType(MySqlDataReader reader, Type type) {
        if (READER_GET_FIELD_VALUE_BY_TYPE.TryGetValue(type, out Func<MySqlDataReader, int, object> valueGetter)) {
            return valueGetter;
        }
        MethodInfo methodInfo = reader.GetType().GetMethod("GetFieldValue")!.MakeGenericMethod(type);
        valueGetter = (r, index) => methodInfo.Invoke(r, new object[] { index });
        READER_GET_FIELD_VALUE_BY_TYPE.Add(type, valueGetter);

        return valueGetter;
    }
}