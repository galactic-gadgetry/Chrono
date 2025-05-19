using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Chrono.Stores;

namespace Chrono.Services
{
    static class DialogService
    {
        /// <summary>
        /// Displays a deletion confirmation message box.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <returns>True if the user wishes to continue, false
        /// otherwise</returns>
        public static bool PromptUserWithDeleteConfirmationMessage(
            BookStore bookStore)
        {
            string message = "Are you sure that you want to delete " +
                $"the log book ('{bookStore.CurrentBook.Name}')? " +
                $"This action can't be undone.";
            string caption = $"{bookStore.CurrentBook.Name}";
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
        /// Displays a save changes message box.
        /// </summary>
        /// <param name="bookStore"></param>
        /// <returns></returns>
        public static MessageBoxResult PromptUserWithSaveChangesMessage(
            BookStore bookStore)
        {
            string message = "Do you want to save changes?";
            string caption = $"{bookStore.CurrentBook}";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Question;

            return MessageBox.Show(
                message, caption, button, icon);
        }
    }
}
