using Colors.Net;
using HW10.Entitys;
using HW10.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using HW10.Queries;
using static Colors.Net.StringStaticMethods;

namespace HW10.Repositorys
{
    public class UserRepositoryDapper:IUserRepository
    {
        public static User? CurrentUser { get; set; }

        public void Changepassword(User user, string newpass)
        {
            using (IDbConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                if (CurrentUser != null)
                {

                    CurrentUser = con.QueryFirstOrDefault<User>(Queriesuser.GetById, new { Username = user.Username, Password = user.Password });
                    if (CurrentUser == null)
                    {
                        ColoredConsole.WriteLine($"{Red("old password is incorrect.")}");
                    }
                    else
                    {

                        con.Execute(Queriesuser.UpdatePassword, new { Username = user.Username, oldPassword = CurrentUser.Password, newPassword = newpass });
                        ColoredConsole.WriteLine($"{Green("Password changed successfully.")}");
                    }
                }
                else ColoredConsole.WriteLine($"{Red("Please login .")}");
            }
        }

        public void Register(User user)
        {
            using (IDbConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                CurrentUser = con.QueryFirstOrDefault<User>(Queriesuser.GetById, new { Username = user.Username, Password = user.Password });
                if (CurrentUser == null)
                {
                    con.Execute(Queriesuser.Create, new { user.Username, user.Password });
                    ColoredConsole.WriteLine($"{Green("You have successfully register.")}");
                }
                else ColoredConsole.WriteLine($"{Red("register failed! username already exists.")}");
            }
           
        }

        public void Login(User user)
        {
            using (IDbConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                 CurrentUser=con.QueryFirstOrDefault<User>(Queriesuser.GetById, new { Username = user.Username, Password = user.Password });
                if (CurrentUser == null)
                {
                    ColoredConsole.WriteLine($"{Red("The username or password is incorrect.")}");
                }
                else
                    ColoredConsole.WriteLine($"{Green("You have successfully logged in.")}");

            }
        }

        public void Logout(User user)
        {
            CurrentUser = null;
        }

        public List<User> Search(User user, string txt)
        {
            using (IDbConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                if (CurrentUser != null)
                {
                    CurrentUser = con.QueryFirstOrDefault<User>(Queriesuser.GetById, new { Username = user.Username, Password = user.Password });
                    var listUser= con.Query<User>(Queriesuser.GetAllsearch, new { searchWord = txt }).ToList();
                    foreach (var item in listUser)
                    {
                        ColoredConsole.WriteLine($"{Yellow(item.Username)} | {Yellow("Status")} {(item.Status)}");
                    }

                    return listUser;

                }
                else
                {
                    ColoredConsole.WriteLine($"{Red("Please login .")}");
                }

                return null;
            }   
            
                
        }

        public void Status(User user, string sta)
        {
            using (IDbConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                if (CurrentUser != null)
                {
                    CurrentUser = con.QueryFirstOrDefault<User>(Queriesuser.GetById, new { Username = user.Username, Password = user.Password });
                    if (sta == "AVAILABLE")
                    {
                        CurrentUser.Status = true;

                        con.Execute(Queriesuser.UpdateStatus, new { Username = user.Username, Password = user.Password, Status = CurrentUser.Status });
                        ColoredConsole.WriteLine($"{Green("Username")} {Green(user.Username)} {Green("set available")}");
                    }
                    else if (sta == "NOTAVAILABLE")
                    {
                        CurrentUser.Status = false;
                        con.Execute(Queriesuser.UpdateStatus, new { Username = user.Username, Password = user.Password,Status = user.Status });
                        ColoredConsole.WriteLine($"{Red("Username")} {Red(user.Username)} {Red("set not available")}");
                    }
                }
                else
                {
                    ColoredConsole.WriteLine($"{Red("Please login .")}");
                }
}
                
        }

        public int GetId()
        {
            throw new NotImplementedException();
        }
    }
}
