using System;

namespace YordleYelper.database.attributes; 

public class QueryParameter : Attribute {
    public readonly string name;

    public QueryParameter(string name) {
        this.name = name;
    }
}