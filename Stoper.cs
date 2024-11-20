using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace zgloszenia_mantis
{
    public class Stoper : Panel
    {
        private int hour = 0, minute = 0, second = 0;
        private static List<Stoper> stopers = new List<Stoper>();

        private Button start, stop, reset;
        private Label showTime;
        private System.Timers.Timer timer;

        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 2,
            ColumnCount = 1

        };

        FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
        {
            WrapContents = false,
            AutoSize = true,
            Anchor = AnchorStyles.None

        };
        public Stoper()
        {
            stopers.Add(this);

            GenerateLayout();
            
            
            
           

        }

        public void GenerateLayout()
        {

            start = new Button { 
                Text = "Start",
                MaximumSize = new Size(65, 50) 
            };

            stop = new Button 
            { 
                Text = "Stop",
                MaximumSize = new Size(65, 50) 
            };
            
            reset = new Button 
            { 
                Text = "Reset",
                MaximumSize = new Size(65, 50) 
            };

            showTime = new Label
            {
                Text = $"{hour:D2}:{minute:D2}:{second:D2}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            start.Click += (s, e) => StartStoper();
            reset.Click += (s, e) => ResetStoper();

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            flowLayoutPanel.Controls.Add(start);
            flowLayoutPanel.Controls.Add(stop);
            flowLayoutPanel.Controls.Add(reset);
            tableLayoutPanel.Controls.Add(showTime, 0, 0);
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 0, 1);

            Controls.Add(tableLayoutPanel);
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
            start.Text = "Start";
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
                start.Text = "Pause";
            }
            else
            {
                timer.Stop();
                start.Text = "Start";
            }
        }

        public void UpdateShowTime()
        {
            showTime.Invoke(new Action(() =>
            {
                showTime.Text = $"{hour:D2}:{minute:D2}:{second:D2}";
            }));
        }

        public string GetTime()
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
            return $"{_hour:2D}:{_minute:2D}";

        }

        public void StopStoper()
        {
            if(timer != null)
            {
                timer.Stop();
                start.Text = "Start";
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