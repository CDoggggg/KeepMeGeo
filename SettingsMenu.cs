using Modding;
using Satchel.BetterMenus;

namespace KeepMeGeo;

internal static class SettingsMenu
{
    internal static Menu MenuRef = null;
    internal static MenuScreen GetMenu(MenuScreen lastMenu, ModToggleDelegates? toggleDelegates)
    {
        MenuRef ??= PrepareMenu((ModToggleDelegates) toggleDelegates);
        return MenuRef.GetMenuScreen(lastMenu);
    }

    internal static Menu PrepareMenu(ModToggleDelegates toggleDelegates) => new
    ("Keep Me Geo", new Element[]
        {
            Blueprints.CreateToggle(toggleDelegates, "Keep Me Geo Toggle", "Enable or disable this mod.", "Enabled", "Disabled"),
            Blueprints.HorizontalBoolOption("Spawn Shades", "Enable or disable the spawning of a shade on death.", (choose) => KeepMeGeo.globalSettings.doSpawnShades = choose, () => KeepMeGeo.globalSettings.doSpawnShades, Id: "DoSpawnShades")
        }
    );
}