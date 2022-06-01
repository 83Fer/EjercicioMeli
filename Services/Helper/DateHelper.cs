using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Helper
{
    public static class DateHelper
    {

        public static string CalculateFormattedDate(DateTime local, DateTime currentTime)
        {
            var difference = DateTime.Now - local;

            return currentTime.Add(difference).ToLongDateString();
        }
    }
}
