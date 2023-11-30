using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Visual.Particles;

namespace PathofWar.Components.RadiantDawn
{
    internal class SpoilsOfWar : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackRoll>, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
            if (!evt.IsHit)
                return;

            var initiator = Fact.MaybeContext.MaybeCaster;
            var healingAmount = 3 + initiator.Stats.Charisma.Bonus * (initiator.Progression.CharacterLevel >= 10 ? 2 : initiator.Progression.CharacterLevel >= 5 ? 1 : 0);

            var res = Context.TriggerRule(new RuleHealDamage(initiator, evt.Initiator, healingAmount) { Reason = Context });
            FxHelper.SpawnFxOnUnit(AbilityRefs.CureLightWounds.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink.Load(), evt.Initiator.View);

            evt.Initiator.RemoveFact(Fact);
        }
    }
}
