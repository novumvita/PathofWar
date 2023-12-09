using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.FactLogic;
using System.Linq;

namespace PathofWar.Common
{
    internal class FeatureChecker
    {
        internal static bool IsFeatureImmunityOrResistance(BlueprintFeature feature)
        {
            #region checkGetComponents
            if (feature.GetComponents<BuffDescriptorImmunity>().Any())
                return true;
            if (feature.GetComponents<AddPhysicalImmunity>().Any())
                return true;
            if (feature.GetComponents<AddSpellImmunity>().Any())
                return true;
            if (feature.GetComponents<ManeuverImmunity>().Any())
                return true;
            if (feature.GetComponents<CompleteDamageImmunity>().Any())
                return true;
            if (feature.GetComponents<AddConditionImmunity>().Any())
                return true;
            if (feature.GetComponents<AddEnergyDamageImmunity>().Any())
                return true;
            if (feature.GetComponents<AddEnergyImmunity>().Any())
                return true;
            if (feature.GetComponents<AddImmunityToAbilityScoreDamage>().Any())
                return true;
            if (feature.GetComponents<AddImmunityToCriticalHits>().Any())
                return true;
            if (feature.GetComponents<AddImmunityToPrecisionDamage>().Any())
                return true;
            if (feature.GetComponents<SpellImmunityToSpellDescriptor>().Any())
                return true;
            if (feature.GetComponents<GhostCriticalAndPrecisionImmunity>().Any())
                return true;
            if (feature.GetComponents<AddImmunityToEnergyDrain>().Any())
                return true;
            if (feature.GetComponents<AddSpellResistance>().Any())
                return true;
            if (feature.GetComponents<SwarmDamageResistance>().Any())
                return true;
            if (feature.GetComponents<SpellResistanceAgainstSpellSchool>().Any())
                return true;
            if (feature.GetComponents<SpellResistanceAgainstSpellDescriptor>().Any())
                return true;
            if (feature.GetComponents<SpellResistanceAgainstAlignment>().Any())
                return true;
            if (feature.GetComponents<AddDamageResistanceBase>().Any())
                return true;
            #endregion

            return false;
        }
    }
}
