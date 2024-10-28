
using Colors.Net;
using HW10.Entitys;
using HW10.services;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.IO;
using static Colors.Net.StringStaticMethods;
UserService userService = new UserService();
User user = new User();
while (true)
{
    Console.Clear();
    ColoredConsole.Write($"{Blue("Enter Command :")}");
    string command = Console.ReadLine();
    var section = command.Split(" ");
    var com = section[0];
    if (com == "login")
    {
        try
        {
            user.Username = section[2];
            user.Password = section[4];
            userService.LoginUser(user);
            
        }
        catch (Exception ex)
        {
            ColoredConsole.WriteLine($"{Red("Should Enter The Command Register Like This: Login --username [username] --password [password]")}");
        }
        
        Console.ReadKey();
    }
    if (com == "register")
    {
        try
        {
            user.Id = userService.index();
            user.Username = section[2];
            user.Password = section[4];
            userService.RegisterUser(user);
           
        }
        catch(Exception ex)
        {
            ColoredConsole.WriteLine($"{Red("Should Enter The Command Register Like This: Register --username [username] --password [password]")}");

        }
        
        Console.ReadKey();
    }

    if (com == "change")
    {
        try
        {
            if (section[2] == "available")
            {

                userService.Statususer(user, section[2]);


                
            }
            else if (section[2] == "notavailable")
            {

                userService.Statususer(user, section[2]);
               
            }
        }
        catch (Exception ex)
        {
            ColoredConsole.WriteLine($"{Red("Should Enter The Command Register Like This: Change --status [available]/[not available] ")}");

        }

        Console.ReadKey();
    }
    if (com == "search")
    {
        try
        {
            var text = section[2];
             userService.Searchuser(user,text);
            
        }

        catch (Exception ex)
        {
            ColoredConsole.WriteLine($"{Red("Should Enter The Command Register Like This: Search --username [username] ")}");

        }
        Console.ReadKey();
    }
    if (com == "changepassword")
    {
        try
        {
            user.Password = section[2];
            var newpass = section[4];
            userService.Changepass(user, newpass);
            
        }
        catch (Exception ex)
        {
            ColoredConsole.WriteLine($"{Red("Should Enter The Command Register Like This: ChangePassword --old [myOldPassword] --new [myNewPassword] ")}");

        }
        Console.ReadKey();
    }
    if (com == "logout")
    {
        try
        {
            userService.Logoutuser(user);
            ColoredConsole.WriteLine($"{Red("You are logged out.")}");
        }
        catch (Exception ex)
        {
            ColoredConsole.WriteLine($"{Red("Should Enter The Command Register Like This: logout")}");

        }
        Console.ReadKey();
    }
}
