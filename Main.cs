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
            

        }
       
    }
}
