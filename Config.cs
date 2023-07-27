using Exiled.API.Interfaces;
using System.ComponentModel;
using UnityEngine;

namespace PluginUtils.Plugins.SCP999
{
    public class Config : IConfig
    {
        [Description("Включить или Выключить плагин на SCP-999?")]
        public bool IsEnabled { get; set; } = true;
        [Description("Не используется.")]
        public bool Debug { get; set; } = false;
        [Description("Впишите своё название, чтобы использовать другую модельку.")]
        public string Model { get; set; } = "SCP999";
        [Description("Локальная позиция относительно игрока при спавне (лучше не трогать).")]
        public Vector3 Position { get; set; } = new Vector3(0, -0.4f, 0);
        [Description("Локальный поворот относительно игрока при спавне (лучше не трогать).")]
        public Vector3 Rotation { get; set; } = new Vector3(0, -90, 0);
        [Description("Кол. хп у SCP-999")]
        public int Health { get; set; } = 2000;
        [Description("Включить или Выключить god у SCP-999")]
        public bool IsGodModeEnabled { get; set; } = false;
        [Description("Дальность действия лечения")]
        public static int HealDistance { get; set; } = 5;
        [Description("Время отката обезбола")]
        public static float PainkillersDelay { get; set; } = 10f;

        [Description("Количество отхила обезбола")]
        public static float PainkillersAmount { get; set; } = 25f;
        [Description("Количество отхила аптечки")]
        public static float MedkitAmount { get; set; } = 50f;
        [Description("Количество отхила SCP-500")]
        public static float SCP500Amount { get; set; } = 100f;

        [Description("Время отката аптечки")]
        public static float MedkitDelay { get; set; } = 20f;
        [Description("Время отката SCP-500")]
        public static float SCP500Delay { get; set; } = 30f;
    }
}