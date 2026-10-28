using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Projcet_WPF.Model;
using System.IO;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Projcet_WPF.Data
{
    public class EventsDataProvider
    {
        public Event Dentist = new() { Id = 1,
            Title = "Dentist Appointment",
            StartDate = new Date(){ Year=2024, Month=05, Day=18},
            StartTime= new Time(){ Hour=12 , Minute=0, Second=0},
            DurationMinutes = 180 };

        public Event AdvancedProgramming1 = new () { Id = 2,
            Title = "Advanced Programming Class",
            StartDate = new Date() { Year = 2024, Month = 04, Day = 05 },
            StartTime = new Time() { Hour = 3, Minute = 0, Second = 0 },
            DurationMinutes = 90 };

        public Event AdvancedProgramming2 = new(){Id = 3,
           Title = "Advanced Programming Class2",
            StartDate = new Date() { Year = 2024, Month = 04, Day = 01 },
            StartTime = new Time() { Hour = 3, Minute = 0, Second = 0 },
            DurationMinutes = 90};
        public void SerializeAllEvents()
        {
           List<Event> events = new()
           {
               Dentist,
               AdvancedProgramming1,
               AdvancedProgramming2
           };
           var option = new JsonSerializerOptions();
           option.WriteIndented= true;
           string json = JsonSerializer.Serialize(events, option);
        
           File.WriteAllText("EventsDataProvider.json", json);
        }
        public List<Event> DeSerializeAllEvents()
        {
           string json = File.ReadAllText("EventsDataProvider.json");
           List<Event> events = JsonSerializer.Deserialize<List<Event>>(json);
           return events;

        }
        public void EditFileProvider(ObservableCollection<Event> NewEvents)
        {
            var option = new JsonSerializerOptions();
            option.WriteIndented = true;
            string json = JsonSerializer.Serialize(NewEvents, option);
            File.WriteAllText("EventsDataProvider.json", json);
        }
         
    }
}
