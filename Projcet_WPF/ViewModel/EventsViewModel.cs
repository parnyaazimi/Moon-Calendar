using Projcet_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Projcet_WPF.ViewModel
{
    public class EventsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Event> Events { get; set; } = new();
        private Event? selectedItem;
        private Event newEvent = new();
        private DateTime selectedDate=DateTime.UtcNow;
        public HashSet<int> YearsNum { get; set; } = new();
        public ObservableCollection<YearEventsList> YearEventsList { get; set; } = new();
        //----------------------------------------------------------------
        public ObservableCollection<Event> Sunday { get; set; } = new();
        public ObservableCollection<Event> Monday { get; set; } = new();
        public ObservableCollection<Event> Tuesday { get; set; } = new();
        public ObservableCollection<Event> Wednesday { get; set; } = new();
        public ObservableCollection<Event> Thursday { get; set; } = new();
        public ObservableCollection<Event> Friday { get; set; } = new();
        public ObservableCollection<Event> Saturday { get; set; } = new();
        //----------------------------------------------------------------

        public DateTime SelectedDate { get => selectedDate; set { selectedDate = value;OnPropertyChanged(); } }
        public Event? SelectedItem { get => selectedItem; set { selectedItem = value; OnPropertyChanged(); } }
        public Event NewEvent { get => newEvent; set { newEvent = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
