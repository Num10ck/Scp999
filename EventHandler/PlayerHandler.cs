using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.API.Features.Roles;
using Exiled.Events.EventArgs.Player;
using MEC;
using UnityEngine;

namespace PluginUtils.Plugins.SCP999.EventHandler
{
    public class PlayerHandler
    {
        public void OnChangeRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                Plugin.Destroy(ev.Player);
            }
        }
        public void OnSpawning(SpawningEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                Plugin.Destroy(ev.Player);
            }
        }
        public void OnSearchPickup(SearchingPickupEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                ev.IsAllowed = false;
            }
        }
        public void OnDropItem(DroppingItemEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP999")
            {
                ev.IsAllowed = false;
            }
        }
        public void OnItemUsed(UsedItemEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                if (ev.Item.Type == ItemType.Painkillers)
                {
                    ev.Player.ShowHint($"<color=green>Вы вылечили игроков на {Config.PainkillersAmount} ХП</color>\n<color=yellow>Подождите <color=red> {Config.PainkillersDelay} секунд</color>>", 5);
                    HealPlayers(ev.Player, Config.PainkillersAmount);

                    Timing.CallDelayed(Config.PainkillersDelay, () =>
                    {
                        ev.Player.AddItem(ItemType.Painkillers);
                    });
                }
                if (ev.Item.Type == ItemType.Medkit)
                {
                    ev.Player.ShowHint($"<color=green>Вы вылечили игроков на {Config.MedkitAmount} ХП</color>\n<color=yellow>Подождите <color=red>{Config.MedkitDelay} секунд</color>", 5);
                    HealPlayers(ev.Player, Config.MedkitAmount);

                    Timing.CallDelayed(Config.MedkitDelay, () =>
                    {
                        ev.Player.AddItem(ItemType.Medkit);
                    });
                }
                if (ev.Item.Type == ItemType.SCP500)
                {
                    ev.Player.ShowHint($"<color=green>Вы вылечили игроков на {Config.SCP500Amount} ХП</color>\n<color=yellow>Подождите <color=red>{Config.SCP500Delay} секунд</color>", 5);
                    HealPlayers(ev.Player, Config.SCP500Amount);

                    Timing.CallDelayed(Config.SCP500Delay, () =>
                    {
                        ev.Player.AddItem(ItemType.SCP500);
                    });
                }
            }
        }
        public void HealPlayers(Player scp, float amount)
        {
            foreach (Player player in Player.List)
            {
                if (player != scp && Vector3.Distance(player.Position, scp.Position) < Config.HealDistance)
                {
                    player.ShowHint("Вас вылечил <i><color=yellow>SCP-999</color></i>", 5);
                    player.Heal(amount);
                }
            }
        }
        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                Plugin.Destroy(ev.Player);
            }
        }
        public void OnLeave(LeftEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                Plugin.Destroy(ev.Player);
            }
        }
        public void OnVerified(VerifiedEventArgs ev)
        {
            foreach (var player in Plugin._schematicScp999.Keys)
            {
                if (player.Role.Is(out FpcRole role))
                {
                    if (!role.IsInvisibleFor.Add(ev.Player))
                        role.IsInvisibleFor.Remove(ev.Player);
                }
            }
        }
        public void OnEntirePocketDimension(EnteringPocketDimensionEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                ev.IsAllowed = false;
            }
        }
        public void OnHandcuff(HandcuffingEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                ev.IsAllowed = false;
            }
        }
        public void OnDoorInteract(InteractingDoorEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                switch (ev.Door.Type)
                {
                    case DoorType.GateA:
                    case DoorType.GateB:
                    case DoorType.Scp914Gate:
                        { }
                        break;
                    default:
                        {
                            ev.IsAllowed = true;
                        }
                        break;
                }
            }
        }
    }
}