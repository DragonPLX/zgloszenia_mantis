using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace zgloszenia_mantis
{
    public class ClientManagerForm : Form
    {
        

        readonly ClientManager clientManager = new ClientManager();
        ComboBox comboBox;
        TextBox textBox;
        public ClientManagerForm()
        {
           GenerateLayout();  
        }

        private void GenerateLayout()
        {
            
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel 
            {
                ColumnCount = 1,
                RowCount = 3,
                Dock = DockStyle.Fill,
            };

            comboBox = new ComboBox 
            {
                DataSource = clientManager.Clients,
                Dock = DockStyle.Fill,
                DropDownStyle = ComboBoxStyle.DropDownList,
            };

            textBox = new TextBox
            {
                Dock = DockStyle.Fill,
                WordWrap = false,
                
            };

            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel 
            {
                Dock = DockStyle.Fill,
                WrapContents = false
            };

            Button removeClient = new Button
            {
                Text = "Usuń"
            };
            Button addClient = new Button
            {
                Text = "Dodaj"
            };
            Button cancel = new Button
            {
                Text = "Anuluj"
            };

            removeClient.Click += (o, e) => 
            {
                clientManager.RemoveClientFromList(comboBox.SelectedItem.ToString());
                RefreshData();
            };
            addClient.Click += (o, e) => 
            {
                clientManager.AddClientToList(textBox.Text);
                RefreshData();
            };
            cancel.Click += (o, e) => Close();

            flowLayoutPanel.Controls.Add(addClient);
            flowLayoutPanel.Controls.Add(removeClient);
            flowLayoutPanel.Controls.Add(cancel);

            tableLayoutPanel.Controls.Add(comboBox,0,0);
            tableLayoutPanel.Controls.Add(textBox,0,1);
            tableLayoutPanel.Controls.Add(flowLayoutPanel,0,2);

            Controls.Add(tableLayoutPanel);
        }

        private void RefreshData()
        {
            comboBox.DataSource = null;
            comboBox.DataSource = clientManager.Clients;
            textBox.Text = "";
        }

     

    }
}
