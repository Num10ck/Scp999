using Exiled.API.Features;
using HarmonyLib;
using RemoteAdmin;
using System;
using System.Reflection;

namespace Scp999
{
    internal class RemoteAdminPatch
    {
        private static Config config;

        [HarmonyPatch(typeof(CommandProcessor), nameof(CommandProcessor.ProcessQuery))]
        public static class RemoteAdmin
        {
            static bool Prefix(string q, CommandSender sender)
            {
                try
                {
                    config = new Config();
                    if (!config.RemoteAdminDisabled) return true;

                    Player player = Player.Get(sender);
                    if (player == null) return true;

                    if (player.CustomInfo != Plugin.NAME) return true;

                    if (q.StartsWith("$") || player == null || !config.DonateList.Contains(player.GroupName)) return true;

                    sender.RaReply($"You not permitted to use RemoteAdmin while being SCP-999", false, true, string.Empty);

                    return false;
                }
                catch (Exception e)
                {
                    Log.Error($"Patch Error - <Server> [RemoteAdmin]:{e}\n{e.StackTrace}");
                    return true;
                }
            }
        }
    }
}