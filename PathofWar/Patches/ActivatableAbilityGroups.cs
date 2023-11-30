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
            static bool GetGroupSize(ActivatableAbilityGroup group, ref int __result)
            {
                try
                {
                    if (group == MartialStance)
                    {
                        Logger.Verbose(() => "Returning group size for MartialStance");
                        __result = 1;
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