using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Horse.WebSite.Models;
using System.Threading.Tasks;
using PeopleFinderLib;

namespace Horse.WebSite.Controllers
{
    public class BaseUserController : Controller
    {
        protected User currentUser;

        public BaseUserController()
        {
            setUserinfo();
            ViewBag.UserName = currentUser.DisplayName == null ? currentUser.NTAccount : currentUser.DisplayName;
            ViewBag.UserPic = currentUser.Pic;
        }

        public static User GetUser(string name)
        {
            User user = null;
            using (var db = new HorseData())
            {
                
                user = db.Users.Where(u => u.NTAccount == name).FirstOrDefault();

                if (user == null)
                {
                    user = new Horse.WebSite.Models.User();
                    user.NTAccount =name;
                    user.CreateDt = DateTime.Now;
                    user.LastUpdateDt = DateTime.Now;
                    setUserData(user);
                    db.Users.Add(user);
                }
                else if (user.LastUpdateDt.Value.AddDays(1).CompareTo(DateTime.Now) < 0)
                {
                    setUserData(user);
                    user.LastUpdateDt = DateTime.Now;
                }

                db.SaveChanges();
            }

            return user;
        }
        // GET: BaseUser
      

        private static void setUserData(User user)
        {
            var info = Task.Run(() => PeopleFinderHelper.SearchDetail(user.NTAccount.Replace('\\', ':'))).Result;
            if (info.result.Count > 0)
            {
                var u = info.result.Where(c => c.ntUserDomainId.Trim().ToLower() == user.NTAccount.Replace('\\', ':').ToLower()).FirstOrDefault();
                if (u != null)
                {
                    user.Email = u.uid;
                    user.BusinessUnit = u.hpBusinessUnit;
                    user.EmployeeId = int.Parse(u.employeeNumber);
                    user.Manager = u.manager;
                    user.Pic = u.hpPictureThumbnailURI;
                    user.DisplayName = $"{u.cn}({u.hpDisplayNameExtension})";
                }
            }
            else
            {
                user.DisplayName = user.NTAccount;
            }

            
        }

        private void setUserinfo()
        {
            var session = System.Web.HttpContext.Current.Session;
            if (session["u"] == null)
            {
                var me = System.Web.HttpContext.Current.User;
                session["u"] = BaseUserController.GetUser(me.Identity.Name);
            }
            currentUser = session["u"] as User;
            //ViewBag.IsValid = currentUser.IsValid;
        }
    }
}