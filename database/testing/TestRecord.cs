using YordleYelper.database.attributes;

namespace YordleYelper.database.testing; 

public class TestRecord {
    [RecordParameter("id")]
    public int Id { get; set; }
    
    [RecordParameter("name")]
    public string Name { get; set; }
    
    [RecordParameter("age")]
    public int Age { get; set; }
    
    [RecordParameter("test_arg")]
    public string TestArg { get; set; }
    
    [RecordParameter("test_arg_2")]
    public int TestArg2 { get; set; }
}