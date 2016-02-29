using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse.TransportModel
{
    public class AgentTask
    {
        public string Name { get; set; }

        public TaskTypes TaskType { get; set; }
    }

    public enum TaskTypes
    {
        GetSoftWares = 0,
        DownloadSoftware = 1,
        InstallSoftware = 2,
        SetFolder = 3,
    }
}
