using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chrono.Models
{
    public enum BookStatus
    {
        Active,
        Archived,
    }

    class Book : INotifyPropertyChanged
    {
        // Backing Fields
        private bool hasUnsavedChanges = false;
        private string headerSaveFilePath = string.Empty;
        private string name = string.Empty;



        public string CreatedDateString =>
            CreatedDateTime.ToString("dd MMMM, yyyy");


        public DateTime CreatedDateTime { get; init; }



        public bool HasUnsavedChanges
        {
            get => hasUnsavedChanges;
            set
            {
                hasUnsavedChanges = value;
                OnPropertyChanged(nameof(HasUnsavedChanges));
            }
        }


        public string HeaderSaveFilePath
        {
            get => headerSaveFilePath;
            set
            {
                headerSaveFilePath = value;
                HasUnsavedChanges = true;
            }
        }


        public Guid ID { get; init; }


        public bool IsBookVoid = false;


        public string Name { get; set; } = string.Empty;


        public string SaveFilePath { get; init; } = string.Empty;


        public BookStatus Status { get; set; }


        public string StatusString => Status.ToString();



        public event PropertyChangedEventHandler? PropertyChanged;



        public Book()
        {
            CreatedDateTime = DateTime.Now;
            ID = Guid.NewGuid();
            Status = BookStatus.Active;
        }



        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
