using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Utility;

namespace PathofWar.Components.Common
{
    internal class AbilityTargetChecks
    {
        public class AbilityTargetIncorporeal : BlueprintComponent, IAbilityTargetRestriction
        {
            public bool negate = false;
            public string GetAbilityTargetRestrictionUIText(UnitEntityData caster, TargetWrapper target)
            {
                return negate ? "Target is incorporeal." : "Target is not incorporeal.";
            }

            public bool IsTargetRestrictionPassed(UnitEntityData caster, TargetWrapper target)
            {
                return negate ^ target.Unit.Descriptor.HasFact(FeatureRefs.Incorporeal.Reference.Get());
            }
        }
    }
}
