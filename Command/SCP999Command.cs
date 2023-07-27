using CommandSystem;
using Exiled.API.Features;
using System;
using PluginUtils.Plugins.SCP999;
using PlayerRoles;
using Exiled.Permissions.Extensions;

namespace PluginUtils.Command
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class SCP999Command : ICommand
    {
        public string Command { get; } = "scp999";

        public string[] Aliases { get; } = new string[] { "scp999 (me / id / all)" };

        public string Description { get; } = "Введите scp999 (id / all), чтобы стать щекоточным монстренком SCP-999.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!((CommandSender)sender).CheckPermission(".scp999"))
            {
                response = "У вас нет прав на использование этой команды.";
                return false;
            }

            if (arguments.Count != 1)
            {
                response = "Используйте: scp999 (me / id / all)";
                return false;
            }

            switch (arguments.At(0))
            {
                case "*":
                case "all":
                    {
                        foreach (Player pl in Player.List)
                        {
                            if (pl.Role == RoleTypeId.Spectator || pl.Role == RoleTypeId.None)
                                continue;

                            Plugin.Spawn(pl, false);
                        }
                        response = "<color=green>Игроки стали монстрёнками SCP-999.</color>";
                        return false;
                    }
                case "me":
                    {
                        Plugin.Spawn(Player.Get(sender), false);
                        response = "<color=green>Вы стали монстрёнком SCP-999.</color>";
                        return false;
                    }
                default:
                    Player ply = Player.Get(arguments.At(0));
                    if (ply == null)
                    {
                        response = $"<color=red>Игрок не найден: {arguments.At(0)}</color>";
                        return false;
                    }

                    if (ply.IsDead)
                    {
                        response = $"<color=red>SCP999: Игрок мертв.</color>";
                        return false;
                    }

                    Plugin.Spawn(ply, false);

                    response = "<color=green>Игрок стал монстрёнком SCP-999.</color>";
                    return false;
            }
        }
    }
}
