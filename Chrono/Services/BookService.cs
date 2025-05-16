using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Models;
using Chrono.Models.DTOs;
using Chrono.Stores;

namespace Chrono.Services
{
    static class BookService
    {
        /// <summary>
        /// Creates a new <see cref="Book"/> instance sets it as the
        /// book store's current book.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="dto">Book data transfer object used to
        /// initialize the book</param>
        /// <returns></returns>
        public static Book CreateNewCurrentBook(BookStore bookStore,
            BookDTO dto)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            // Create a new book from the DTO.
            Book book = GetNewBook(dto);

            // Set the newly created book as the book store's
            // current book.
            SetBookStoreCurrentBook(bookStore, book);

            return book;
        }

        
        public static Book GetNewBook(BookDTO dto)
        {
            Book book = new()
            {
                Name = dto.Name,
            };

            return book;
        }


        /// <summary>
        /// Sets the book store's current book.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="book"></param>
        private static void SetBookStoreCurrentBook(BookStore bookStore, Book book)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));
            ArgumentNullException.ThrowIfNull(book, nameof(book));

            bookStore.CurrentBook = book;
        }
    }
}
