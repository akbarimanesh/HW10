using HW10.Entitys;
using HW10.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using HW10.Queries;
using static Colors.Net.StringStaticMethods;
using Colors.Net;
using System.Data;
using Dapper;
namespace HW10.Repositorys
{
    public class UserRepositoryAdo : IUserRepository
    {
        public static User? CurrentUser { get; set; }
        public void Changepassword(User user, string newpass)
        {
            using (SqlConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(Queriesuser.UpdatePassword, con))
                {
                    using (SqlCommand cmd1 = new SqlCommand(Queriesuser.GetById, con))
                    {
                        if (CurrentUser != null)
                        {
                            cmd1.Parameters.AddWithValue("@Username", user.Username);
                            cmd1.Parameters.AddWithValue("@Password", user.Password);
                            con.Open();
                            using (SqlDataReader reader = cmd1.ExecuteReader())
                            {
                                if (!reader.Read())
                                {

                                    ColoredConsole.WriteLine($"{Red("old password is incorrect.")}");
                                }

                                else
                                {

                                    cmd.Parameters.AddWithValue("@Username", user.Username);
                                    cmd.Parameters.AddWithValue("@oldPassword", user.Password);
                                    cmd.Parameters.AddWithValue("@newPassword", newpass);
                                    con.Close();
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    ColoredConsole.WriteLine($"{Green("Password changed successfully.")}");
                                }
                            }



                        }
                        else ColoredConsole.WriteLine($"{Red("Please login .")}");
                    }
                }

            }




        }


        public void Login(User user)
        {
            using (SqlConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(Queriesuser.GetById, con))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user.Id = (int)reader["Id"];
                            user.Username = (string)reader["Username"];
                            user.Password = (string)reader["Password"];
                            user.Status = (bool)reader["Status"];
                            CurrentUser = user;
                            ColoredConsole.WriteLine($"{Green("You have successfully logged in.")}");
                        }

                        else
                        {


                            ColoredConsole.WriteLine($"{Red("The username or password is incorrect.")}");
                        }


                    }
                }
            }

        }


        public void Logout(User user)
        {
            CurrentUser = null;
        }

        public void Register(User user)
        {
            using (SqlConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(Queriesuser.Create, con))
                {
                    using (SqlCommand cmd1 = new SqlCommand(Queriesuser.GetById, con))
                    {
                        cmd1.Parameters.AddWithValue("@Username", user.Username);
                        cmd1.Parameters.AddWithValue("@Password", user.Password);
                        con.Open();
                        using (SqlDataReader reader = cmd1.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user.Id = (int)reader["Id"];
                                user.Username = (string)reader["Username"];
                                user.Password = (string)reader["Password"];
                                user.Status = (bool)reader["Status"];
                                ColoredConsole.WriteLine($"{Red("register failed! username already exists.")}");
                            }

                            else
                            {

                                cmd.Parameters.AddWithValue("Username", user.Username);
                                cmd.Parameters.AddWithValue("Password", user.Password);
                                con.Close();
                                con.Open();
                                cmd.ExecuteNonQuery();
                                ColoredConsole.WriteLine($"{Green("You have successfully register.")}");
                            }


                        }
                    }
                }

            }
        }

        public List<User> Search(User user, string txt)
        {
            using (SqlConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(Queriesuser.GetAllsearch, con))
                {
                    if (CurrentUser != null)
                    {
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@searchWord", txt);
                        con.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {

                            List<User> users = new List<User>();

                            while (reader.Read())
                            {
                                User user1 = new User()
                                {
                                    Id = (int)reader["Id"],
                                    Username = (string)reader["Username"],
                                    Password = (string)reader["Password"],
                                    Status = (bool)reader["Status"],
                                };

                                users.Add(user1);

                            }
                            foreach (var item in users)
                            {
                                ColoredConsole.WriteLine($"{Yellow(item.Username)} | {Yellow("Status")} {(item.Status)}");
                            }

                            return users;



                        }

                    }
                    else
                    {
                        ColoredConsole.WriteLine($"{Red("Please login .")}");
                    }

                    return null;

                }
            }
        }

        public void Status(User user, string sta)
        {


            using (SqlConnection con = new SqlConnection(Counfiguer.Connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(Queriesuser.UpdateStatus, con))
                {
                    using (SqlCommand cmd1 = new SqlCommand(Queriesuser.GetById, con))
                    {
                        if (CurrentUser != null)
                        {
                            cmd1.Parameters.AddWithValue("@Username", user.Username);
                            cmd1.Parameters.AddWithValue("@Password", user.Password);
                            cmd1.Parameters.AddWithValue("@Status", user.Status);

                            con.Open();
                            using (SqlDataReader reader = cmd1.ExecuteReader())
                            {

                                if (sta == "AVAILABLE")
                                {
                                    CurrentUser.Status = true;
                                    cmd.Parameters.AddWithValue("Username", user.Username);
                                    cmd.Parameters.AddWithValue("Password", user.Password);
                                    cmd.Parameters.AddWithValue("Status", user.Status);
                                    con.Close();
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    ColoredConsole.WriteLine($"{Green("Username")} {Green(user.Username)} {Green("set available")}");
                                }
                                else if (sta == "NOTAVAILABLE")
                                {
                                    CurrentUser.Status = false;
                                    cmd.Parameters.AddWithValue("Username", user.Username);
                                    cmd.Parameters.AddWithValue("Password", user.Password);
                                    cmd.Parameters.AddWithValue("Status", user.Status);
                                    con.Close();
                                    con.Open();
                                    cmd.ExecuteNonQuery();
                                    ColoredConsole.WriteLine($"{Red("Username")} {Red(user.Username)} {Red("set not available")}");
                                }





                            }

                        }
                        else
                        {
                            ColoredConsole.WriteLine($"{Red("Please login .")}");
                        }
                    }
                }
            }
        }

            public int GetId()
            {
                throw new NotImplementedException();
            }
        }
    }
