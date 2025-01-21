using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zgloszenia_mantis
{
    internal class TimerToNextDay
    {
        
        public TimerToNextDay() 
        {
            _ = CheckTime();
        }

        public async Task CheckTime()
        {
            while (true) 
            {
                Debug.WriteLine("Sprawdzam czas");
                TimeSpan timeSpan = DateTime.Today.AddDays(1) - DateTime.Now;

                if(timeSpan <= TimeSpan.FromHours(1))
                {
                    Description.SaveDataForAll();
                }

                await Task.Delay(TimeSpan.FromMinutes(5));
            }
        }
    }
}
