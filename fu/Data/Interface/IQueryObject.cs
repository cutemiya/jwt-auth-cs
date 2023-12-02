namespace fu.Data.Interface;

public interface IQueryObject
{
    string Sql { get; }

    object Params { get; }
}