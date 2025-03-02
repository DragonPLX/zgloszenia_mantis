using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace zgloszenia_mantis
{
    internal class TimerToNextDay
    {

        CancellationTokenSource cancellationToken = new CancellationTokenSource();
        
        bool dataSaved = false;

        public TimerToNextDay() 
        {
            _ = CheckTime(cancellationToken.Token);
        }

        public async Task CheckTime(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    Debug.WriteLine("Sprawdzam czas");
                    TimeSpan timeSpan = DateTime.Today.AddDays(1) - DateTime.Now;

                    if (timeSpan <= TimeSpan.FromHours(1) && !dataSaved)
                    {
                        Description.SaveDataForAll();
                        dataSaved = true;
                    }
                    if (DateTime.Now.Hour == 0 && DateTime.Now.Minute < 5)
                    {
                        dataSaved = false;
                    }

                    await Task.Delay(TimeSpan.FromMinutes(5));
                }
                catch (TaskCanceledException) 
                {
                    break;
                }
                catch(Exception e)
                {
                    Debug.WriteLine(e.Message);
                }

             
            }
        }

        public void Stop()
        {
            cancellationToken.Cancel();
        }
    }
}
