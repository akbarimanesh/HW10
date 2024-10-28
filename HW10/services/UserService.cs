using HW10.Entitys;
using HW10.Interfaces;
using HW10.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.services
{
    public class UserService
    {
        IUserRepository userrep;
        public UserService()
        {
            userrep = new UserRepository();
        }
        public int index()
        {
            return userrep.GetId();

        }
        public void RegisterUser(User user)
        {
            userrep.Register(user);

        }
        public void LoginUser(User user)
        {
            userrep.Login(user);
        }
        public void Changepass(User user, string newpass)
        {
            userrep.Changepassword(user, newpass);
        }
        public List<User> Searchuser(User user, string txt)
        {
            return userrep.Search(user, txt);
        }
        public void Logoutuser(User user)
        {
            userrep.Logout(user);
        }
        public void Statususer(User user,string sta)
        {
            userrep.Status(user,sta);
        }
    }
}
