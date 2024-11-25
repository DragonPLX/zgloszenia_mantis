using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public class StoperPanel : Panel
    {
        private Button start , stop, reset;

        private Label showTime;

        private Stoper stoper;
        public Button Start { get => start; set => start = value; } 

        public Button Reset { get => reset; set => reset = value; }
        public Button Stop { get => stop; set => stop = value; }
        public Label ShowTime { get => showTime; set => showTime = value; }

        
        public StoperPanel()
        {
            stoper = new Stoper(this);
            GenerateLayout();
        }

        public void GenerateLayout()
        {
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

            start = new Button
            {
                Text = "Start",
                MaximumSize = new Size(65, 50)
            };

            Stop = new Button
            {
                Text = "Stop",
                MaximumSize = new Size(65, 50)
            };

            Reset = new Button
            {
                Text = "Reset",
                MaximumSize = new Size(65, 50)
            };

            ShowTime = new Label
            {
                Text = $"{stoper.Hour:D2}:{stoper.Minute:D2}:{stoper.Second:D2}",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            start.Click += (s, e) => stoper.StartStoper();
            Reset.Click += (s, e) => stoper.ResetStoper();

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));

            flowLayoutPanel.Controls.Add(start);
            flowLayoutPanel.Controls.Add(Stop);
            flowLayoutPanel.Controls.Add(Reset);
            tableLayoutPanel.Controls.Add(ShowTime, 0, 0);
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 0, 1);

            Controls.Add(tableLayoutPanel);
        }
    }
}
