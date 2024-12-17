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
    public class File
    {
        public string DefaultPath { get; private set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public static DateTime Today { get; private set; } = DateTime.Today;

        public string MonthFolder { get; private set; } = Today.ToString("yyyy-MM");

        public static string FilePath { get; set; }

        private readonly FilePanel filePanel;

        public readonly string pathFile = "Path.txt";

        public File()
        {
            LoadPath();
        }

        public File(FilePanel _filePanel)
        {
            filePanel = _filePanel;
            LoadPath();
            _ = IntializeAsync();
        }

        private void LoadPath()
        {
            if (!System.IO.File.Exists(pathFile))
            {
                try
                {
                    StreamWriter _writer = new StreamWriter(pathFile);
                    _writer.WriteLine(DefaultPath);
                    _writer.Close();
                    Debug.WriteLine($"Zapisałem plik ze ścieżką: {pathFile}");
                }
                catch (Exception)
                {
                    MessageBox.Show("Bład przy tworzeniu pliku ze ścieżką!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            try
            {
                StreamReader _reader = new StreamReader(pathFile);
                DefaultPath = _reader.ReadLine();
                _reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd przy wczytywaniu ścieżki z pliku!", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task IntializeAsync()
        {
            await RefreshDate();
        }

        public async Task RefreshDate()
        {
            while (true)
            {
               // Debug.WriteLine("Wykonuje porównanie czasu!");
                DateTime _dateTime = DateTime.Today;
                if (Today != _dateTime)
                {
                    Today = _dateTime;
                    MonthFolder = Today.ToString("yyyy-MM");
                    filePanel.UpdateLabelPath();
                }
                filePanel.UpdateLabelPath();
                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }

        public void SaveFile()
        {
            if (!Directory.Exists(Path.Combine(DefaultPath, MonthFolder)))
            {
                Directory.CreateDirectory(Path.Combine(DefaultPath, MonthFolder));
            }

            try
            {
                if (!System.IO.File.Exists(FilePath))
                {
                    System.IO.File.Create(FilePath).Dispose();
                    Debug.WriteLine(FilePath);
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
                if (System.IO.File.Exists(FilePath))
                {
                    Process.Start(new ProcessStartInfo { FileName = FilePath, UseShellExecute = true });
                }
                else
                {
                    var result = MessageBox.Show("Plik nie został utworzony, czy utworzyć plik?", "Brak pliku", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        SaveFile();
                    }
                }
            }
            catch (Exception)
            {     
                MessageBox.Show("Błąd otwarcia pliku!", "Bład!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ChangePath()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                DefaultPath = dialog.SelectedPath;
                try
                {
                    StreamWriter stream = new StreamWriter(pathFile);
                    stream.Write(DefaultPath);
                    stream.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Błąd przy zapisie zmiany katalogu!", "Bład!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}