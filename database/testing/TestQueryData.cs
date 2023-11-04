using YordleYelper.database.attributes;

namespace YordleYelper.database.testing; 

public class TestQueryData : IQueryData {
    [QueryParameter("p_name")] public readonly string name;
    
    [QueryParameter("p_age")] public readonly int age;

    public TestQueryData(string name, int age) {
        this.name = name;
        this.age = age;
    }

    public string GetStoredProcedureName => "test_proc_with_arg";
}