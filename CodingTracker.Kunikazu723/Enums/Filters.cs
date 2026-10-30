using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.Kunikazu723.Enums
{
    public static class Filters
    {
        public enum FilterType
        {
            Days,
            Weeks,
            Months,
            Years,
        }

        public enum FilterOrder
        {
            Ascending,
            Descending,
        }
    }
}
