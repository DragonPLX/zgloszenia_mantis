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

            FlowLayoutPanel layout = new FlowLayoutPanel() 
            {
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                AutoSize = true
                
            };

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

            Button generate = new Button()
            {
                Text="Generuj"
            };
            generate.Click += (s, e) => GenerateData();

            monthComboBox.BindingContextChanged += (s, e) => monthComboBox.SelectedItem = NameMonth[DateTime.Today.Month];


            DataGridView dataGridView = new DataGridView();

            


            layout.Controls.Add(yearComboBox);
            layout.Controls.Add(monthComboBox);
            Controls.Add(layout);
        
        }

        void GenerateData()
        {

        }


        List<MyTask> LoadData(int year, int month)
        {
            string fileSource;

            List<MyTask> myTasks = new List<MyTask>();

            DirectoryInfo directory = new DirectoryInfo(Path.Combine(File.DirectoryPath,$"{year}-{month}"));

            var files = directory.GetFiles();

            string pattern = string.Join("|", clientManager.Clients.Select(Regex.Escape));

            foreach (var file in files) 
            {
                if (file.Exists) 
                {
                    try
                    {
                        var contentOfFile = file.OpenText();

                        var text = contentOfFile.ReadToEnd();

                        var tasksInString = Regex.Split(text,pattern).Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

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
