using Exiled.API.Extensions;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using HarmonyLib;
using MapEditorReborn.API.Features;
using MapEditorReborn.API.Features.Objects;
using PlayerRoles;
using Scp999.EventHandlers;
using System;
using System.Collections.Generic;
using UnityEngine;
using _Map = Exiled.Events.Handlers.Map;
using _Player = Exiled.Events.Handlers.Player;
using _Scp096 = Exiled.Events.Handlers.Scp096;
using _Warhead = Exiled.Events.Handlers.Warhead;

namespace Scp999
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "SCP-999";
        public override string Author => "Num1ock";
        public override Version Version => new Version(1, 0, 0);
        public override string Prefix => "scp999";

        public const string NAME = "SCP-999";

        private static SchematicObject schematic;

        private Harmony HarmonyPatch;
        private PlayerHandler playerHandler;
        private AbilityHandler abilityHandler;
        public override void OnEnabled()
        {
            playerHandler = new PlayerHandler();
            abilityHandler = new AbilityHandler();

            HarmonyPatch = new Harmony("Scp999");
            HarmonyPatch.PatchAll();

            _Player.Verified += OnVerified;

            _Player.SpawningRagdoll += playerHandler.OnSpawningRagdoll;
            _Player.Died += playerHandler.OnDied;
            _Player.DroppingItem += playerHandler.OnDroppingItem;
            _Player.PickingUpItem += playerHandler.OnPickingUpItem;
            _Player.Handcuffing += playerHandler.OnHandcuffing;
            _Player.Hurting += playerHandler.OnHurting;
            _Player.ActivatingGenerator += playerHandler.OnActivatingGenerator;
            _Player.StoppingGenerator += playerHandler.OnStoppingGenerator;
            _Player.Dying += playerHandler.OnDying;
            _Player.ChangingRole += playerHandler.OnChangingRole;

            _Player.ChangingItem += abilityHandler.OnChangeItem;
            _Player.UsedItem += abilityHandler.OnUsedItem;

            _Warhead.Starting += playerHandler.OnStartingWarhead;
            _Warhead.Stopping += playerHandler.OnStoppingWarhead;

            _Scp096.AddingTarget += playerHandler.OnAddingTarget;

            _Map.PlacingBlood += playerHandler.OnPlacingBlood;

            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            _Player.Verified -= OnVerified;

            _Player.SpawningRagdoll -= playerHandler.OnSpawningRagdoll;
            _Player.Died -= playerHandler.OnDied;
            _Player.DroppingItem -= playerHandler.OnDroppingItem;
            _Player.PickingUpItem -= playerHandler.OnPickingUpItem;
            _Player.Handcuffing -= playerHandler.OnHandcuffing;
            _Player.Hurting -= playerHandler.OnHurting;
            _Player.ActivatingGenerator -= playerHandler.OnActivatingGenerator;
            _Player.StoppingGenerator -= playerHandler.OnStoppingGenerator;
            _Player.Dying -= playerHandler.OnDying;
            _Player.ChangingRole -= playerHandler.OnChangingRole;

            _Player.ChangingItem -= abilityHandler.OnChangeItem;
            _Player.UsedItem -= abilityHandler.OnUsedItem;

            _Warhead.Starting -= playerHandler.OnStartingWarhead;
            _Warhead.Stopping -= playerHandler.OnStoppingWarhead;

            _Scp096.AddingTarget -= playerHandler.OnAddingTarget;

            _Map.PlacingBlood -= playerHandler.OnPlacingBlood;

            abilityHandler = null;
            playerHandler = null;
            HarmonyPatch.UnpatchAll();
            base.OnDisabled();
        }

        public static void Spawn(Player ply, bool Spawned)
        {
            Config config = new Config();
            ply.Role.Set(RoleTypeId.Tutorial);
            if (Spawned)
            {
                ply.Position = Room.Get(config.Room).Position;
            }
            ply.Health = config.Health;
            ply.ResetInventory(new List<ItemType>
            {
                ItemType.Adrenaline,
                ItemType.SCP500,
                ItemType.Painkillers
            });
            MirrorExtensions.ChangeAppearance(ply, RoleTypeId.Tutorial, false);
            ply.CustomInfo = NAME;
            schematic = ObjectSpawner.SpawnSchematic("SCP999", ply.Position + config.PositionOffset,
                Quaternion.Euler(ply.Rotation + config.RotationOffset), config.Scale);
            schematic.transform.parent = ply.Transform;
            ply.Broadcast(7, config.Broadcast);
        }
        public static void Destroy(Player ply, bool ForceSpectator)
        {
            if (ForceSpectator)
            {
                ply.Role.Set(RoleTypeId.Spectator);
            }
            ply.ClearInventory();
            ply.CustomInfo = String.Empty;
            schematic.Destroy();
        }
        public void OnVerified(VerifiedEventArgs ev) // убрать после тестов
        {
            Spawn(ev.Player, false);
        }

    }
}
