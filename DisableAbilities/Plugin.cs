using BepInEx;
using HarmonyLib;
using BepInEx.Logging;

namespace DisableAbilities;

[BepInPlugin("at.hofinga.bopl.disableabilities", "Disable Abilities", "1.0.0")]
public class Plugin : BaseUnityPlugin
{
    public static ManualLogSource Log;
    
    public static List<int> AbilityList = new List<int>();

    void Awake()
    {
        Log = Logger;
        Harmony harmony = new Harmony("at.hofinga.bopl.disableabilities");
        harmony.PatchAll();
        Log.LogInfo("Disable Abilities Mod has been loaded!");
    }
}

[HarmonyPatch(typeof(RandomAbility), nameof(RandomAbility.GetRandomAbilityPrefab))]
[HarmonyPatch(new Type[] { typeof(NamedSpriteList), typeof(NamedSpriteList) })]
class Patch_RandomAbility
{
    [HarmonyPostfix]
    static void Postfix(ref NamedSprite __result, NamedSpriteList abilityIcons)
    {
        NamedSpriteList obj = abilityIcons;
        int count = obj.sprites.Count;
        count -= 2;
        int index;
        do
        {
            index = Updater.RandomInt(0, count) + 2;
            Plugin.Log.LogInfo($"RandomAbility: index={index}");
            
            if (Plugin.AbilityList.Count >= count)
            {
                Plugin.Log.LogInfo($"No ability could be chosen");
                Plugin.Log.LogInfo(Plugin.AbilityList.Count.ToString());
                Plugin.Log.LogInfo(count);
                break;
            }
        } while (Plugin.AbilityList.Contains(index - 1));
        __result = obj.sprites[index];
    }
}



