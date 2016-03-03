using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Horse.WebSite.Models;
using Horse.TransportModel;
using System.Data.Entity;

namespace Horse.WebSite.Controllers
{
    [RoutePrefix("api/ClientRegister")]
    public class ClientRegisterController : ApiController
    {
        private User currentUser;
        private HorseData db = new HorseData();

        public ClientRegisterController()
        {
           currentUser = BaseUserController.GetUser(User.Identity.Name);
        }

        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(RegisterInfo info)
        {
            var um = db.UserMachines.Include(u => u.User).Include(u => u.Machine).Where(u => u.Uid == currentUser.Id && u.Machine.Name == info.HostName).FirstOrDefault();
            if(um!=null)
            {
                um.Machine.IPAddress = info.IPAddress;
            }
            else
            {
                um = new UserMachine() { Uid = currentUser.Id, Machine = new Machine() { IPAddress = info.IPAddress, Name = info.HostName } };
                db.UserMachines.Add(um);
            }
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult GetTest1(string machine)
        {
            var um = db.UserMachines.Include(u => u.User).Include(u => u.Machine).Where(u => u.Uid == currentUser.Id && u.Machine.Name == machine).FirstOrDefault();
            if (um != null)
            {
                //um.Machine.IPAddress = info.IPAddress;
            }
            else
            {
                um = new UserMachine() { Uid = currentUser.Id, Machine = new Machine() {  Name = machine } };
                db.UserMachines.Add(um);
            }
            db.SaveChanges();
            return Ok();
        }

        public IHttpActionResult GetTest()
        {
            return Ok<string>(currentUser.DisplayName);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
