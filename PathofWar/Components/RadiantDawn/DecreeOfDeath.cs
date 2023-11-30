using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;

namespace PathofWar.Components.RadiantDawn
{
    internal class DecreeOfDeath : UnitFactComponentDelegate, ITargetRulebookHandler<RuleDealDamage>, ITargetRulebookSubscriber
    {
        private RuleDealDamage new_evt = null;
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
            var initiator = Fact.MaybeContext.MaybeCaster;
            evt.Target.Buffs.RemoveFact(Fact);
            new_evt = new RuleDealDamage(initiator, evt.Target, evt.m_DamageBundle) { Half = true, Reason = Context };
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            if (new_evt != null)
            {
                Context.TriggerRule(new_evt);
                new_evt = null;
            }
        }
    }
}
