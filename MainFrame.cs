using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public class MainFrame : TabControl
    {

        public MainFrame() 
        {
            Frame tab1 = new Frame { Text="Zad 1", BorderStyle = BorderStyle.Fixed3D };
            TabPage tab2 = new TabPage { Text = "+" };
            TabPage tab3 = new TabPage { Text = "-" };

            Controls.Add(tab1);
            Controls.Add(tab2);
            Controls.Add(tab3);
        }



    }
}
