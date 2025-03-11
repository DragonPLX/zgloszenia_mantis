using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public partial class ReportFrame : Form
    {
        ComboBox yearComboBox, monthComboBox;

        ClientManager clientManager = new ClientManager();

        readonly Dictionary<int, string> NameMonth = new Dictionary<int, string>()
        { 
            { 1, "Styczeń" },
            { 2, "Luty" },
            { 3, "Marzec" },
            { 4, "Kwiecień" },
            { 5, "Maj" },
            { 6, "Czerwiec" },
            { 7, "Lipiec" },
            { 8, "Sierpień" },
            { 9, "Wrzesień" },
            { 10, "Październik" },
            { 11, "Listopad" },
            { 12, "Grudzień" }
        };

        readonly string[] ArrayOfNameMonth = { "Styczeń","Luty","Marzec","Kwiecień","Maj","Czerwiec","Lipiec","Sierpień","Wrzesień","Październik","Listopad","Grudzień" };

        TableLayoutPanel mainLayout = new TableLayoutPanel()
        {
            Dock = DockStyle.Fill,
            RowCount = 2
        };


        FlowLayoutPanel layout = new FlowLayoutPanel()
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.LeftToRight,
            WrapContents = false,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            AutoSize = true

        };

        Panel panel = new Panel()
        {
            Dock = DockStyle.Fill,
        };

        public ReportFrame()
        {
            ClientManager.ClientsChanged += (s, e) => {};


            GenerateLayout();
            InitializeComponent();
            Text = "Raport";
            Show();
        }


        void GenerateLayout()
        {
            StartPosition = FormStartPosition.CenterScreen;

            

            yearComboBox = new ComboBox() 
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                DataSource = GenerateYears()
            };

            yearComboBox.BindingContextChanged += (s, e) => yearComboBox.SelectedItem = DateTime.Today.Year;
            

            monthComboBox = new ComboBox() 
            { 
                DataSource = ArrayOfNameMonth,
                DropDownStyle = ComboBoxStyle.DropDownList

            };

            Button generateButton = new Button()
            {
                Text="Generuj"
            };
            generateButton.Click += (s, e) => GenerateData();

            monthComboBox.BindingContextChanged += (s, e) => monthComboBox.SelectedItem = NameMonth[DateTime.Today.Month];

            

            layout.Controls.Add(yearComboBox);
            layout.Controls.Add(monthComboBox);
            layout.Controls.Add(generateButton);
            
            mainLayout.Controls.Add(layout);
            
            
            mainLayout.Controls.Add(panel);

            Controls.Add(mainLayout);
        }

        void GenerateData()
        {
            panel.Controls.Clear();

            DataGridView dataGridView = new DataGridView()
            {
                Dock = DockStyle.Fill,
                
            };
            var allTasks = LoadData((int)yearComboBox.SelectedItem, NameMonth.Single(m => m.Value == monthComboBox.SelectedItem.ToString()).Key);

            var tasksGrouped = MyTask.JoinTasksInListTasksGroup(allTasks);
            //dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = tasksGrouped;


            //dataGridView.Columns.Add(new DataGridViewColumn() { DataPropertyName = "Client", HeaderText = "Klient" });
            //dataGridView.Columns.Add(new DataGridViewColumn() { DataPropertyName = "Time", HeaderText = "Czas" });
            //dataGridView.Columns.Add(new DataGridViewColumn() { DataPropertyName = "Description", HeaderText = "Opis", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            dataGridView.DataBindingComplete += (s, e) => 
            { 
                //dataGridView.Columns[].Visible = false;
                dataGridView.Columns[0].HeaderText = "Klient";
                dataGridView.Columns[1].HeaderText = "Czas";
                dataGridView.Columns[2].HeaderText = "Opis";
                dataGridView.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            };

            panel.Controls.Add(dataGridView);
        }


        List<MyTask> LoadData(int year, int month)
        {


            List<MyTask> myTasks = new List<MyTask>();

            DirectoryInfo directory = new DirectoryInfo(Path.Combine(File.DirectoryPath,$"{year}-{month:d2}"));

            var files = directory.GetFiles();

            foreach (var file in files) 
            {
                if (file.Exists) 
                {
                    try
                    {
                        var reader = file.OpenText();

                        var jsonText = reader.ReadToEnd();
                        reader.Close();
                        
                        var tasks = JsonConvert.DeserializeObject<List<MyTask>>(jsonText);

                        myTasks.AddRange(tasks);
                    }
                    catch (Exception ex) 
                    {
                        Debug.WriteLine(ex.Message);
                    }
                }

            }

            
            return myTasks;

        }

        List<int> GenerateYears()
        {
            List<int> years = new List<int>();
            
            DateTime now = DateTime.Today;
            
            for(int i = now.Year-5; i <= now.Year+5; i++)
            {
                years.Add(i);
            }
            return years;

        } 

    }
}
