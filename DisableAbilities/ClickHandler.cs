using DisableAbilities.Patches;
using HarmonyLib;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;

namespace DisableAbilities;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public int index;
    public AbilityGridEntry abilityGridEntry;

    public void OnPointerClick(PointerEventData eventData)
    {
        AbilityGrid abilityGrid = Traverse.Create(abilityGridEntry).Field<AbilityGrid>("abilityGrid").Value;
        AbilityGridEntry[] grid = Traverse.Create(abilityGrid).Field<AbilityGridEntry[]>("grid").Value;
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (index == 0)
            {
                if (Plugin.AbilityList.Count == 29)
                {
                    Plugin.AbilityList.Clear();
                }
                else
                {
                    Plugin.AbilityList.Clear();
                    for (int i = 1; i < grid.Length; i++)
                    {
                        Plugin.AbilityList.Add(i);
                    }
                }
                return;
            }
            if (Plugin.AbilityList.Contains(index))
            {
                Plugin.AbilityList.Remove(index);
                Plugin.Log.LogInfo($"Ability Enabled: {index}");
                abilityGridEntry.SetColor(abilityGrid.borderImage.color);
            }
            else
            {
                Plugin.AbilityList.Add(index);
                Plugin.Log.LogInfo($"Ability Disabled: {index}");
                abilityGridEntry.SetColor(Color.red);
            }
        } else if (eventData.button == PointerEventData.InputButton.Left)
        {
            abilityGrid.CloseGrid();
        }
    }
}