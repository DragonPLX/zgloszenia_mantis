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
    public class Description 
    {

        public string Time { get; set; }
        public string DescriptionOfReports { get; set; }
        public string Client {  get; set; }

        private ClientManagerForm clientManagerForm;
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

        public void ChangeClients(ClientManager clientManager)
        {

            clientManagerForm = new ClientManagerForm(clientManager)
            {
                Size = new Size(350, 125),
                StartPosition = FormStartPosition.CenterScreen,
                Visible = true
            };
            
        }

       
    }
}
