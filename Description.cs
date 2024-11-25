using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zgloszenia_mantis
{
    public class Description 
    {

        public ClientManager ClientManager { get; private set; } = new ClientManager();
        


        public Description()
        {
            

        }

        public void SaveData()
        {

        }

        public void ChangeClients()
        {
            
            ClientManagerForm clientManagerForm = new ClientManagerForm
            {
                Size = new Size(350, 125),
                StartPosition = FormStartPosition.CenterScreen,
                Visible = true
            };
            
        }

       
    }
}
