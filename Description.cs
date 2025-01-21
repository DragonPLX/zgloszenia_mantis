using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace zgloszenia_mantis
{
    public class Description 
    {
        public event EventHandler SaveDataButtonClicked;
        public event EventHandler ResetStoperChanging;

        private static readonly List<Description> descriptionList = new List<Description>();
        public string Time { get; set; }
        public string DescriptionOfReports { get; set; }
        public string Client {  get; set; }
        public bool IsFile { get; private set; }

        private DescriptionPanel descriptionPanel;

        private ClientManagerForm clientManagerForm;

        public Description(DescriptionPanel descriptionPanel)
        {
            this.descriptionPanel = descriptionPanel;
            descriptionList.Add(this);
        }


        public static void SaveDataForAll()
        {
            foreach(var item in descriptionList)
            {
                item.SaveData(true);
            }
        }


        public bool SaveData()
        {

            try
            {
                StreamWriter writer = new StreamWriter(File.FilePath, true);
                writer.WriteLine(Time + " " + Client + " " + DescriptionOfReports);
                writer.Close();
                MessageBox.Show("Dane zostały zapisane", "Dane zostały zapisane", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;

            } catch (DirectoryNotFoundException)
            {
                var reuslt = MessageBox.Show("Brak pliku! Czy utworzyć plik i zapisać dane?","Brak pliku",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (reuslt == DialogResult.Yes)
                {
                    File file = new File();
                    file.SaveFile();
                    SaveData();
                    return true;
                }
                else
                {
                    return false;
                }

            } catch (Exception) {

                return false;
            }
        }

        public bool SaveData(bool force)
        {
            SaveDataButtonClicked?.Invoke(this, EventArgs.Empty);
            
            if(descriptionPanel != null && descriptionPanel.comboBox.SelectedItem != null && descriptionPanel.comboBox.SelectedItem.ToString() != String.Empty ) 
            {
                Client = descriptionPanel.comboBox.SelectedItem.ToString();
                DescriptionOfReports = descriptionPanel.textBox.Text;
            }else
                return false;


            try
            {
                if (!System.IO.File.Exists(File.FilePath))
                {
                    File file = new File();
                    file.SaveFile();
                }

                StreamWriter writer = new StreamWriter(File.FilePath, true);
                writer.WriteLine(Time + " " + Client + " " + DescriptionOfReports);
                writer.Close();

                ResetStoperChanging?.Invoke(this, EventArgs.Empty);
                descriptionPanel.comboBox.SelectedIndex = 0;
                descriptionPanel.textBox.Clear();

                return true;

            }
            catch (Exception)
            {

                return false;
            }


        }

        public void ChangeClients(ClientManager clientManager)
        {

            clientManagerForm = new ClientManagerForm(clientManager)
            {
                Size = new Size(350, 125),
                StartPosition = FormStartPosition.CenterScreen,
                Visible = true
            };
            
        }

        public void SavedClicked(object sender, EventArgs e)
        {
            SaveDataButtonClicked?.Invoke(sender, e);
            Client = descriptionPanel.comboBox.SelectedItem.ToString();
            DescriptionOfReports = descriptionPanel.textBox.Text;
            IsFile = SaveData();
            if (IsFile)
            {
                ResetStoperChanging?.Invoke(sender, e);
                descriptionPanel.comboBox.SelectedIndex = 0;
                descriptionPanel.textBox.Clear();
            }
        }

    }
}
