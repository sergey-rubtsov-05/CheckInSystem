namespace DataAccess
{
    public interface ITransaction
    {
        void Commit();
        void Rollback();
    }
}