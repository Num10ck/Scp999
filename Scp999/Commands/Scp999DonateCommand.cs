using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace Scp999.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Scp999DonateCommand : ICommand
    {
        public string Command { get; } = "scp999 me";
        public string[] Aliases { get; } = new string[] { "scp999 me" };
        public string Description { get; } = "Spawning you as SCP-999";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission("scp999donate"))
            {
                response = "You don't have permissions to execute this command";
                return false;
            }

            Player ply = Player.Get(arguments.At(0));
            if (ply.IsDead)
            {
                response = "Cannot spawn dead player";
                return false;
            }
            Plugin.Spawn(ply, false);
            response = "Player spawned";
            return false;
        }
    }
}
