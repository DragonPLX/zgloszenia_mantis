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
        Frame tab1 = new Frame { Text = "Zad 1" };
        TabPage plusTab = new TabPage { Text = "+" };
        TabPage minusTab = new TabPage { Text = "-" };

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
            }
            else if(tabPage == minusTab) {

                TabPages.RemoveAt(selectedTabIndex);
                e.Cancel = true;
                if(selectedTabIndex == TabCount - 2)
                {
                    Debug.WriteLine("Usuwam zakładkę ostatnią");
                    selectedTabIndex--;
                    SelectedIndex = selectedTabIndex;
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
