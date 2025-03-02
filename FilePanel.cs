using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace zgloszenia_mantis
{
    public class FilePanel : Panel
    {

        private Button saveFile, changePath, openFile, saveAllTasks;

        readonly File file;

        Label labelPath = new Label
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            MaximumSize = new Size(255, 0)
        };
        public FilePanel()
        {
            file = new File(this);
            GenerateLayout();
        }

        private void GenerateLayout()
        {
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 4,
                ColumnCount = 2
            };
            
            UpdateLabelPath();
            GenerateButton();

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            tableLayoutPanel.Controls.Add(labelPath, 0, 0);

            tableLayoutPanel.Controls.Add(saveFile, 1, 1);
            tableLayoutPanel.Controls.Add(openFile, 0, 3);
            tableLayoutPanel.Controls.Add(changePath, 0, 1);
            tableLayoutPanel.Controls.Add(saveAllTasks, 0, 2);
            tableLayoutPanel.SetColumnSpan(labelPath, 2);
            tableLayoutPanel.SetColumnSpan(openFile, 2);
            tableLayoutPanel.SetColumnSpan(saveAllTasks, 2);

            Controls.Add(tableLayoutPanel);
        }

        public void UpdateLabelPath()
        {
            File.FilePath = Path.Combine(File.DirectoryPath, File.MonthFolder, File.Today.ToString("yyyy-MM-dd") + ".txt");
            labelPath.Text = File.FilePath;
        }
        private void GenerateButton()
        {
            saveFile = new Button
            {
                Text = "Utwórz plik",
                Dock = DockStyle.Fill
            };
            saveFile.Click += (s, e) => file.SaveFile();
            changePath = new Button
            {
                Text = "Zmień",
                Dock = DockStyle.Fill
            };
            changePath.Click += (s, e) => 
            {
                file.ChangePath();
                UpdateLabelPath(); 
            };
            openFile = new Button
            {
                Text = "Otwórz plik",
                Dock = DockStyle.Fill
            };
            openFile.Click += (s, e) => file.OpenFile();

            saveAllTasks = new Button
            {
                Text = "Zapisz wszystkie zadania",
                Dock = DockStyle.Fill
            };
            saveAllTasks.Click += (s, e) => Description.SaveDataForAll();
        
        }

    }
}
