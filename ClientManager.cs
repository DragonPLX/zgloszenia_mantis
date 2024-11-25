using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public class ClientManager
    {
        public static ClientManager Instance { get; private set; } = new ClientManager();

        public event EventHandler ClientsChanged;

        public List<string> Clients { get; private set; } = new List<string>();

        private readonly string _path = "clients.txt";

        private ClientManager()
        {
            GenerateAndLoadingClients();
        }

        public void AddClientToList(string client)
        {
            if (!String.IsNullOrEmpty(client))
                if (!Clients.Contains(client))
                {
                    Clients.Add(client);
                    Clients.Sort(StringComparer.OrdinalIgnoreCase);
                    ClientsChanged?.Invoke(this, EventArgs.Empty);
                    Debug.WriteLine("Zdarzenie ClientsChanged zostało wywołane.");
                    try
                    {
                        SaveClientsToFile();
                        MessageBox.Show($"Klient {client} został dodany", "Dodany klient", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show("Brak pliku \"client.txt\" w katalogu z aplikacją! Dodaj plik z \";\" jako separatorem klientów", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Błąd przy zapisie danych klientów!", "Błąd przy zapisywaniu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Klient o takiej nazwie już istnieje, przerywam dodawanie!", "Klient już istnieje!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            else
                MessageBox.Show("Nazwa klienta jest pusta, wprowadź prawidłową nazwę", "Błędna nazwa", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void RemoveClientFromList(string client)
        {
            if (!String.IsNullOrEmpty(client))
            {
                if (Clients.Contains(client))
                {
                    Clients.Remove(client);
                    Clients.Sort(StringComparer.OrdinalIgnoreCase);
                    ClientsChanged?.Invoke(this, EventArgs.Empty);
                    Debug.WriteLine("Zdarzenie ClientsChanged zostało wywołane.");

                    try
                    {
                        SaveClientsToFile();
                        MessageBox.Show($"Klient {client} został usunięty", "Usunięty klient", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (FileNotFoundException)
                    {
                        MessageBox.Show("Brak pliku \"client.txt\" w katalogu z aplikacją! Dodaj plik z \";\" jako separatorem klientów", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show($"Błąd przy usuwaniu danych klienta {client}!", "Błąd przy zapisywaniu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Błąd nazwa klienta jest pusta", "Błąd nazwy klienta", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SaveClientsToFile()
        {
            StreamWriter writer = new StreamWriter(_path);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var clientsFromArray in Clients)
            {
                if (clientsFromArray != null && !clientsFromArray.Equals(""))
                    stringBuilder.Append(clientsFromArray.ToString() + ";");
            }
            writer.Write(stringBuilder.ToString());
            writer.Close();
        }

        private void GenerateAndLoadingClients()
        {
            if (!System.IO.File.Exists(_path))
                System.IO.File.Create(_path).Close();

            try
            {
                StreamReader streamReader = new StreamReader(_path);
                var result = streamReader.ReadToEnd().Split(';');
                streamReader.Close();

                foreach (var line in result)
                {
                    Clients.Add(line);
                }

                Clients.Sort(StringComparer.OrdinalIgnoreCase);
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Brak pliku \"client.txt\" w katalogu z aplikacją! Dodaj plik z \";\" jako separatorem klientów", "Błąd!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("Błąd przy zaczytaniu danych klientów!", "Błąd przy wczytywaniu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}