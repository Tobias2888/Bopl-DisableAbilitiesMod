using System.Collections;
using HarmonyLib;
using UnityEngine;

namespace DisableAbilities.Patches;

[HarmonyPatch(typeof(AbilityGridEntry))]
class AbilityGridEntryPatch
{
    public static Color defaultColor;
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(AbilityGridEntry.Click))]
    static bool Prefix(AbilityGridEntry __instance)
    {
        __instance.Select();
        return false;
    }
    
    [HarmonyPostfix]
    [HarmonyPatch(nameof(AbilityGridEntry.Init))]
    static void Postfix(AbilityGridEntry __instance, int index)
    {
        ClickHandler clickHandler = __instance.gameObject.AddComponent<ClickHandler>();
        clickHandler.index = index;
        clickHandler.abilityGridEntry = __instance;

        defaultColor = __instance.GetColor();
    }
}