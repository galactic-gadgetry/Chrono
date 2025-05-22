using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Services;
using Chrono.Stores;

namespace Chrono.Utilities
{
    static class StoreFactory
    {

        public static BookStore GetNewBookStore()
        {
            return new BookStore();
        }


        public static NavigationStore GetNewNavigationStore()
        {
            return new NavigationStore();
        }


        public static BookStore LoadBookStoreFromFile(string filePath)
        {
            BookStore bookStore = GetNewBookStore();
            BookService.LoadBookToBookStoreFromJson(bookStore, filePath);

            return bookStore;
        }
    }
}
