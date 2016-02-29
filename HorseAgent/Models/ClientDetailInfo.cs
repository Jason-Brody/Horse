using Horse.TransportModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horse.Agent.Models
{
    public class ClientDetailInfo : ClientInfo
    {
        public ClientDetailInfo() { }

        public ClientDetailInfo(string ConnectionId, string UserName, DateTime startDate)
        {
            this.ConnectionId = ConnectionId;
            this.Name = UserName;
            this.LoginStart = startDate;
        }

        public string ConnectionId { get; set; }

        public DateTime LoginStart { get; set; } = DateTime.Now;
    }
}
