using HarmonyLib;
using UnityEngine;

namespace DisableAbilities.Patches;

[HarmonyPatch(typeof(AbilityGrid))]
public class AbilityGridPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("Update")]
    public static void Postfix(AbilityGrid __instance)
    {
        AbilityGridEntry[] grid = Traverse.Create(__instance).Field<AbilityGridEntry[]>("grid").Value;
        
        for (int i = 1; i < grid.Length; i++)
        {
            if (Plugin.AbilityList.Contains(i))
            {
                grid[i].SetColor(Color.red);
            }
            else
            {
                grid[i].SetColor(__instance.borderImage.color);
            }
        }
    }
}