using System.Collections.Generic;

using Exiled.API.Enums;
using Exiled.API.Interfaces;

using UnityEngine;

namespace Scp999
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; } = true;
        public RoomType Room { get; set; } = RoomType.Lcz330;
        public ushort Health { get; set; } = 5000;
        public string SchematicName { get; set; } = "SCP-999";
        public Vector3 PositionOffset { get; set; } = new Vector3(0, 0, 0);
        public Vector3 RotationOffset { get; set; } = new Vector3(0, 0, 0);
        public Vector3 Scale { get; set; } = new Vector3(0.7f, 0.7f, 0.7f);
        public bool IsBodyEnabled { get; set; } = true;
        public bool GeneratorActivating { get; set; } = false;
        public bool GeneratorStopping { get; set; } = false;
        public bool ActivatingWarhead { get; set; } = false;
        public bool StoppingWarhead { get; set; } = false;
        public bool RemoteAdminDisabled { get; set; } = true;
        public float HealDistance { get; set; } = 5f;
        public float HealDelay { get; set; } = 15f;
        public float EffectDelay { get; set; } = 20f;
        public int HealAmount { get; set; } = 45;
        public int SpawnsCount { get; set; } = 2;
        public float EffectDistance { get; set; } = 5f;
        public float EffectDuration { get; set; } = 25;
        public byte EffectIntensity { get; set; } = 20;
        public string AdrenalineItemHint { get; set; } = "<color=red>Адреналин <color=yellow>выдает игрокам в пределе %distance% метров эффект <color=red>скорости</color>";
        public string PainkillersItemHint { get; set; } = "<color=red>Обезболивающее <color=yellow>регенерирует игроков в пределе %distance% метров <color=red>на %amount% ХП</color>";
        public string SCP500ItemHint { get; set; } = "";
        public string AdrenalineUsedHint { get; set; } = "<color=yellow>Вы дали игрокам эффект <color=red>скорости</color>";
        public string PainkillersUsedHint { get; set; } = "<color=yellow>Вы дали игрокам эффект <color=red>регенерации</color>";
        public string PlayerHealed { get; set; } = "";
        public string PlayerEffect { get; set; } = "";
        public string Broadcast { get; set; } = "<color=yellow>Вы стали <color=orange>SCP-999</color>\n<color=yellow>. Используйте предметы в инвентаре чтобы помогать людям</color>";

        public List<string> DonateList = new List<string>
        {
            "donate1",
            "donate2",
            "donate3",
            "donate4"
        };
    }
}
