using Exiled.Events.EventArgs.Map;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Warhead;

namespace Scp999.EventHandlers
{
    public class PlayerHandler
    {
        private Config config = new Config();
        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                Plugin.Destroy(ev.Player, false);
            }
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                Plugin.Destroy(ev.Player, false);
            }
        }
        public void OnDroppingItem(DroppingItemEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }
        public void OnPickingUpItem(PickingUpItemEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }
        public void OnHandcuffing(HandcuffingEventArgs ev)
        {
            if (ev.Target.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }
        public void OnHurting(HurtingEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                if (ev.Attacker == null)
                {
                } 
                else if (ev.Attacker.IsScp)
                {
                    ev.IsAllowed = false;
                }
            }
        }
        public void OnDying(DyingEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                ev.Player.ClearInventory();
            }
        }
        public void OnActivatingGenerator(ActivatingGeneratorEventArgs ev)
        {
            if (!config.GeneratorActivating && ev.Player.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }
        public void OnStoppingGenerator(StoppingGeneratorEventArgs ev)
        {
            if (!config.GeneratorStopping && ev.Player.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }
        public void OnStartingWarhead(StartingEventArgs ev)
        {
            if (!config.ActivatingWarhead && ev.Player.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }
        public void OnStoppingWarhead(StoppingEventArgs ev)
        {
            if (!config.StoppingWarhead && ev.Player.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }

        public void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (ev.Target.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }
        public void OnSpawningRagdoll(SpawningRagdollEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }
        public void OnPlacingBlood(PlacingBloodEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                ev.IsAllowed = false;
            }
        }
    }
}
