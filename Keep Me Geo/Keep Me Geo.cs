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
        internal static Settings globalSettings = new();
        public void OnLoadGlobal(Settings settings) => globalSettings = settings;
        public Settings OnSaveGlobal() => globalSettings;
        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? toggle) => SettingsMenu.GetMenu(modListMenu, toggle);
        new public string GetName() => "Keep Me Geo";
        public override string GetVersion() => "1.0.0.0";
        public override void Initialize() => ModHooks.AfterPlayerDeadHook += RecoverGeo;
        public void Unload() => ModHooks.AfterPlayerDeadHook -= RecoverGeo;

        private void RecoverGeo()
        {
            int recoveredGeo;
            int lostGeo;
            if (globalSettings.geoRecoveryPercentage == 0f)
            {
                recoveredGeo = 0;
                lostGeo = PlayerData.instance.geoPool;
            }
            if (globalSettings.geoRecoveryPercentage == 100f)
            {
                recoveredGeo = PlayerData.instance.geoPool;
                lostGeo = 0;
            } else
            {
                recoveredGeo = (int) Math.Round((double) PlayerData.instance.geoPool * (double) (globalSettings.geoRecoveryPercentage / 100f));
                lostGeo = PlayerData.instance.geoPool - recoveredGeo;
            }
            PlayerData.instance.AddGeo(recoveredGeo);
            PlayerData.instance.geoPool = lostGeo;
            if (!globalSettings.doSpawnShades)
                RecoverShade();
        }

        private void RecoverShade()
        {
            PlayerData.instance.shadeScene = "None";
            PlayMakerFSM.BroadcastEvent("HOLLOW SHADE KILLED");
            RemoveSoulLimit();
        }
    
        private void RemoveSoulLimit()
        {
            PlayerData.instance.EndSoulLimiter();
            foreach (PlayMakerFSM fsm in GameCameras.instance.hudCanvas.transform.Find("Soul Orb")
                .GetComponentsInChildren<PlayMakerFSM>())
                fsm.SendEvent("SOUL LIMITER DOWN");
        }
    }
}
