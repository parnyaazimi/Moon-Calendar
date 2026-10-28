using Projcet_WPF.Data;
using Projcet_WPF.Lists;
using Projcet_WPF.Model;
using Projcet_WPF.ViewModel;
using System;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Projcet_WPF;
public partial class MainWindow : Window
{
    EventsViewModel _viewModel = new();
    HelperMethos Method = new();
    public MainWindow()
    {
        InitializeComponent();
        DataContext = _viewModel;
        Loaded += HandelStartUp_Events;

    }
    public void HandelStartUp_Events(object sender, RoutedEventArgs e)
    {
        var DataProvider = new EventsDataProvider();
        DataProvider.SerializeAllEvents();
        var PrpviderEvents = DataProvider.DeSerializeAllEvents();

        foreach (Event eventitem in PrpviderEvents)
        {
            _viewModel.NewEvent= eventitem;
            Add(sender, e);
        }
    }
    private void ShowEvents(object sender, RoutedEventArgs e)
    {
        _viewModel.Sunday.Clear();
        _viewModel.Monday.Clear();
        _viewModel.Tuesday.Clear();
        _viewModel.Wednesday.Clear(); 
        _viewModel.Thursday.Clear();  
        _viewModel.Friday.Clear();
        _viewModel.Saturday.Clear();
        int WeekNumber = Method.GetWeekNumber(_viewModel.SelectedDate);
        foreach (var year in _viewModel.YearEventsList)
        {
            if (year.YearNum == _viewModel.SelectedDate.Year)
            {
                foreach (var month in year.EventsList_Y)
                {
                    if (month.MonthNum == _viewModel.SelectedDate.Month)
                    {
                        foreach (var week in month.EventsList_M)
                        {
                            if (week.WeakNum == WeekNumber)
                            {
                                foreach (var eventItem in week.EventsList_W)
                                {
                                    if (eventItem.StartTime.Hour == 0)
                                    {
                                        eventItem.CanvasTop = (23*60) + eventItem.StartTime.Minute;
                                    }
                                    else
                                    {
                                        eventItem.CanvasTop = ((eventItem.StartTime.Hour-1)*60) + eventItem.StartTime.Minute;
                                    }
                                    DateTime Date_Time = new DateTime(
                                                        eventItem.StartDate.Year,
                                                        eventItem.StartDate.Month,
                                                        eventItem.StartDate.Day,
                                                        eventItem.StartTime.Hour,
                                                        eventItem.StartTime.Minute,
                                                        eventItem.StartTime.Second
                                    );
                                    int DayNumber = (int)Date_Time.DayOfWeek;
                                    if (DayNumber == 0)
                                    {
                                        _viewModel.Sunday.Add(eventItem);
                                    }
                                    else if (DayNumber == 1)
                                    {
                                        _viewModel.Monday.Add(eventItem);
                                    }
                                    else if (DayNumber == 2)
                                    {
                                        _viewModel.Tuesday.Add(eventItem);
                                    }
                                    else if (DayNumber == 3)
                                    {
                                        _viewModel.Wednesday.Add(eventItem);
                                    }
                                    else if (DayNumber == 4)
                                    {
                                        _viewModel.Thursday.Add(eventItem);
                                    }
                                    else if (DayNumber == 5)
                                    {
                                        _viewModel.Friday.Add(eventItem);
                                    }
                                    else
                                    {
                                        _viewModel.Saturday.Add(eventItem);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    public void Add(object sender, RoutedEventArgs e)
    {
        _viewModel.NewEvent.Id=_viewModel.Events.Count+1;
        if (_viewModel.NewEvent.StartTime.Hour == 0)
        {
            _viewModel.NewEvent.CanvasTop =(23*60)+ _viewModel.NewEvent.StartTime.Minute;
        }
        else
        {
            _viewModel.NewEvent.CanvasTop =((_viewModel.NewEvent.StartTime.Hour-1)*60)+ _viewModel.NewEvent.StartTime.Minute;
        }
        DateTime Date_Time = new DateTime(
            _viewModel.NewEvent.StartDate.Year,
            _viewModel.NewEvent.StartDate.Month,
            _viewModel.NewEvent.StartDate.Day,
            _viewModel.NewEvent.StartTime.Hour,
            _viewModel.NewEvent.StartTime.Minute,
            _viewModel.NewEvent.StartTime.Second
        );

        int NewWeekNumber = Method.GetWeekNumber(Date_Time);

        if (_viewModel.NewEvent != null)
        {
            // اگر سال وجود نداشت
            if (!_viewModel.YearsNum.Contains(_viewModel.NewEvent.StartDate.Year))
            {
                //Year
                YearEventsList NewYear = new();
                NewYear.YearNum = _viewModel.NewEvent.StartDate.Year;
                //Month
                MonthEventsList NewMonth = new();
                NewMonth.MonthNum = _viewModel.NewEvent.StartDate.Month;
                //Week
                WeekEventsList NewWeek = new();
                NewWeek.WeakNum = NewWeekNumber;

                NewWeek.EventsList_W.Add(_viewModel.NewEvent);
                NewMonth.EventsList_M.Add(NewWeek);
                NewMonth.WeakNum.Add(NewWeekNumber);
                NewYear.EventsList_Y.Add(NewMonth);
                NewYear.MonthNum.Add(_viewModel.NewEvent.StartDate.Month);

                _viewModel.YearsNum.Add(_viewModel.NewEvent.StartDate.Year);
                _viewModel.YearEventsList.Add(NewYear);
                MessageBox.Show($"Event: {_viewModel.NewEvent.Title}.\t {Date_Time} => Duration: {_viewModel.NewEvent.DurationMinutes}");
            }
            else // اگر سال وجود داشت
            {
                foreach (var year in _viewModel.YearEventsList)
                {
                    if (year.YearNum == _viewModel.NewEvent.StartDate.Year)
                    {
                        // اگر ماه وجود نداشت
                        if (!year.MonthNum.Contains(_viewModel.NewEvent.StartDate.Month))
                        {
                            //Month
                            MonthEventsList NewMonth = new();
                            NewMonth.MonthNum = _viewModel.NewEvent.StartDate.Month;
                            //Week
                            WeekEventsList NewWeek = new();
                            NewWeek.WeakNum = NewWeekNumber;

                            NewWeek.EventsList_W.Add(_viewModel.NewEvent);
                            NewMonth.EventsList_M.Add(NewWeek);
                            NewMonth.WeakNum.Add(NewWeekNumber);
                            year.EventsList_Y.Add(NewMonth);
                            year.MonthNum.Add(_viewModel.NewEvent.StartDate.Month);
                            MessageBox.Show($"Event: {_viewModel.NewEvent.Title}.\t {Date_Time} => Duration: {_viewModel.NewEvent.DurationMinutes}");
                        }
                        else // اگر ماه وجود داشت
                        {
                            foreach (var month in year.EventsList_Y)
                            {
                                if (month.MonthNum == _viewModel.NewEvent.StartDate.Month)
                                {
                                    // اگر هفته وجود نداشت
                                    if (!month.WeakNum.Contains(NewWeekNumber))
                                    {
                                        //Week
                                        WeekEventsList NewWeek = new();
                                        NewWeek.WeakNum = NewWeekNumber;

                                        NewWeek.EventsList_W.Add(_viewModel.NewEvent);
                                        month.EventsList_M.Add(NewWeek);
                                        month.WeakNum.Add(NewWeekNumber);
                                        MessageBox.Show($"Event: {_viewModel.NewEvent.Title}.\t {Date_Time} => Duration: {_viewModel.NewEvent.DurationMinutes}");
                                    }
                                    else // اگر هفته وجود داشت
                                    {
                                        foreach (var week in month.EventsList_M)
                                        {
                                            if (week.WeakNum == NewWeekNumber)
                                            {
                                                week.EventsList_W.Add(_viewModel.NewEvent);
                                                MessageBox.Show($"Event:  {_viewModel.NewEvent.Title}.\t {Date_Time} => Duration: {_viewModel.NewEvent.DurationMinutes}");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            _viewModel.Events.Add(_viewModel.NewEvent);
            var DataProvider = new EventsDataProvider();
            DataProvider.EditFileProvider(_viewModel.Events);
            _viewModel.NewEvent = new Event();
        }
    }
    private void Delete(object sender, RoutedEventArgs e)
    {
        if (_viewModel.SelectedItem != null)
        {
            DateTime Date_Time = new DateTime(
                _viewModel.SelectedItem.StartDate.Year,
                _viewModel.SelectedItem.StartDate.Month,
                _viewModel.SelectedItem.StartDate.Day,
                _viewModel.SelectedItem.StartTime.Hour,
                _viewModel.SelectedItem.StartTime.Minute,
                _viewModel.SelectedItem.StartTime.Second
            );
            int WeekNumber = Method.GetWeekNumber(Date_Time);
            foreach (var year in _viewModel.YearEventsList)
            {
                foreach (var month in year.EventsList_Y)
                {
                    if (month.MonthNum == Date_Time.Month)
                    {
                        foreach (var week in month.EventsList_M)
                        {
                            if (week.WeakNum == WeekNumber)
                            {
                                week.EventsList_W.Remove(_viewModel.SelectedItem);
                            }
                        }
                    }
                }
            }
            int DayNumber=(int)Date_Time.DayOfWeek;
            _viewModel.Events.Remove(_viewModel.SelectedItem);
            if (DayNumber == 0)
            {
                _viewModel.Sunday.Remove(_viewModel.SelectedItem);
            }
            else if (DayNumber == 1)
            {
                _viewModel.Monday.Remove(_viewModel.SelectedItem);
            }
            else if (DayNumber == 2)
            {
                _viewModel.Tuesday.Remove(_viewModel.SelectedItem);
            }
            else if (DayNumber == 3)
            {
                _viewModel.Wednesday.Remove(_viewModel.SelectedItem);
            }
            else if (DayNumber == 4)
            {
                _viewModel.Thursday.Remove(_viewModel.SelectedItem);
            }
            else if (DayNumber == 5)
            {
                _viewModel.Friday.Remove(_viewModel.SelectedItem);
            }
            else
            {
                _viewModel.Sunday.Remove(_viewModel.SelectedItem);
            }
        }
        var DataProvider = new EventsDataProvider();
        DataProvider.EditFileProvider(_viewModel.Events);
    }

    private void Edit(object sender, RoutedEventArgs e)
    {
        if (_viewModel.SelectedItem != null)
        {
            Event Orginal= _viewModel.SelectedItem;
            Delete(sender, e);
            _viewModel.NewEvent = Orginal;
            Add(sender, e);
            _viewModel.NewEvent = new Event();
        }
        var dataProvider = new EventsDataProvider();
        dataProvider.EditFileProvider(_viewModel.Events);
    }

}