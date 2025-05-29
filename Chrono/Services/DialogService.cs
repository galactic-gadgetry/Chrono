using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Chrono.Models;
using Chrono.Stores;

namespace Chrono.Services
{
    static class DialogService
    {
        /// <summary>
        /// Displays a deletion confirmation message box.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool PromptUserWithDeleteConfirmationMessage(
            string name)
        {
            string message = "Are you sure that you want to delete " +
                $" the log book ('{name}')? " +
                "This action can't be undone.";
            string caption = $"Delete '{name}'";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Exclamation;

            MessageBoxResult result = MessageBox.Show(
                message, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Displays a deletion confirmation message box.
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public static bool PromptUserWithDeleteConfirmationMessage(
            BookHeader header)
        {
            ArgumentNullException.ThrowIfNull(header, nameof(header));

            return PromptUserWithDeleteConfirmationMessage(header.Name);
        }

        /// <summary>
        /// Displays a deletion confirmation message box.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <returns>True if the user wishes to continue, false
        /// otherwise</returns>
        public static bool PromptUserWithDeleteConfirmationMessage(
            BookStore bookStore)
        {
            ArgumentNullException.ThrowIfNull(bookStore, nameof(bookStore));

            return PromptUserWithDeleteConfirmationMessage(bookStore.CurrentBook.Name);
        }


        public static void PromptUserWithErrorMessageWithOkButton(
            string caption, string message)
        {
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;

            MessageBox.Show(message, caption, button, icon);
        }

        /// <summary>
        /// Displays a save changes message box.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <returns></returns>
        public static MessageBoxResult PromptUserWithSaveChangesMessage(
            BookStore bookStore)
        {
            string message = "Do you want to save changes?";
            string caption = $"{bookStore.CurrentBook.Name}";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;

            return MessageBox.Show(
                message, caption, button, icon);
        }
    }
}
