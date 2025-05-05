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


        public Description description;

        private ClientManager clientManager = new ClientManager();
        
        public ComboBox comboBox;
        public TextBox textBox, numberOfTask;
        public DescriptionPanel()
        {
            description = new Description(this);
            GenerateLayout();
           
            ClientManager.ClientsChanged += (o, e) =>
            {
                comboBox.DataSource = null;
                comboBox.DataSource = clientManager.Clients;
                Refresh();
            };
        }


        private void GenerateLayout()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
            };


            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 15F));

            comboBox = new ComboBox
            {
                DataSource = clientManager.Clients,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Dock = DockStyle.Fill

            };

            numberOfTask = new TextBox 
            {
                Dock = DockStyle.Fill,
                ScrollBars = ScrollBars.Vertical,
                
                
            };

            textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                ShortcutsEnabled = true

            };

            Label numberOrLinkTask = new Label() { Text = "Nr/link zgłoszenia: ", Dock = DockStyle.Fill};

            FlowLayoutPanel taskLinkOrNumber = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                WrapContents = false
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


            taskLinkOrNumber.Controls.Add(numberOrLinkTask);
            taskLinkOrNumber.Controls.Add(numberOfTask);

            changeClients.Click += (o, e) => description.ChangeClients(clientManager);
            saveData.Click += description.SavedClicked;

            flowLayoutPanel.Controls.Add(changeClients);
            flowLayoutPanel.Controls.Add(saveData);

            flowLayoutPanel.Resize += (s, e) =>
            {
                int buttonWidth = flowLayoutPanel.ClientSize.Width / 2 - 5 ; 
                changeClients.Width = buttonWidth;
                saveData.Width = buttonWidth;
            };

            tableLayoutPanel.Controls.Add(comboBox, 0, 0);
            tableLayoutPanel.Controls.Add(taskLinkOrNumber, 0, 1);
            tableLayoutPanel.Controls.Add(textBox, 0, 2);
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 0, 3);
            Controls.Add(tableLayoutPanel);


        }

        public void ResetData()
        {
            textBox.Text = string.Empty;
            comboBox.SelectedIndex = 0;
        }
        

        
    }
}
