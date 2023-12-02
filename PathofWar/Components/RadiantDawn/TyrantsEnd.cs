using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;

namespace PathofWar.Components.RadiantDawn
{
    internal class TyrantsEnd : UnitFactComponentDelegate, ITargetRulebookHandler<RuleDealDamage>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
            foreach (BaseDamage item in evt.DamageBundle)
            {
                item.Vulnerability = 1.5f;
            }
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
        }
    }
}
