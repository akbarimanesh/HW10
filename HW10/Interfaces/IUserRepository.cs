using HW10.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Interfaces
{
    public interface IUserRepository
    {
        public void Login(User user);
        public void Register(User user);
        public void Changepassword(User user, string newpass);
        public List<User> Search(User user, string txt);
        public void Logout(User user);

        public void Status(User user,string sta);
        public int GetId();
    }
}
