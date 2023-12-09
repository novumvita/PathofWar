using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker.Blueprints;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Visual.Particles;

namespace PathofWar.Components.RadiantDawn
{
    internal class NoblesseOblige : UnitFactComponentDelegate, IGlobalRulebookHandler<RuleDealDamage>, IGlobalRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            if (evt.Target.Descriptor.HPLeft > 0 || !evt.Target.IsAlly(Context.MaybeCaster))
                return;


            evt.Target.Damage += evt.Target.Descriptor.HPLeft - 1;
            FxHelper.SpawnFxOnUnit(AbilityRefs.Resurrection.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink.Load(), evt.Target.View);
            Fact.RunActionInContext(ActionsBuilder.New().DealDamage(DamageTypes.Energy(DamageEnergyType.Holy), ContextDice.Value(DiceType.D6, 4, Context.MaybeCaster.Stats.Charisma.Bonus)).Build(), evt.Initiator);

            FxHelper.SpawnFxOnUnit(AbilityRefs.SearingLight.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink.Load(), evt.Initiator.View);
            Fact.RunActionInContext(ActionsBuilder.New().HealTarget(ContextDice.Value(DiceType.D6, 4, Context.MaybeCaster.Stats.Charisma.Bonus)).ApplyBuffPermanent(BuffRefs.Fatigued.Reference.Get()).Build(), evt.Target);
        }
    }
}
