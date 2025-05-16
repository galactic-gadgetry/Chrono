using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Stores;

namespace Chrono.Utilities
{
    static class StoreFactory
    {

        public static BookStore CreateBookStore()
        {
            return new BookStore();
        }


        public static NavigationStore CreateNavigationStore()
        {
            return new NavigationStore();
        }
    }
}
