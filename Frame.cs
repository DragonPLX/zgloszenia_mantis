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
        readonly Options options;
        readonly StoperPanel stoperPanel;
        public StoperPanel Stoper { get { return stoperPanel; } }
        readonly FilePanel filePanel;
        readonly DescriptionPanel descriptionPanel;

        readonly TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
        {
            ColumnCount = 2,
            RowCount = 2,
            Dock = DockStyle.Fill

        };
        public Frame()
        {
            
            options = new Options
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };

            stoperPanel = new StoperPanel
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.FixedSingle
            };
            filePanel = new FilePanel
            {
                Anchor = AnchorStyles.Left | AnchorStyles.Right,
                Margin = new Padding(10, 0, 10, 0)
            };
            descriptionPanel = new DescriptionPanel()
            {
                Dock = DockStyle.Fill
            };

            stoperPanel.Stop.Click += Stop_Click;
            descriptionPanel.description.SaveDataButtonClicked += BindingDataGetTime;
            descriptionPanel.description.ResetStoperChanging += DescriptionPanel_ResetStoperChanging;
            stoperPanel.stoper.ResetTimer += Stoper_ResetTimer;

            for (int i = 0; i < tableLayoutPanel.ColumnCount; i++)
            {
                tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / tableLayoutPanel.ColumnCount));
            }

            
            for (int i = 0; i < tableLayoutPanel.RowCount; i++)
            {
                tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / tableLayoutPanel.RowCount));
            }

            tableLayoutPanel.Controls.Add(options, 0, 0);

            tableLayoutPanel.Controls.Add(stoperPanel, 1, 0);

            tableLayoutPanel.Controls.Add(filePanel, 0, 1);

            tableLayoutPanel.Controls.Add(descriptionPanel, 1, 1);

            Controls.Add(tableLayoutPanel);
        }

        private void Stoper_ResetTimer(object sender, EventArgs e)
        {
            descriptionPanel.ResetData();
        }

        private void DescriptionPanel_ResetStoperChanging(object sender, EventArgs e)
        {
            stoperPanel.stoper.ResetStoper();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            descriptionPanel.description.SavedClicked(sender, e);
        }

        private void BindingDataGetTime(object o, EventArgs e)
        {
            descriptionPanel.description.Time = stoperPanel.stoper.GetTimeToString();
            
        }
    }
}
