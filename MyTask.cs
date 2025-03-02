using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zgloszenia_mantis
{
    public class MyTask
    {
        public string Client {  get; set; }
        public string Time { get; set; }
        public string Description { get; set; }
    
        public MyTask(string client, string time, string description)
        {
            Client = client;
            Time = time;
            Description = description;
        }   
    }
}
