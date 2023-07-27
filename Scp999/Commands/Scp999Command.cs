using CommandSystem;
using Exiled.API.Features;
using Exiled.Permissions.Extensions;
using System;

namespace Scp999.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Scp999Command : ICommand
    {
        public string Command { get; } = "scp999";
        public string[] Aliases { get; } = new string[] { "scp999 ( id )" };
        public string Description { get; } = "Spawning SCP-999";

        private Config config;
        private int Spawns;
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            config = new Config();
            if (!((CommandSender)sender).CheckPermission("scp999"))
            {
                response = "You don't have permissions to execute this command";
                return false;
            }
            if (arguments.Count != 1)
            {
                response = "Incorrect arguments. Use \"scp999 (id)\"";
                return false;
            }
            Player ply = Player.Get(arguments.At(0));
            Player plySender = Player.Get(sender);
            if (plySender.GroupName.Contains(config.DonateList.ToString()) && Spawns >= config.SpawnsCount)
            {
                response = $"Cannot spawn SCP-999 more than {config.SpawnsCount} times";
                return false;
            }
            if (plySender.GroupName.Contains(config.DonateList.ToString()) && ply != plySender)
            {
                response = $"You not permitted to spawn anybody else than you";
                return false;
            }
            if (ply == null)
            {
                response = $"No players with id {arguments.At(0)}";
                return false;
            }
            if (ply.IsDead)
            {
                response = $"Player with id {arguments.At(0)} spawned as SCP-999 on spawnpoint";
                Plugin.Spawn(ply, true);
                return true;
            }
            response = $"Player with id {arguments.At(0)} spawned as SCP-999 on current position";
            Plugin.Spawn(ply, false);
            return true;
        }
    }
}
