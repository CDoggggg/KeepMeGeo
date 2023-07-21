using Modding;
using Satchel.BetterMenus;

namespace KeepMeGeo;

internal static class SettingsMenu
{
    internal static Menu MenuRef = null;
    internal static MenuScreen GetMenu(MenuScreen lastMenu, ModToggleDelegates? toggleDelegates)
    {
        MenuRef ??= PrepareMenu((ModToggleDelegates)toggleDelegates);
        return MenuRef.GetMenuScreen(lastMenu);
    }

    internal static Menu PrepareMenu(ModToggleDelegates toggleDelegates) => new
    ("Keep Me Geo", new Element[]
        {
            Blueprints.CreateToggle(toggleDelegates, "Keep Me Geo Toggle", "Enable or disable this mod.", "Enabled", "Disabled"),
            Blueprints.HorizontalBoolOption
            (
                "Spawn Shades", "Enable or disable the spawning of a shade on death.",
                (choice) =>
                {
                    KeepMeGeo.globalSettings.doSpawnShades = choice;
                    if (choice == false)
                    {
                        KeepMeGeo.globalSettings.geoRecoveryPercentage = 100f;
                        MenuRef.Update();
                    }
                },
                () => KeepMeGeo.globalSettings.doSpawnShades,
                Id: "DoSpawnShades"
            ),
            Blueprints.FloatInputField
            (
                "Percent of Geo Kept",
                (percent) =>
                {
                    percent = (percent > 100f || KeepMeGeo.globalSettings.doSpawnShades == false) ? 100f : percent;
                    percent = (percent < 0f) ? 0f : percent;
                    KeepMeGeo.globalSettings.geoRecoveryPercentage = percent;
                    MenuRef.Update();
                },
                () => KeepMeGeo.globalSettings.geoRecoveryPercentage,
                _characterLimit: 5,
                Id: "GeoRecoveryPercentage"
            ),
            new TextPanel("Percent from 0% to 100% of your geo that will be kept after death.", fontSize: 24),
        }
    );
}