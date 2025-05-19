using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chrono.Stores;
using Chrono.Models;

namespace Chrono.Services
{
    static class SettingsService
    {

        public static string? GetLastOpenBookFilePath()
        {
            return Chrono.Properties.Settings.Default.LastOpenBookFilePath;
        }


        public static void SetAppSettings(BookStore bookStore)
        {
            Properties.Settings settings = Properties.Settings.Default;
            Book currentBook = bookStore.CurrentBook;

            settings.LastOpenBookFilePath =
                currentBook.IsBookVoid ? string.Empty : currentBook.SaveFilePath;

            SaveSettings();
        }


        
        private static void SaveSettings()
        {
            Properties.Settings.Default.Save();
        }
    }
}
