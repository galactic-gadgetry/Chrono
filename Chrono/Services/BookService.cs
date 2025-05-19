using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Chrono.Models;
using Chrono.Models.DTOs;
using Chrono.Stores;

namespace Chrono.Services
{
    static class BookService
    {
        /// <summary>
        /// Closes the book store's current book.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <returns>True selects the Yes or No button, or the
        /// current book has no unsaved changes, false otherwise</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool CloseCurrentBook(BookStore bookStore)
        {
            // If the book has unsaved changes, prompt the user
            // to save the book before closing.
            Book book = bookStore.CurrentBook;
            if (!book.IsBookVoid && book.HasUnsavedChanges)
            {
                MessageBoxResult result =
                    DialogService.PromptUserWithSaveChangesMessage(bookStore);
                if (result == MessageBoxResult.Cancel)
                {
                    return false;
                }
                else if (result == MessageBoxResult.Yes)
                {
                    SaveCurrentBook(bookStore);
                    return true;
                }
                else if (result == MessageBoxResult.No)
                {
                    return true;
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            // Set the book store's current book to a void-state
            // book.
            SetBookStoreCurrentBookToVoidState(bookStore);

            return true;
        }

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

        /// <summary>
        /// Deletes the book and header files of the book.
        /// </summary>
        /// <param name="book"></param>
        public static void DeleteBookFile(Book book)
        {
            ArgumentNullException.ThrowIfNull(book, nameof(book));

            // Delete the book and book header file.
            FileService.DeleteFile(book.SaveFilePath);
            FileService.DeleteFile(book.HeaderSaveFilePath);
        }

        /// <summary>
        /// Deletes the book and header files of the book store's
        /// current book.
        /// </summary>
        /// <param name="bookStore"></param>
        public static void DeleteCurrentBook(BookStore bookStore)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));

            DeleteBookFile(bookStore.CurrentBook);

            // Set the book store's current book to a void state
            // book.
            SetBookStoreCurrentBookToVoidState(bookStore);
        }


        public static BookHeader GetNewBookHeader(Book book)
        {
            return new BookHeader()
            {
                BookSaveFilePath = book.SaveFilePath,
                CreatedDateTime = book.CreatedDateTime,
                ID = book.ID,
                Name = book.Name,
                SaveFilePath = book.HeaderSaveFilePath,
                Status = book.StatusString,
            };
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
        /// Loads a <see cref="Book"/> instance from a JSON file and
        /// sets it as the book store's current book.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static BookStore LoadBookToBookStoreFromJson(
            BookStore bookStore, string filePath)
        {
            Book book = JsonService.LoadBookFromJsonFile(filePath);
            SetBookStoreCurrentBook(bookStore, book);

            return bookStore;
        }

        /// <summary>
        /// Saves the book store's current book to file.
        /// </summary>
        /// <param name="bookStore"></param>
        public static void SaveCurrentBook(BookStore bookStore)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));

            SaveCurrentBookToJson(bookStore);
        }

        
        /// <summary>
        /// Saves the b ook and its associated header to file.
        /// </summary>
        /// <param name="book"></param>
        /// <exception cref="InvalidOperationException">Thrown if
        /// the book is void state</exception>
        private static void SaveBookToJson(Book book)
        {
            // Check for null or invalid book state.
            ArgumentNullException.ThrowIfNull(book, nameof(book));
            if (book.IsBookVoid)
            {
                throw new InvalidOperationException("Book cannot be " +
                    "void state");
            }

            // Create a book header for saving.
            BookHeader header = GetNewBookHeader(book);

            // Set the Book instance's HasUnsavedChanges property false
            // to indicate that the book has no unsaved changes.
            book.HasUnsavedChanges = false;

            JsonService.SaveObject(header, header.SaveFilePath);
            JsonService.SaveObject(book, book.SaveFilePath);
        }

        /// <summary>
        /// Saves the book store's current book to a JSON file.
        /// </summary>
        /// <param name="bookStore"></param>
        private static void SaveCurrentBookToJson(BookStore bookStore)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));

            SaveBookToJson(bookStore.CurrentBook);
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

        /// <summary>
        /// Sets the book store's current book to a void state book.
        /// </summary>
        /// <param name="bookStore"></param>
        public static void SetBookStoreCurrentBookToVoidState(BookStore bookStore)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));

            Book voidBook = new() { IsBookVoid = true };
            SetBookStoreCurrentBook(bookStore, voidBook);
        }
    }
}
