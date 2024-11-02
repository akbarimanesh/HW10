using HW10.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW10.Queries
{
    public static class Queriesuser
    {
        public static string Create = "insert into Users (Username, Password, Status) values (@Username, @Password, 0);";
        public static string GetById = "select * from Users where Username=@Username and Password=@Password;";
        public static string UpdatePassword = "update Users set Password=@newPassword where Username=@Username and Password=@oldPassword;";
        public static string UpdateStatus = "update Users set Status=@Status where Username=@Username and Password=@Password;";
        public static string GetAllsearch = "select * from Users where Username like @searchWord+'%';";
    }

}
