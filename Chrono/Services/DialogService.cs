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
