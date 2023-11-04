using System.Reflection;

namespace YordleYelper.database; 

public class QueryParameterField {
    public readonly string name;
    public readonly FieldInfo fieldInfo;

    public QueryParameterField(string name, FieldInfo fieldInfo) {
        this.name = "@" + name;
        this.fieldInfo = fieldInfo;
    }
}