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
        readonly Frame tab1 = new Frame { Text = "Zad 1", Name="Frame" };
        readonly TabPage plusTab = new TabPage { Text = "+", Name="plusTab" };
        readonly TabPage minusTab = new TabPage { Text = "-", Name="minusTab" };

        TabPage _selectedTab;
        int _selectedTabIndexMinusOne;
        public MainFrame() 
        {
            Controls.Add(tab1);
            Controls.Add(plusTab);
            Controls.Add(minusTab);
            SelectedIndex = 0;
            _selectedTab = SelectedTab;
            
            SelectedIndexChanged += TabClick;
        }


        private void TabClick(object sender, EventArgs e)
        {
            TabPage tabPage = SelectedTab;

            if (tabPage != null) {
                if(tabPage.Name == "plusTab")
                {
                    AddNewTab();
                }
                else if(tabPage.Name == "minusTab")
                {
                    RemoveCurrentTab();
                }
                else
                {
                    _selectedTab = SelectedTab;
                    _selectedTabIndexMinusOne = SelectedIndex - 1;
                    Debug.WriteLine(_selectedTab);
                }
            }

        }

        private void AddNewTab()
        {
            Frame newTab = new Frame { Text = $"Zad {TabCount - 1}" };
            TabPages.Insert(TabCount - 2, newTab);
            if (Options.IsYesStopOthersStopers) {
                newTab.Stoper.stoper.StopAllOtherStopers();
            }
            newTab.Stoper.stoper.StartStoper();
            SelectedTab = newTab;
        }

        private void RemoveCurrentTab()
        {
            TabPage tabPage = _selectedTab;
            
            if (tabPage != null && tabPage.Name != "plusTab" && tabPage.Name != "minusTab") {
            
                TabPages.Remove(tabPage);
                if(TabCount ==2)
                    SelectedIndex = -1;
                else
                    SelectedIndex = _selectedTabIndexMinusOne;
            }
            else
            {
                MessageBox.Show("Chcesz usunąć błędną zakładkę!", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }   

    }
}
