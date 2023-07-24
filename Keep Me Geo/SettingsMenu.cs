using Modding;
using Satchel.BetterMenus;
using UnityEngine;

namespace KeepMeGeo;

internal static class SettingsMenu
{
    internal static Menu MenuRef = null;
    internal static MenuScreen GetMenu(MenuScreen lastMenu, ModToggleDelegates? toggleDelegates)
    {
        MenuRef ??= PrepareMenu(lastMenu, (ModToggleDelegates)toggleDelegates);
        MenuScreen modMenu = MenuRef.GetMenuScreen(lastMenu);
        return modMenu;
    }

    internal static Menu PrepareMenu(MenuScreen lastMenu, ModToggleDelegates toggleDelegates) => new
    ("Keep Me Geo", new Element[]
        {
            Blueprints.CreateToggle(toggleDelegates, "Keep Me Geo Toggle", "Enable or disable this mod.", "Enabled", "Disabled"),
            Blueprints.HorizontalBoolOption
            (
                "Spawn Shades", "Enable or disable the spawning of a shade on death.",
                (choice) =>
                {
                    KeepMeGeo.globalSettings.doSpawnShades = choice;
                    if (!choice)
                    {
                        KeepMeGeo.globalSettings.doRemoveSoulLimit = true;
                        MenuRef.Update();

                        if (KeepMeGeo.globalSettings.geoRecoveryPercentage < 100f)
                            MenuRef.AddConfirmDialog(MakeWarningMenu(lastMenu));
                    } else
                    {
                        MenuRef.returnScreen = lastMenu;
                        MenuRef.CancelAction = delegate
                        {
                            Utils.GoToMenuScreen(lastMenu);
                        };
                    }
                },
                () => KeepMeGeo.globalSettings.doSpawnShades,
                Id: "DoSpawnShades"
            ),
            Blueprints.HorizontalBoolOption
            (
                "Remove Soul Limit", "Enable or disable the removal of the soul limit on death.",
                (choice) =>
                {
                    KeepMeGeo.globalSettings.doRemoveSoulLimit = choice;
                    if (!choice && !KeepMeGeo.globalSettings.doSpawnShades)
                    {
                        KeepMeGeo.globalSettings.doSpawnShades = true;
                        MenuRef.Update();
                    }
                },
                () => KeepMeGeo.globalSettings.doRemoveSoulLimit,
                Id: "DoRemoveSoulLimit"
            ),
            Blueprints.FloatInputField
            (
                "Percent of Geo Kept",
                (percent) =>
                {
                    percent = (percent > 100f) ? 100f : percent;
                    percent = (percent < 0f) ? 0f : percent;
                    KeepMeGeo.globalSettings.geoRecoveryPercentage = percent;
                    MenuRef.Update();
                    if (percent < 100 && !KeepMeGeo.globalSettings.doSpawnShades)
                        MenuRef.AddConfirmDialog(MakeWarningMenu(lastMenu));
                    else
                    {
                        MenuRef.returnScreen = lastMenu;
                        MenuRef.CancelAction = delegate
                        {
                            Utils.GoToMenuScreen(lastMenu);
                        };
                    }
                },
                () => KeepMeGeo.globalSettings.geoRecoveryPercentage,
                _characterLimit: 5,
                Id: "GeoRecoveryPercentage"
            ),
            new TextPanel("Percent from 0% to 100% of your geo that will be kept after death.", fontSize: 24),
        }
    );

    private static Menu MakeWarningMenu(MenuScreen lastMenu)
    {
        return Blueprints.CreateDialogMenu
        (
            title: "Are you sure?",
            subTitle: "With your current settings, " + (100f - KeepMeGeo.globalSettings.geoRecoveryPercentage).ToString() + "% of your geo will be permanently lost when you die!",
            Options: new string[] { "Yes", "No" },
            OnButtonPress: (selection) =>
            {
                switch (selection)
                {
                    case "Yes":
                        Utils.GoToMenuScreen(lastMenu);
                        break;
                    case "No":
                        Utils.GoToMenuScreen(MenuRef.menuScreen);
                        break;
                }
            }
        );
    }
}