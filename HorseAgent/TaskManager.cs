using Horse.TransportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Horse.Agent
{

    delegate void PopTaskHandler(object sender, AgentTask e);
    static class TaskManager
    {
        public static event PopTaskHandler OnTaskPop;

        private static Timer _timer;
        static TaskManager()
        {
            _timer = new Timer();
            _timer.Interval = 1000 * 10;
            _timer.Elapsed += _timer_Elapsed;

            TaskQueue = new Queue<AgentTask>();
        }

        private static void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            
        }

        


        public static Queue<AgentTask> TaskQueue;


    }
}
