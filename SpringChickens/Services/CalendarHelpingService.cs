using Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpringChickens.Services
{
    public class CalendarHelpingService : ICalendarHelpingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public bool ShowOutdatedEvents
        {
            get => "true" == _httpContextAccessor.HttpContext.Session.GetString("ShowOutdatedEvents");
            set
            {
                _httpContextAccessor.HttpContext.Session.SetString("ShowOutdatedEvents", value ? "true" : "false");
            }
        }

        public int Year
        {
            get
            {
                int output;
                if (!int.TryParse(_httpContextAccessor.HttpContext.Session.GetString("CalendarYear"), out output))
                {
                    output = 2020;
                }
                return output;
            }

            private set
            {
                _httpContextAccessor.HttpContext.Session.SetString("CalendarYear", value.ToString());
            }
        }

        public int Month
        {
            get
            {
                int output;
                if (!int.TryParse(_httpContextAccessor.HttpContext.Session.GetString("CalendarMonth"), out output))
                {
                    output = 4;
                }
                return output;
            }

            private set
            {
                _httpContextAccessor.HttpContext.Session.SetString("CalendarMonth", value.ToString());
            }
        }

        public CalendarHelpingService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            ResetToToday();
        }

        public void ModifyMonth(int change)
        {
            Month += change;

            if (Month <= 0)
            {
                Month = 12;
                Year--;
            }

            if (Month > 12)
            {
                Month = 1;
                Year++;
            }

            if (Year < 1)
            {
                Year = 1;
            }
            
            if (Year > 9999)
            {
                Year = 9999;
            }
        }

        public void ResetToToday()
        {
            var today = DateTime.Now;

            Year = today.Year;
            Month = today.Month;
        }
    }
}
