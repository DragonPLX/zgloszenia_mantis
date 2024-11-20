using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public class Description : Panel
    {
        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel 
        {
            Dock = DockStyle.Fill,
            ColumnCount = 1,
            RowCount = 3
        };
        public Description()
        {
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent,20F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent,60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent,20F));



        }
    }
}
