# Keep Me Geo

A Hollow Knight mod which makes it so that when you die, you keep your geo.  
There are also options for turning off the spawning of shades when you die too.

## How it Works

There is a player data property called "geoPool", and one called "geo". "geoPool" stores the amount of geo dropped when you last died, and "geo" stores the current amount of geo you have. This mod hooks onto a script provided by the Hollow Knight modding API which runs after you die. It then replaces your current geo with the value of "geoPool", and sets "geoPool" to zero.

## Available Options

The following settings can be changed:
- **Mod toggle**  
  Turn the mod on and off.
- **Spawn shades**  
  Toggle whether or not shades spawn when you die.
