using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;

namespace PathofWar.Components.RadiantDawn
{
    internal class DecreeOfDeath : UnitFactComponentDelegate, ITargetRulebookHandler<RuleDealDamage>, ITargetRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            var initiator = Fact.MaybeContext.MaybeCaster;
            evt.Target.Buffs.RemoveFact(Fact);
            var new_evt = new RuleDealDamage(initiator, evt.Target, evt.m_DamageBundle) { Half = true, Reason = Context };
            Context.TriggerRule(new_evt);
        }
    }
}
