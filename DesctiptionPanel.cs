using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public class DescriptionPanel : Panel
    {

        public event EventHandler SaveDataButtonClicked;
        public event EventHandler ResetStoperChanging;
        
        public readonly Description description = new Description();

        ClientManager clientManager = new ClientManager();
        public bool IsFile { get; private set; }
        ComboBox comboBox;
        TextBox textBox;
        public DescriptionPanel()
        {
            
            GenerateLayout();
            clientManager.ClientsChanged += (o, e) =>
            {
                comboBox.DataSource = null;
                comboBox.DataSource = clientManager.Clients;
            };
        }

        private void GenerateLayout()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };


            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 75F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));

            comboBox = new ComboBox
            {
                DataSource = clientManager.Clients,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill

            };

            textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ShortcutsEnabled = true

            };

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false
            };

            Button changeClients = new Button
            {
                Text = "Zmiana klientów",
                
            };

            Button saveData = new Button
            {
                Text = "Zapisz",
                
            };

            changeClients.Click += (o, e) => description.ChangeClients();
            saveData.Click += SavedClicked;

            flowLayoutPanel.Controls.Add(changeClients);
            flowLayoutPanel.Controls.Add(saveData);

            flowLayoutPanel.Resize += (s, e) =>
            {
                int buttonWidth = flowLayoutPanel.ClientSize.Width / 2 - 5 ; 
                changeClients.Width = buttonWidth;
                saveData.Width = buttonWidth;
            };

            tableLayoutPanel.Controls.Add(comboBox, 0, 0);
            tableLayoutPanel.Controls.Add(textBox, 0, 1);
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 0, 2);
            Controls.Add(tableLayoutPanel);


        }
        public void SavedClicked(object sender, EventArgs e)
        {
            SaveDataButtonClicked?.Invoke(sender, e);
            description.Client = comboBox.SelectedItem.ToString();
            description.DescriptionOfReports = textBox.Text;
            IsFile = description.SaveData();
            if (IsFile)
            {
                ResetStoperChanging?.Invoke(sender,e);
                comboBox.SelectedIndex = 0;
                textBox.Clear();
            }
        }

        
    }
}
