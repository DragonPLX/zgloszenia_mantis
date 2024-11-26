using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace zgloszenia_mantis
{
    public class Stoper 
    {
        private int hour = 0, minute = 0, second = 0;
        private readonly static List<Stoper> stopers = new List<Stoper>();

        private readonly StoperPanel stoperPanel;
        private System.Timers.Timer timer;

        public int Hour { get => hour; private set => hour = value; }
        public int Minute { get => minute; private set => minute = value; }
        public int Second { get => second; private set => second = value; }

        public Stoper(StoperPanel _stoperPanel)
        {
            stoperPanel= _stoperPanel;
            stopers.Add(this);
        }

        
        public void ResetStoper()
        {
            hour = 0;
            minute = 0;
            second = 0;

            if (timer != null && timer.Enabled)
            {
                timer.Stop();
            }
            timer = null;
            stoperPanel.Start.Text = "Start";
            UpdateShowTime();
        }

        public void StartStoper()
        {
            if (timer == null || !timer.Enabled)
            {
                timer = new System.Timers.Timer(1000);
                timer.Elapsed += (s, e) =>
                {
                    second++;
                    if (second == 60)
                    {
                        second = 0;
                        minute++;
                        if (minute == 60)
                        {
                            minute = 0;
                            hour++;
                        }
                    }
                    UpdateShowTime();
                };
                timer.Start();
                stoperPanel.Start.Text = "Pause";
            }
            else
            {
                timer.Stop();
                stoperPanel.Start.Text = "Start";
            }
        }

        public void UpdateShowTime()
        {
            stoperPanel.ShowTime.Invoke(new Action(() =>
            {
                stoperPanel.ShowTime.Text = $"{hour:D2}:{minute:D2}:{second:D2}";
            }));
        }

        public string GetTimeToString()
        {
            int _hour , _minute;
            if (second >= 30)
            {
                if (minute > 59)
                {
                    _hour = hour + 1;
                    _minute = minute + 1;
                }
                else {
                    _hour = hour;
                    _minute = minute+1;
                }

            }
            else
            {
                _hour = hour; 
                _minute = minute;
            }
            
            return $"{_hour:D2}:{_minute:D2}";

        }

        public void StopStoper()
        {
            if(timer != null)
            {
                timer.Stop();
                stoperPanel.Start.Text = "Start";
            }
        }

        public void StopAllOtherStopers()
        {
            foreach(Stoper stoper in stopers)
            {
                if(stoper != this)
                {
                    stoper.StopStoper();
                }
            }
        }

       

    }
}