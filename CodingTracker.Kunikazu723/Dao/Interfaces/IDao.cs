namespace CodingTracker.Kunikazu723.Dao.Interfaces
{
    public interface IDao<T> where T : class
    {
        List<T> GetAllItems();
        T? GetItemById(int id);
        void InsertItem(T item);
        void UpdateItemById(T newItem);
        void DeleteItemById(T item);
        void InsertMany(List<T> items);
    }
}
