using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.API.Features.Roles;
using Exiled.API.Interfaces;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using PlayerRoles;
using PluginUtils.Loader.Features;
using PluginUtils.Plugins.SCP999.EventHandler;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using UnityEngine;

namespace PluginUtils.Plugins.SCP999
{
    internal class Plugin : Utils<Config>
    {
        public override string Name => "SCP999";
        public override byte Priority => 10;

        private PlayerHandler _playerHandler;
        private ServerHandler _serverHandler;
        public static Dictionary<Player, SchematicObject> _schematicScp999;
        public override void Enable()
        {
            _playerHandler = new PlayerHandler();
            _serverHandler = new ServerHandler();
            _schematicScp999 = new Dictionary<Player, SchematicObject>();

            Exiled.Events.Handlers.Player.ChangingRole += _playerHandler.OnChangeRole;
            Exiled.Events.Handlers.Player.Spawning += _playerHandler.OnSpawning;
            Exiled.Events.Handlers.Player.Died += _playerHandler.OnDied;
            Exiled.Events.Handlers.Player.Left += _playerHandler.OnLeave;
            Exiled.Events.Handlers.Player.Verified += _playerHandler.OnVerified;
            Exiled.Events.Handlers.Player.SearchingPickup += _playerHandler.OnSearchPickup;
            Exiled.Events.Handlers.Player.DroppingItem += _playerHandler.OnDropItem;
            Exiled.Events.Handlers.Player.UsedItem += _playerHandler.OnItemUsed;
            Exiled.Events.Handlers.Player.EnteringPocketDimension += _playerHandler.OnEntirePocketDimension;
            Exiled.Events.Handlers.Player.Handcuffing += _playerHandler.OnHandcuff;
            Exiled.Events.Handlers.Player.InteractingDoor += _playerHandler.OnDoorInteract;

            Exiled.Events.Handlers.Warhead.Stopping += _serverHandler.OnWarheadStop;
            Exiled.Events.Handlers.Scp096.Enraging += _serverHandler.OnScpEnrage;
            Exiled.Events.Handlers.Scp096.AddingTarget += _serverHandler.OnAddingTarget;

            base.Enable();
        }
        public override void Disable()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= _playerHandler.OnChangeRole;
            Exiled.Events.Handlers.Player.Spawning -= _playerHandler.OnSpawning;
            Exiled.Events.Handlers.Player.Died -= _playerHandler.OnDied;
            Exiled.Events.Handlers.Player.Left -= _playerHandler.OnLeave;
            Exiled.Events.Handlers.Player.Verified -= _playerHandler.OnVerified;
            Exiled.Events.Handlers.Player.SearchingPickup -= _playerHandler.OnSearchPickup;
            Exiled.Events.Handlers.Player.DroppingItem -= _playerHandler.OnDropItem;
            Exiled.Events.Handlers.Player.UsedItem -= _playerHandler.OnItemUsed;
            Exiled.Events.Handlers.Player.EnteringPocketDimension -= _playerHandler.OnEntirePocketDimension;
            Exiled.Events.Handlers.Player.Handcuffing -= _playerHandler.OnHandcuff;
            Exiled.Events.Handlers.Player.InteractingDoor -= _playerHandler.OnDoorInteract;

            Exiled.Events.Handlers.Warhead.Stopping -= _serverHandler.OnWarheadStop;
            Exiled.Events.Handlers.Scp096.Enraging -= _serverHandler.OnScpEnrage;
            Exiled.Events.Handlers.Scp096.AddingTarget -= _serverHandler.OnAddingTarget;

            _schematicScp999 = null;
            _playerHandler = null;
            _serverHandler = null;

            base.Disable();
        }
        public static void Spawn(Player player, bool isSpawn)
        {
            var _config = new Config();
            player.Role.Set(RoleTypeId.Tutorial, RoleSpawnFlags.AssignInventory);
            player.Position = RoleExtensions.GetRandomSpawnLocation(RoleTypeId.Scientist).Position;
            player.Health = _config.Health;
            player.CustomInfo = "SCP-999";
            player.IsGodModeEnabled = _config.IsGodModeEnabled;
            player.AddItem(ItemType.Painkillers);
            player.AddItem(ItemType.Medkit);
            player.AddItem(ItemType.SCP500);
            player.Broadcast(7, "<color=yellow>Вы стали <color=red>SCP-999</color> Щекоточный монстр\n<color=#FFA500>Остальная информация в консоли на [Ё].</color>");
            player.SendConsoleMessage($"Вы стали SCP-999 - Щекоточный монстр.\nУ вас {_config.Health} ХП.\nВы можете использовать медицинские предметы чтобы лечить всех рядом.", "green");
            player.EnableEffect(EffectType.Disabled);

            var scpObject = ObjectSpawner.SpawnSchematic("SCP999", player.Position + _config.Position, Quaternion.Euler(player.Rotation + _config.Rotation), new Vector3(1, 1, 1));
            scpObject.transform.parent = player.Transform;

            player.Scale = new Vector3(0.7f, 0.7f, 0.7f);

            foreach (Player pl in Player.List)
            {
                if (player.Role.Is(out FpcRole role))
                {
                    if (!role.IsInvisibleFor.Add(pl))
                        role.IsInvisibleFor.Remove(pl);
                }
            }

            _schematicScp999.Add(player, scpObject);
        }
        public static void Destroy(Player player)
        {
            if (player != null)
            {
                player.CustomInfo = string.Empty;
                player.Scale = new Vector3(1, 1, 1);

                foreach (Player pl in Player.List)
                {
                    if (player.Role.Is(out FpcRole role))
                    {
                        role.IsInvisibleFor.Add(pl);
                    }
                }
            }

            _schematicScp999.TryGetValue(player, out var scpObject);
            scpObject.Destroy();
            _schematicScp999.Remove(player);
        }
    }
}