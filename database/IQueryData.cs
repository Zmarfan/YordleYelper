namespace YordleYelper.database; 

public interface IQueryData<T> {
    string GetStoredProcedureName { get; }
}