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

        public string NumberOfTask {  get; set; }
        public string Description { get; set; }

    
        public MyTask(string client, string time, string description)
        {
            Client = client;
            Time = time;
            Description = description;
        }   


        public static string CountTime(List<string> times)
        {
            int hours = 0 , minutes = 0;

            if (times != null && times.Count > 0) 
            {
                foreach (var time in times) 
                {
                    var timesArray = time.Split(':');

                    string hourString = timesArray[0];
                    string minutesString = timesArray[1];

                    int.TryParse(hourString, out int parsedHour);
                    hours += parsedHour;
                    int.TryParse(minutesString, out int parsedMinute);
                    minutes += parsedMinute;

                    if (minutes >= 60)
                    {
                        minutes -= 60;
                        hours++;
                    }

                }

            }

            return $"{hours:d2}:{minutes:d2}";

        }


        public static List<MyTask> JoinTasksInListTasksGroup(List<MyTask> tasks)
        {

            List<MyTask> myTasks = new List<MyTask>();

            var groupOfClient = tasks.GroupBy(e=>e.Client);

            foreach(var client in groupOfClient)
            {

                List<string> times = new List<string>();

                StringBuilder desc = new StringBuilder();

                foreach(var taskOfClient in client.ToList())
                {
                    times.Add(taskOfClient.Time);

                    desc.Append(taskOfClient.Description);

                    if(taskOfClient != client.Last())
                    {
                        desc.Append(", ");
                    }
                }

                string time = CountTime(times);


                myTasks.Add(new MyTask(client.Key, time, desc.ToString()));

            }

            return myTasks;

        }
    }
}
