using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

using MEC;
using UnityEngine;

namespace Scp999.EventHandlers
{
    public class AbilityHandler
    {
        private Config config = new Config();
        public void OnChangeItem(ChangingItemEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                switch (ev.NewItem.Type)
                {
                    case ItemType.Adrenaline:
                        {
                            ev.Player.ShowHint(config.AdrenalineItemHint.Replace("%distance%", config.EffectDistance.ToString()), 4);
                            break;
                        }
                    case ItemType.Painkillers:
                        {
                            string text = config.PainkillersItemHint.Replace("%distance%", config.HealDistance.ToString());
                            text = text.Replace("%amount%", config.HealAmount.ToString());
                            ev.Player.ShowHint(text, 7);
                            break;
                        }
                    case ItemType.SCP500:
                        {
                            ev.Player.ShowHint("123");
                            break;
                        }
                }
            }
        }

        public void OnUsedItem(UsedItemEventArgs ev)
        {
            if (ev.Player.CustomInfo == Plugin.NAME)
            {
                switch (ev.Item.Type)
                {
                    case ItemType.Adrenaline:
                        {
                            Effect(ev.Player);
                            ev.Player.ShowHint(config.AdrenalineUsedHint, 5);
                            Timing.CallDelayed(config.EffectDelay, () =>
                            {
                                if (ev.Player.IsAlive)
                                {
                                    ev.Player.AddItem(ItemType.Adrenaline);
                                }
                            });
                            break;
                        }
                    case ItemType.Painkillers:
                        {
                            Heal(ev.Player);
                            ev.Player.ShowHint(config.PainkillersUsedHint, 5);
                            Timing.CallDelayed(config.HealDelay, () =>
                            {
                                if (ev.Player.IsAlive)
                                {
                                    ev.Player.AddItem(ItemType.Painkillers);
                                }
                            });
                            break;
                        }
                }
            }
        }

        public void Heal(Player Scp999)
        {
            foreach (Player ply in Player.List)
            {
                if (ply.IsHuman && (Vector3.Distance(Scp999.Position, ply.Position)) < config.HealDistance)
                {
                    ply.ShowHint(config.PlayerHealed, 4);
                    for (int i = config.HealAmount; i >= 0; i--)
                    {
                        ply.Heal(1);
                        Timing.WaitForSeconds(0.5f);
                    }
                }
            }
        }

        public void Effect(Player Scp999)
        {
            foreach (Player ply in Player.List)
            {
                if (ply.IsHuman && (Vector3.Distance(Scp999.Position, ply.Position)) < config.EffectDistance)
                {
                    ply.ShowHint(config.PlayerEffect, 4);
                    ply.EnableEffect(EffectType.Invigorated, config.EffectDuration);
                    ply.ChangeEffectIntensity(EffectType.MovementBoost, config.EffectIntensity, config.EffectDuration);
                }
            }
        }
    }
}
