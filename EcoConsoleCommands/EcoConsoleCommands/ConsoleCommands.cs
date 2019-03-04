using Asphalt;
using Asphalt.Api.Event;
using Eco.Core.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoConsoleCommands
{
    [AsphaltPlugin(nameof(ConsoleCommands))]
    public class ConsoleCommands : IModKitPlugin
    {
        public void OnEnable()
        {
            EventManager.RegisterListener(new EventListener());
        }

        public string GetStatus()
        {
            return "running...";
        }
    }
}
