using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MySqlConnector;
using YordleYelper.database.attributes;

namespace YordleYelper.database.sql_parser; 

public class RecordConstructionInfo {
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
            MethodInfo methodInfo = reader.GetType().GetMethod("GetFieldValue")!.MakeGenericMethod(property.Item1.PropertyType);
            object value = methodInfo.Invoke(reader, new object[] { reader.GetOrdinal(property.Item2) });
            property.Item1.SetValue(record, value);
        }

        return record;
    }
}