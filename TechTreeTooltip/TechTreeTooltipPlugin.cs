using System;
using System.Collections.Generic;
using UnityEngine;

namespace TechTreeTooltip
{
    // Run once when we hit the main menu. Doing this per-frame in the VAB causes lag.
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class TechTreeTooltipPlugin : MonoBehaviour
    {
        // Static flag to prevent double-injection if the user reloads the database via Alt-F12
        private static bool _hasRun = false;

        private void Start()
        {
            if (_hasRun) return;

            Debug.Log("[TechTreeTooltip] Waking up and mapping tech nodes...");

            // Map of internal tech ID -> Readable Display Name
            var techNames = new Dictionary<string, string>();

            // Pull all tech tree configs. This handles Stock, RP-1, CTT, Skyhawk, etc.
            ConfigNode[] techTreeNodes = GameDatabase.Instance.GetConfigNodes("TechTree");

            foreach (ConfigNode tree in techTreeNodes)
            {
                foreach (ConfigNode rdNode in tree.GetNodes("RDNode"))
                {
                    string id = rdNode.GetValue("id");
                    string title = rdNode.GetValue("title");

                    // Sanity check because sometimes in-dev mods leave empty RDNodes
                    if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(title))
                    {
                        if (!techNames.ContainsKey(id))
                        {
                            techNames.Add(id, title);
                        }
                    }
                }
            }

            int partsModified = 0;

            // Loop through every loaded part and inject the tech node at the very top
            foreach (AvailablePart p in PartLoader.LoadedPartsList)
            {
                // Skip if the part is broken or has no tech requirement (like procedural parts sometimes do early on)
                if (p == null || string.IsNullOrEmpty(p.TechRequired)) continue;

                string techID = p.TechRequired;

                // Fallback to the raw ID if we somehow didn't find the display name
                string displayName = techNames.ContainsKey(techID) ? techNames[techID] : techID;

                // Format it for the top, with line breaks after to separate it from the main description
                string prependText = $"<b><color=#00FF99>Tech Node:</color></b> {displayName}\n\n";

                // Safety catch so we don't spam the description on soft reloads
                if (!p.description.Contains("<color=#00FF99>Tech Node:</color>"))
                {
                    // Stick it at the front instead of the back
                    p.description = prependText + p.description;
                    partsModified++;
                }
            }

            _hasRun = true;
            Debug.Log($"[TechTreeTooltip] Done! Injected tech info into the top of {partsModified} parts.");
        }
    }
}