using System;

namespace YordleYelper.database.attributes; 

public class RecordParameter : Attribute {
    public readonly string name;

    public RecordParameter(string name) {
        this.name = name;
    }
}