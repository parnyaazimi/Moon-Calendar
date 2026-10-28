using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Projcet_WPF.Lists
{
    public class HelperMethos
    {
        public int GetWeekNumber(DateTime date)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            Calendar calendar = culture.Calendar;
            return calendar.GetWeekOfYear(
                date,
                culture.DateTimeFormat.CalendarWeekRule,
                culture.DateTimeFormat.FirstDayOfWeek
            );
        }
    }
}
