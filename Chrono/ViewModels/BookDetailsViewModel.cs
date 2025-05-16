using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Models;
using Chrono.Stores;

namespace Chrono.ViewModels
{
    class BookDetailsViewModel : ViewModelBase
    {

        private readonly BookStore _bookStore;



        public Book CurrentBook =>
            _bookStore.CurrentBook;



        public BookDetailsViewModel(BookStore bookStore)
        {
            _bookStore = bookStore;
        }
    }
}
