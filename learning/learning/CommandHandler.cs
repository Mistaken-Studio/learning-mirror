using CommandSystem;
using Exiled.API.Features;
using Mistaken.API.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mistaken.learning
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    class CommandHandler : IBetterCommand, CommandSystem.IUsageProvider, IPermissionLocked
    {
        public override string Command => "yes";

        public string[] Usage => new string[]
        {
            "%player%"
        };

        public string Permission => "yes";

        public string PluginName => "learning";

        public override string[] Execute(CommandSystem.ICommandSender sender, string[] args, out bool success)
        {
            success = false;
            if (args.Length == 0)
            {
                return new string[] { "fuck off" };
            }

            if (NazwaPluginu.classDKills.TryGetValue(Player.Get(args[0]), out int value))
            {
                success = true;
                return new string[] { value.ToString() };
            }

            return new string[] { "done" };
        }
    }
}
