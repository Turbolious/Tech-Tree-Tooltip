TechTreeTooltip
I made this because I got tired of having to right-click or dig through the R&D screen in RP-1 just to figure out what tech node a part came from and how much it was going to cost to unlock.

This is a super simple, lightweight UI tweak for KSP 1.12.5. It slaps the required Tech Tree node and the Entry Cost right at the very top of the standard hover tooltip in the VAB/SPH.

<img width="352" height="318" alt="Tech node tooltip" src="https://github.com/user-attachments/assets/255e3ebd-88c5-4065-8788-601fcb4deed3" />


What it does:
Saves clicks: Puts the tech node name in bright green at the top of the normal part description.

Entry Costs: Shows the exact unlock cost in funds right next to it (e.g., (15000f)). If you play RP-1, you know how crucial this is for budgeting your unlock queue.

Works with everything: It dynamically pulls from the game database on startup. Whether you play Stock, RP-1, Community Tech Tree, or Skyhawk, it just figures out the correct names.

Zero lag: The script runs exactly once when you hit the Main Menu. There is no per-frame UI overhead or lag while you're actually building rockets.

Plays nice: It safely ignores broken mod parts and survives Alt-F12 database reloads without pasting the text twice.

Installation
Grab the latest .zip from the Releases tab.

Extract it and toss the GameData folder into your main Kerbal Space Program directory.

Changelog (v1.1)
Moved the tooltip info to the very top of the description so you don't have to scroll past miles of RealFuels engine data to see it.

Added the entry cost indicator.

Switched to a faster dictionary lookup under the hood to speed up main menu loading.

Added a hidden marker system so tooltips don't duplicate if you use the Alt-F12 menu to reload the game database.

License
MIT License. Do whatever you want with it!
