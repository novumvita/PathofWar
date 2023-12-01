using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;

namespace PathofWar.Components.RadiantDawn
{
    internal class DecreeOfMercy : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleDealDamage>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
            evt.MinHPAfterDamage = 1;
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            evt.Initiator.Buffs.RemoveFact(Fact);
        }
    }
}
