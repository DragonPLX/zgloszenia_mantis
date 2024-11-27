using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public class Options : Panel
    {
        public static event EventHandler IsYesStopOthersStopersChanged;
        public static bool IsYesStopOthersStopers { get; private set; } = true;

        RadioButton yes,no;
        public Options()
        {
            GeneratedLayout();
            IsYesStopOthersStopersChanged += CheckedIsYesStopOthersStopers;
        }

        public void GeneratedLayout()
        {
            Label label = new Label { Text = "Czy chcesz aby otworzenie nowej zakładki zatrzymywało wszystkie zegary?", Width = 260, Height = 50, TextAlign = ContentAlignment.TopCenter };

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel { Anchor = AnchorStyles.Left | AnchorStyles.Right };
            yes = new RadioButton { Text = "Tak" };
            no = new RadioButton { Text = "Nie" };
            if (IsYesStopOthersStopers)
                yes.Checked = true;
            else
                no.Checked = true;


            yes.Click += (o, e) => 
            {
                IsYesStopOthersStopers = true;
                IsYesStopOthersStopersChanged?.Invoke(this, EventArgs.Empty);
            };
            no.Click += (o, e) => 
            { 
                IsYesStopOthersStopers = false;
                IsYesStopOthersStopersChanged?.Invoke(this, EventArgs.Empty);
            };
            flowLayoutPanel.Controls.Add(yes);
            flowLayoutPanel.Controls.Add(no);

            Controls.Add(label);
            Controls.Add(flowLayoutPanel);
        }

        private void CheckedIsYesStopOthersStopers(object o , EventArgs e)
        {
            if (IsYesStopOthersStopers)
                yes.Checked = true;
            else
                no.Checked = true;

        }
    }
}