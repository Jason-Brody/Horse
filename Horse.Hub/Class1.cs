using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse.HubInterface
{
    public interface IAgentHub
    {
        bool Download(string FileName);

        bool UnzipFile(string FileName);

        bool InstallSoftware(string FileName);

        bool UninstallSoftware(string SoftwareName);
    }
}
