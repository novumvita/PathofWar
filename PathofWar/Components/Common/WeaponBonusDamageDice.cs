using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;

namespace PathofWar.Components.Common
{
    internal class WeaponBonusDamageDice : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateWeaponStats>
    {
        public DamageDescription dmg_desc;
        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
            dmg_desc.SourceFact = Fact;
            evt.DamageDescription.Add(dmg_desc);
        }
    }
}
