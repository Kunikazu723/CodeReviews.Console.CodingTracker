using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Kunikazu723.Services
{
    public interface IService
    {
        void ViewAllItems();
        void AddItem();
        void UpdateItem();
        void DeleteItem();
    }
}
