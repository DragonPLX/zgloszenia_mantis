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
        
        public Options() 
        {
            
            Label label = new Label { Text= "Czy chcesz aby otworzenie nowej zakładki zatrzymywało wszystkie zegary?", Width = 260, Height=50, TextAlign = ContentAlignment.TopCenter  };

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel { Anchor = AnchorStyles.Left | AnchorStyles.Right  };
            RadioButton yes = new RadioButton { Text = "Tak", Checked=true };
            RadioButton no = new RadioButton { Text = "Nie" };
            flowLayoutPanel.Controls.Add(yes);
            flowLayoutPanel.Controls.Add(no);
            Controls.Add(label);
            Controls.Add(flowLayoutPanel);
            

        }





    }
}
