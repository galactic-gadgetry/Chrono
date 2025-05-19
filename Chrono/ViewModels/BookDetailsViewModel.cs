using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Models;
using Chrono.Stores;

namespace Chrono.ViewModels
{
    class BookDetailsViewModel : DefaultViewModelBase
    {
        /// <summary>
        /// Used to manage the app's current book.
        /// </summary>
        private readonly BookStore _bookStore;


        /// <summary>
        /// The app's current book.
        /// </summary>
        public Book CurrentBook =>
            _bookStore.CurrentBook;



        public BookDetailsViewModel(BookStore bookStore)
        {
            _bookStore = bookStore;
        }
    }
}
