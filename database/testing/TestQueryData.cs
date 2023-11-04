using YordleYelper.database.attributes;

namespace YordleYelper.database.testing; 

public class TestQueryData : IQueryData {
    [QueryParameter("p_test_arg")] public readonly string pTestArg;
    
    [QueryParameter("p_test_arg_2")] public readonly int pTestArg2;

    public TestQueryData(string pTestArg, int pTestArg2) {
        this.pTestArg = pTestArg;
        this.pTestArg2 = pTestArg2;
    }

    public string GetStoredProcedureName => "test_proc_with_arg";
}