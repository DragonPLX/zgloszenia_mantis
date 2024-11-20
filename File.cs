using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace zgloszenia_mantis
{
    public class File : Panel
    {
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        
        private static DateTime today = DateTime.Today;
        
        private string monthFolder = today.ToString("yyyy-MM");

        private Button saveFile, changePath, openFile;
  
        private string filePath;

        readonly TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 3,
            ColumnCount = 2  
        };

        readonly Label labelPath = new Label
        {
            Dock = DockStyle.Fill,
            AutoSize = true,
            MaximumSize = new Size(255,0)
        };
        
        readonly string _pathFile = "Path.txt";
        public File()
        {    
            LoadPath();
            GenerateLayout();
            _ = IntializeAsync();
        }

        private void LoadPath()
        {

            if (!System.IO.File.Exists(_pathFile))
            {
                try
                {
                    StreamWriter _writer = new StreamWriter(_pathFile);
                    _writer.WriteLine(path);
                    _writer.Close();
                    Debug.WriteLine($"Zapisałem plik ze ścieżką: {_pathFile}");
                }
                catch (Exception)
                {
                    MessageBox.Show("Bład przy tworzeniu pliku ze ścieżką!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            try
            {
                StreamReader _reader = new StreamReader(_pathFile);
                path = _reader.ReadLine();
                _reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd przy wczytywaniu ścieżki z pliku!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateLabelPath()
        {
            filePath = Path.Combine(path, monthFolder, today.ToString("yyyy-MM-dd") + ".txt");
            labelPath.Text = filePath;   
        }

        private void GenerateLayout()
        {

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

        private void GenerateButton()
        {
            saveFile = new Button
            {
                Text = "Utwórz plik",
                Dock = DockStyle.Fill
            };
            saveFile.Click += (s, e) => SaveFile();
            changePath = new Button
            {
                Text = "Zmień",
                Dock = DockStyle.Fill
            };
            changePath.Click += (s, e) => ChangePath();
            openFile = new Button
            {
                Text = "Otwórz plik",
                Dock = DockStyle.Fill
            };
            openFile.Click += (s, e) => OpenFile();
        }

        private async Task IntializeAsync()
        {
            await RefreshDate();
        }

        public async Task RefreshDate()
        {
            while (true)
            {
                Debug.WriteLine("Wykonuje porównanie czasu!");
                DateTime _dateTime = DateTime.Today;
                if (today != _dateTime)
                {
                    today = _dateTime;
                    monthFolder = today.ToString("yyyy-MM");
                    UpdateLabelPath();
                }

                await Task.Delay(TimeSpan.FromMinutes(15));
            }
        }

        public void SaveFile()
        {
            if (!Directory.Exists(Path.Combine(path, monthFolder)))
            {
                Directory.CreateDirectory(Path.Combine(path, monthFolder));
            }

            try
            {
                if (!System.IO.File.Exists(filePath))
                {
                    System.IO.File.Create(filePath).Dispose();
                    Debug.WriteLine(filePath);
                    MessageBox.Show("Udało się zapisać plik.");
                }
                else
                {
                    MessageBox.Show("Plik już istnieje!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                MessageBox.Show("Błąd przy zapisie pliku!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void OpenFile()
        {
            try
            {
                if (System.IO.File.Exists(filePath))
                {
                    Process.Start(new ProcessStartInfo { FileName = filePath, UseShellExecute = true });
                }
            }
            catch (System.Exception e)
            {
                Debug.WriteLine(e);
                MessageBox.Show("Błąd otwarcia pliku!", "Bład!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ChangePath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                path = dialog.SelectedPath;
                try
                {
                    StreamWriter stream = new StreamWriter(_pathFile);
                    stream.Write(path);
                    stream.Close();
                    UpdateLabelPath();
                }
                catch (Exception) 
                {
                    MessageBox.Show("Błąd przy zapisie zmiany katalogu!", "Bład!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}