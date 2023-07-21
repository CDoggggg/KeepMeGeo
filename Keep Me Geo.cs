using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using HutongGames.PlayMaker;
using JetBrains.Annotations;
using Modding;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace KeepMeGeo
{
    public class KeepMeGeo : Mod, ICustomMenuMod, ITogglableMod, IGlobalSettings<Settings>
    {
        public bool ToggleButtonInsideMenu { get; } = true;

        public static Settings globalSettings = new();

        public void OnLoadGlobal(Settings settings) => globalSettings = settings;

        public Settings OnSaveGlobal()
        {
            return globalSettings;
        }

        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggle) => SettingsMenu.GetMenu(modListMenu, toggle);

        new public string GetName() => "Keep Me Geo";
        public override string GetVersion() => "3.2.1";
        public override void Initialize()
        {
            ModHooks.AfterPlayerDeadHook += RecoverGeo;
        }

        public void RecoverGeo()
        {
            PlayerData.instance.AddGeo(PlayerData.instance.geoPool);
            PlayerData.instance.geoPool = 0;
            if (!globalSettings.doSpawnShades)
            {
                RecoverShade();
            }
        }

        public void RecoverShade()
        {
            PlayerData.instance.EndSoulLimiter();
            PlayerData.instance.shadeScene = "None";
            foreach (PlayMakerFSM fsm in GameCameras.instance.hudCanvas.transform.Find("Soul Orb")
                .GetComponentsInChildren<PlayMakerFSM>())
            {
                fsm.SendEvent("SOUL LIMITER DOWN");
            }

            PlayMakerFSM.BroadcastEvent("HOLLOW SHADE KILLED");
        }
    
        public void Unload()
        {
            ModHooks.AfterPlayerDeadHook -= RecoverGeo;
        }
    }
}
