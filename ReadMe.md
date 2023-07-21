# Keep Me Geo

A Hollow Knight mod which makes it so that when you die, you keep your geo.  
There are also options for turning off the spawning of shades when you die too.

## How it Works

There is a player data property called "geoPool", and one called "geo". "geoPool" stores the amount of geo dropped when you last died, and "geo" stores the current amount of geo you have. This mod hooks onto a script provided by the Hollow Knight modding API which runs after you die. It then replaces your current geo with the value of "geoPool", and sets "geoPool" to zero.

## Available Options

#### The following settings can be changed:
- **Mod Toggle**  
  Enable or disable the mod.
- **Spawn Shades**  
  Toggle whether or not shades spawn when you die.
- **Percent of Geo Kept**  
  Specify the percent of your geo that you want to keep when you die (the rest stays on your shade).

#### The following safeguards are in place:
- **Permanent Geo Loss**  
  The mod does not allow you to set the percent of geo kept after death to anything other than 100% if shade spawns are off, in order to prevent accidental permanent loss of geo.
- **Making Geo out of Thin Air**  
  You cannot set the percent of geo kept after death to anything greater than 100%, and so you'll always have *at most* 100% of your geo when you die.
- **Going into Debt**  
  The percent of geo kept after death cannot be set below 0%, which would cause you to go into geo debt after you die. **Millibelle the Banker is gonna take your house and your dogs otherwise...**

## Dependencies
- [**Satchel**](https://github.com/TheMulhima/Satchel)

## Credits
- [**TheMulhima on GitHub**](https://github.com/TheMulhima)  
  I borrowed the code from [DebugMod](https://github.com/TheMulhima/HollowKnight.DebugMod) which recovers your shade, and used it to do the same in this mod.  
  I also extensively relied on this [fantastic documentation](https://prashantmohta.github.io/ModdingDocs/) for Hollow Knight modding beginners, which I believe TheMulhima also created.
- [**ygsbzr on Github**](https://github.com/ygsbzr)  
  I got my foothold in creating menus with Satchel by borrowing the code used for the menu in the [EnemyHPBars](https://github.com/ygsbzr/Enemy-HP-Bars) mod.
- [**W3Schools**](https://www.w3schools.com/cs/index.php)  
  I'm pretty sure anyone who's ever coded anything in the modern age has used W3Schools.
