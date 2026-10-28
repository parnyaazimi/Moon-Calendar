using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projcet_WPF.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public Date StartDate { get; set; } = new();
        public Time StartTime { get; set; } = new();
        public int DurationMinutes { get; set; }
        public int CanvasTop { get; set; }

        public Thickness ButtonMargin { get; set; }
    }

    public class Date
    {
        public int Year {  get; set; }
        public int Month {  get; set; }
        public int Day {  get; set; }
    }

    public class Time
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Second { get; set; }
    }

    public class WeekEventsList
    {
        public int WeakNum {  get; set; }
        public ObservableCollection<Event> EventsList_W { get; set; } = new();

    }
    public class MonthEventsList
    {
        public int MonthNum { get; set; }
        public ObservableCollection<int> WeakNum { get; set; } = new();
        public ObservableCollection<WeekEventsList> EventsList_M { get; set; } = new();

    }
    public class YearEventsList
    {
        public int YearNum { get; set; }
        public ObservableCollection<int> MonthNum { get; set; } = new();
        public ObservableCollection<MonthEventsList> EventsList_Y { get; set; } = new();

    }
}
