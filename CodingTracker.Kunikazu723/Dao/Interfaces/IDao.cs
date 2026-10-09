using CodingTracker.Kunikazu723.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Kunikazu723.Dao.Interfaces
{
    internal interface IDao<T> where T : class
    {
        List<T> GetAllItems();
        void InsertItem(T item);
        void UpdateItemById(int id);
        void DeleteItemById(int id);
        void InsertMany(List<T> items);
    }
}
