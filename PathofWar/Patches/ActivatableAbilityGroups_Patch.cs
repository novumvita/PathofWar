using HarmonyLib;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Parts;
using PathofWar.Utilities;
using System;

namespace PathofWar.Patches
{
    internal class ExpandedActivatableAbilityGroup
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger(nameof(ExpandedActivatableAbilityGroup));

        internal const ActivatableAbilityGroup MartialStance = (ActivatableAbilityGroup)1450;

        [HarmonyPatch(typeof(UnitPartActivatableAbility))]
        static class UnitPartActivatableAbility_Patch
        {
            [HarmonyPatch(nameof(UnitPartActivatableAbility.GetGroupSize)), HarmonyPrefix]
            static bool GetGroupSize(UnitPartActivatableAbility __instance, ActivatableAbilityGroup group, ref int __result)
            {
                try
                {
                    if (group == MartialStance)
                    {
                        Logger.Log("Returning group size for MartialStance");
                        __result = 2;
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Logger.LogException("UnitPartActivatableAbility_Patch.GetGroupSize", e);
                }
                return true;
            }
        }
    }
}