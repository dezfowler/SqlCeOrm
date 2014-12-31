namespace SqlCeOrm.DataAccess
{
    public interface IRow
    {
        object GetValue(string fieldName);
    }
}