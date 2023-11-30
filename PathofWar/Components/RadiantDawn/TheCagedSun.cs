using Kingmaker.Blueprints;
using Kingmaker.Controllers;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.Visual.Particles;

namespace PathofWar.Components.RadiantDawn
{
    internal class TheCagedSun : UnitFactComponentDelegate, IGlobalRulebookHandler<RuleHealDamage>, IGlobalRulebookSubscriber
    {
        public BlueprintBuff buff;
        private int hpBeforeHeal;
        public void OnEventAboutToTrigger(RuleHealDamage evt)
        {
            if (evt.Target == Context.MaybeCaster)
            {
                evt.m_Modifiers.Add((0.5f, Fact));
                hpBeforeHeal = evt.Target.HPLeft;
                return;
            }

            if (evt.Initiator == Context.MaybeCaster && evt.Target != Context.MaybeCaster)
            {
                evt.Interrupted = true;
                var res = Context.TriggerRule(new RuleHealDamage(evt.Initiator, evt.Initiator, evt.HealFormula.ModifiedValue, 0));
                FxHelper.SpawnFxOnUnit(Context.SourceAbility.GetComponent<AbilitySpawnFx>().PrefabLink.Load(), evt.Initiator.View);
                return;
            }
        }

        public void OnEventDidTrigger(RuleHealDamage evt)
        {
            if (evt.Target == Context.MaybeCaster)
            {
                var tempHp = hpBeforeHeal + evt.Target.TemporaryHP + evt.ValueWithoutReduction - evt.Target.MaxHP;
                if (tempHp <= 0)
                    return;

                Context[AbilitySharedValue.Heal] = tempHp;
                evt.Target.AddBuff(buff, Context, 1.Minutes());
            }
        }
    }
}
