using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MySqlConnector;
using YordleYelper.database.attributes;

namespace YordleYelper.database.sql_parser; 

public class RecordConstructionInfo {
    private static readonly Dictionary<Type, Func<MySqlDataReader, string, object>> READER_GET_FIELD_VALUE_BY_TYPE = new();

    private readonly List<(PropertyInfo, string)> _properties;
    
    public RecordConstructionInfo(Type type) {
        _properties = type
            .GetProperties()
            .Where(property => property.IsDefined(typeof(RecordParameter)))
            .Select(property => (property, property.GetCustomAttribute<RecordParameter>().name))
            .ToList();
    }

    public T ConstructRecord<T>(MySqlDataReader reader) {
        T record = Activator.CreateInstance<T>();
        
        foreach ((PropertyInfo, string) property in _properties) {
            property.Item1.SetValue(record, GetValueGetter(reader, property).Invoke(reader, property.Item2));
        }

        return record;
    }

    private Func<MySqlDataReader, string, object> GetValueGetter(MySqlDataReader reader, (PropertyInfo, string) property) {
        if (READER_GET_FIELD_VALUE_BY_TYPE.TryGetValue(property.Item1.PropertyType, out Func<MySqlDataReader, string, object> valueGetter)) {
            return valueGetter;
        }
        MethodInfo methodInfo = reader.GetType().GetMethod("GetFieldValue")!.MakeGenericMethod(property.Item1.PropertyType);
        valueGetter = (r, name) => methodInfo.Invoke(reader, new object[] { r.GetOrdinal(name) });
        READER_GET_FIELD_VALUE_BY_TYPE.Add(property.Item1.PropertyType, valueGetter);

        return valueGetter;
    }
}