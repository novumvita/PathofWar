using Kingmaker.Blueprints;
using Kingmaker.EntitySystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Utility;
using Kingmaker.Visual.Animation.Kingmaker;
using Kingmaker.Visual.Particles;
using PathofWar.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathofWar.Components.Common
{
    internal class AttackAnimation : BlueprintComponent, IAbilityCustomAnimation
    {
        public UnitAnimationAction GetAbilityAction(UnitEntityData caster)
        {
            return caster.Descriptor.Unit.View.AnimationManager.CreateHandle(UnitAnimationType.MainHandAttack).Action;
        }
    }

    public class AbilityDeliverAttackWithWeaponOnHit : AbilityCustomLogic
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("AbilityDeliverAttackWithWeaponOnHit");

        public bool force_flat_footed = false;
        public List<DamageDescription> bonus_dmg_desc = null;


        public BlueprintAbility ability_for_fx_self = null;
        public BlueprintAbility ability_for_fx_target = null;

        public override IEnumerator<AbilityDeliveryTarget> Deliver(AbilityExecutionContext context, TargetWrapper targetUnit)
        {
            var caster = context.Caster;
            var target = targetUnit.Unit;
            if (target == null)
            {
                yield break;
            }

            if (ability_for_fx_self != null)
            {
                FxHelper.SpawnFxOnUnit(ability_for_fx_self.GetComponent<AbilitySpawnFx>().PrefabLink.Load(), caster.View);
            }

            if (ability_for_fx_target != null)
            {
                FxHelper.SpawnFxOnUnit(ability_for_fx_target.GetComponent<AbilitySpawnFx>().PrefabLink.Load(), target.View);
            }

            Logger.Log("Ability on attack hit.");

            EventHandlers eventHandlers = null;
            if (bonus_dmg_desc != null)
            {
                Logger.Log("Adding event handler.");
                eventHandlers = new EventHandlers();
                eventHandlers.Add(new WeaponBonusDamageDiceHandler(caster, bonus_dmg_desc, context.SourceAbility.CreateFact(context.ParentContext, caster, TimeSpan.MinValue)));
            }

            var rule = new RuleAttackWithWeapon(caster, target, caster.GetFirstWeapon(), 0);
            rule.ForceFlatFooted = force_flat_footed;

            if (eventHandlers != null)
            {
                using (eventHandlers.Activate())
                {
                    context.TriggerRule(rule);
                }
            }
            else
            {
                context.TriggerRule(rule);
            }

            if (rule.AttackRoll.IsHit)
                yield return new AbilityDeliveryTarget(targetUnit);

            yield break;
        }
        public override void Cleanup(AbilityExecutionContext context)
        {
        }
    }
    internal class WeaponBonusDamageDiceHandler : IInitiatorRulebookHandler<RuleCalculateWeaponStats>
    {
        private readonly List<DamageDescription> dmg_desc;
        private readonly UnitFact m_Fact; 
        private readonly UnitEntityData m_Unit;

        public WeaponBonusDamageDiceHandler(UnitEntityData unit, List<DamageDescription> dmg, UnitFact fact)
        {
            m_Unit = unit;
            dmg_desc = dmg;
            m_Fact = fact;
        }

        public UnitEntityData GetSubscribingUnit()
        {
            return m_Unit;
        }

        public void OnEventAboutToTrigger(RuleCalculateWeaponStats evt)
        {
            foreach (DamageDescription dmg in dmg_desc)
                dmg.SourceFact = m_Fact;
            evt.DamageDescription.AddRange(dmg_desc);
        }

        public void OnEventDidTrigger(RuleCalculateWeaponStats evt)
        {
        }
    }

    public class EventHandlers : IDisposable
    {
        private readonly List<object> m_Handlers = new List<object>();

        public void Add(object handler) => this.m_Handlers.Add(handler);

        public EventHandlers Activate()
        {
            foreach (object handler in this.m_Handlers)
                EventBus.Subscribe(handler);
            return this;
        }

        public void Dispose()
        {
            foreach (object handler in this.m_Handlers)
                EventBus.Unsubscribe(handler);
        }
    }
}