using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{

    public class Frame : TabPage
    {
        TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
        {
            ColumnCount = 2,
            RowCount = 2,
            Dock = DockStyle.Fill

        };
        public Frame()
        {

            for (int i = 0; i < tableLayoutPanel.ColumnCount; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / tableLayoutPanel.ColumnCount));
            }

            
            for (int i = 0; i < tableLayoutPanel.RowCount; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / tableLayoutPanel.RowCount));
            }

            Controls.Add(tableLayoutPanel);
          
            tableLayoutPanel.Controls.Add(new Options 
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            }, 0, 0);

            tableLayoutPanel.Controls.Add(new StoperPanel 
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            }, 1, 0);

            tableLayoutPanel.Controls.Add(new FilePanel 
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Margin = new Padding(10,0,10,0)
            }, 0, 1);

            tableLayoutPanel.Controls.Add(new DescriptionPanel
            { 
                Dock = DockStyle.Fill 
            }, 1, 1);
        }
    }
}
