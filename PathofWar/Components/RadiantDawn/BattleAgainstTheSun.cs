using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Blueprints.References;
using BlueprintCore.Utils.Types;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums.Damage;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Visual.Particles;

namespace PathofWar.Components.RadiantDawn
{
    internal class BattleAgainstTheSun : UnitFactComponentDelegate, ITurnBasedModeHandler
    {
        public void HandleRoundStarted(int round)
        {
            var initiator = Context.MaybeCaster;

            var heal_action = ActionsBuilder.New().HealTarget(ContextDice.Value(DiceType.Zero, 0, initiator.Progression.CharacterLevel + round)).Build();
            var dmg_action = ActionsBuilder.New().SavingThrow(SavingThrowType.Reflex, customDC: 16 + initiator.Stats.Charisma.Bonus).DealDamage(DamageTypes.Energy(DamageEnergyType.Holy), ContextDice.Value(DiceType.Zero, 0, initiator.Progression.CharacterLevel + round), halfIfSaved: true).Build();

            FxHelper.SpawnFxOnUnit(AbilityRefs.AngelMinorAbility.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink.Load(), initiator.View);

            foreach (UnitEntityData unit in Game.Instance.TurnBasedCombatController.SortedUnits)
            {
                if (initiator.IsAlly(unit))
                    Fact.RunActionInContext(heal_action, unit);
                else
                    Fact.RunActionInContext(dmg_action, unit);
            }
        }

        public void HandleSurpriseRoundStarted()
        {
        }

        public void HandleTurnStarted(UnitEntityData unit)
        {
        }

        public void HandleUnitControlChanged(UnitEntityData unit)
        {
        }

        public void HandleUnitNotSurprised(UnitEntityData unit, RuleSkillCheck perceptionCheck)
        {
        }
    }
}
