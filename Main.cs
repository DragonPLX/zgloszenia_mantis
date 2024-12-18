using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += (s, e) => Application.Exit();
            Activated += (s, e) => Focus();
            Controls.Add(new MainFrame { Dock = DockStyle.Fill});
            Size = new Size(600, 550);

            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
        }

        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            if (!IsScreenVisible(this))
            {
                CenterWindow();
            }

            
        }
        private bool IsScreenVisible(Form form)
        {
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Contains(form.Bounds))
                    return true;
            }

            return false;
        }

        private void CenterWindow()
        {
            var screen = Screen.PrimaryScreen;
            if(screen != null) 
            {
                StartPosition = FormStartPosition.Manual;
                Location = new Point
                (
                    screen.WorkingArea.Left + (screen.WorkingArea.Width / 2),
                    screen.WorkingArea.Top + (screen.WorkingArea.Height / 2)
                );

            }

        }


    }
}
