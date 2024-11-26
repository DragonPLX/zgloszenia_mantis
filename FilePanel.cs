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

        private Button saveFile, changePath, openFile;

        readonly File file;

        Label labelPath;
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
                RowCount = 3,
                ColumnCount = 2
            };

            labelPath = new Label
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                MaximumSize = new Size(255, 0)
            };

            UpdateLabelPath();
            GenerateButton();

            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            tableLayoutPanel.Controls.Add(labelPath, 0, 0);

            tableLayoutPanel.Controls.Add(saveFile, 1, 1);
            tableLayoutPanel.Controls.Add(openFile, 0, 2);
            tableLayoutPanel.Controls.Add(changePath, 0, 1);
            tableLayoutPanel.SetColumnSpan(labelPath, 2);
            tableLayoutPanel.SetColumnSpan(openFile, 2);

            Controls.Add(tableLayoutPanel);
        }

        public void UpdateLabelPath()
        {
            File.FilePath = Path.Combine(file.DefaultPath, file.MonthFolder, File.Today.ToString("yyyy-MM-dd") + ".txt");
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
        }

    }
}
