using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;

namespace PathofWar.Components.RadiantDawn
{
    internal class DecreeOfDeath : UnitFactComponentDelegate, ITargetRulebookHandler<RuleDealDamage>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
            foreach (BaseDamage item in evt.DamageBundle)
            {
                item.BonusPercent = 50;
            }
            evt.Target.Buffs.RemoveFact(Fact);
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
        }
    }
}
