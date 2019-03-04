using Asphalt.Api.Event;
using Asphalt.Events.Console;
using Eco.Core.Plugins;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoConsoleCommands
{
    public class EventListener
    {
        [EventHandler]
        public void OnConsoleInput(ConsoleInputEvent evt)
        {
            string text = evt.Text + " ";

            string command = text.Substring(0, text.IndexOf(" "));
            string parameters = text.Substring(text.IndexOf(" ")).Trim();

            switch (command)
            {
                case "say":
                    {
                        ChatManager.ServerMessageToAll(new LocString($"<i>[CONSOLE]</i>  <color=red><size=30><b>{parameters}</b></size></color>"), false, DefaultChatTags.Notifications, ChatCategory.Default);
                        Console.WriteLine("[CONSOLE] " + parameters);
                        break;
                    }
                case "online":
                    {
                        foreach (User u in UserManager.OnlineUsers)
                        {
                            Console.WriteLine(u.Name);
                        }
                        Console.WriteLine(UserManager.OnlineUsers.Count() + " players online");
                        break;
                    }
                case "whois":
                    {
                        Whois(parameters);
                        break;
                    }
                case "admin":
                    {
                        Admin(parameters);
                        break;
                    }
                case "save":
                    {
                        StorageManager.SaveAndFlush();
                        break;
                    }
            }
        }

        private void Admin(string pName)
        {
            User user = FindUser(pName);

            if (user == null)
            {
                Console.WriteLine("User not found!");
                return;
            }

            UserManager.AddAdmin(user.SteamId);
            UserManager.AddAdmin(user.SlgId);

            Console.WriteLine($"{user.Name} is now admin!");
        }

        private void Whois(string pName)
        {
            User user = FindUser(pName);

            if (user == null)
            {
                Console.WriteLine("User not found!");
                return;
            }

            Console.WriteLine($"User {user.Name} has the following ids:");

            Console.WriteLine($"   - ID: " + user.Id);
            Console.WriteLine($"   - SLG ID: " + user.SlgId);
            Console.WriteLine($"   - Steam ID: " + user.SteamId);
        }

        private User FindUser(string pName)
        {
            return UserManager.Users.FirstOrDefault(u => u.Name.ToLower() == pName.ToLower());
        }
    }
}
