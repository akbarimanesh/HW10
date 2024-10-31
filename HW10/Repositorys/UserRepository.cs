using Colors.Net;
using HW10.Entitys;
using HW10.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Colors.Net.StringStaticMethods;
namespace HW10.Repositorys
{
    public class UserRepository : IUserRepository
    {
        string path = "E:/Databasse/users.json";
        public static User? CurrentUser { get; set; }
        public int GetId()
        {
            int id;
            if (File.Exists(path))
            {
                var data = File.ReadAllText(path);
                if (data == "")
                {
                    id = 0;
                    return id;
                }
                else
                {
                    var users = JsonConvert.DeserializeObject<List<User>>(data);
                    id = users.Count();
                }


            }
            else
            {
                File.Create(path);
                id = 0;
                return id;
            }
            return id;
        }
        public void Register(User user)
        {
            var data = File.ReadAllText(path);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            if (users is null)
            {
                users = new List<User>();
            }
            CurrentUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (CurrentUser == null)
            {
                users?.Add(user);

                var result = JsonConvert.SerializeObject(users);
                File.WriteAllText(path, result + Environment.NewLine);
                ColoredConsole.WriteLine($"{Green("You have successfully registered.")}");
            }
            else
                ColoredConsole.WriteLine($"{Red("register failed! username already exists.")}");
        }
        public void Login(User user)
        {
            var data = File.ReadAllText(path);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            if (users is null)
            {
                users = new List<User>();
            }
            CurrentUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
            if (CurrentUser == null)
            {
                ColoredConsole.WriteLine($"{Red("The username or password is incorrect.")}");
            }
            else
                ColoredConsole.WriteLine($"{Green("You have successfully logged in.")}");

        }


        public void Changepassword(User user, string newpass)
        {
            var data = File.ReadAllText(path);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            if (users is null)
            {
                users = new List<User>();
            }

            if (CurrentUser != null)
            {

                CurrentUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                if (CurrentUser == null)
                {
                    ColoredConsole.WriteLine($"{Red("old password is incorrect.")}");
                }
                else
                {
                    CurrentUser.Password = newpass;
                    var result = JsonConvert.SerializeObject(users);
                    File.WriteAllText(path, result + Environment.NewLine);
                    ColoredConsole.WriteLine($"{Green("Password changed successfully.")}");



                }


            }

            else
            {
                ColoredConsole.WriteLine($"{Red("Please login .")}");

            }
        }

        public List<User> Search(User user, string txt)
        {
            var data = File.ReadAllText(path);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            if (users is null)
            {
                users = new List<User>();
            }

            if (CurrentUser != null)
            {
                CurrentUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                var listuser = users.Where(u => u.Username.Contains(txt)).ToList();
                foreach (var item in listuser)
                {
                    ColoredConsole.WriteLine($"{Yellow(item.Username)} | {Yellow("Status")} {(item.Status)}");
                }

                return listuser;
            }
            else
            {
                ColoredConsole.WriteLine($"{Red("Please login .")}");
            }

            return users;


        }

        public void Logout(User user)
        {


            CurrentUser = null;

        }

        public void Status(User user, string sta)
        {
            var data = File.ReadAllText(path);
            var users = JsonConvert.DeserializeObject<List<User>>(data);
            if (users is null)
            {
                users = new List<User>();
            }

            if (CurrentUser != null)
            {
                CurrentUser = users.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);
                if (sta == "AVAILABLE")
                {

                    CurrentUser.Status = true;
                    var result = JsonConvert.SerializeObject(users);
                    File.WriteAllText(path, result + Environment.NewLine);
                    ColoredConsole.WriteLine($"{Green("Username")} {Green(user.Username)} {Green("set available")}");
                }
                else if (sta == "NOTAVAILABLE")
                {
                    CurrentUser.Status = false;
                    var result = JsonConvert.SerializeObject(users);
                    File.WriteAllText(path, result + Environment.NewLine);
                    ColoredConsole.WriteLine($"{Red("Username")} {Red(user.Username)} {Red("set not available")}");
                }
            }
            else
            {
                ColoredConsole.WriteLine($"{Red("Please login .")}");

            }
        }
    }
}


