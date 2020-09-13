namespace AiTech.TrackableEntity.Interfaces
{
    public interface ITransaction
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollBackTransaction();
    }
}