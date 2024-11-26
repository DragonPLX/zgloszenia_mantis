using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public class MainFrame : TabControl
    {
        readonly Frame tab1 = new Frame { Text = "Zad 1" };
        readonly TabPage plusTab = new TabPage { Text = "+" };
        readonly TabPage minusTab = new TabPage { Text = "-" };

        int selectedTabIndex = 0;
        public MainFrame() 
        {
            Controls.Add(tab1);
            Controls.Add(plusTab);
            Controls.Add(minusTab);
            Selecting += TabClick;
        }

        private void TabClick(object sender, TabControlCancelEventArgs e)
        {
            TabPage tabPage = e.TabPage;

            if(tabPage == plusTab) { 
                Frame newTab = new Frame { Text = $"Zad {TabCount - 1}" };
                TabPages.Insert(TabCount-2, newTab);
                e.Cancel = true;
                selectedTabIndex = TabCount - 2;
                SelectedIndex = selectedTabIndex;

            }
            else if(tabPage == minusTab) {
                if (TabCount > 2 && selectedTabIndex>=0) { 
                    TabPages.RemoveAt(selectedTabIndex);
                    e.Cancel = true;
                    if(selectedTabIndex == TabCount - 2)
                    {
                        Debug.WriteLine("Usuwam zakładkę ostatnią");
                        selectedTabIndex = TabCount - 3;
                        SelectedIndex = selectedTabIndex;
                    }
                }
                else
                {
                    MessageBox.Show("Chcesz usunąć błędną zakładkę!","Błąd",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else 
            {
                Debug.WriteLine("Ustawiam!");
                selectedTabIndex = e.TabPageIndex;
            }

            
            Debug.WriteLine(selectedTabIndex);
        }

        

    }
}
