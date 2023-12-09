using Kingmaker;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Commands;
using Kingmaker.UnitLogic.Commands.Base;
using PathofWar.Utilities;
using UnityEngine;

namespace PathofWar.Components.VeiledMoon
{
    internal class EclipsingMoon : UnitFactComponentDelegate, IUnitRunCommandHandler, IUnitCommandEndHandler, IInitiatorRulebookHandler<RuleAttackWithWeapon>
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("EclipsingMoon");

        public int count;
        public float distance;
        public BlueprintAbility ability;

        public void HandleUnitCommandDidEnd(UnitCommand cmd)
        {
            if (cmd.Executor != Owner)
                return;

            var aby = cmd as UnitUseAbility;
            if (aby != null && aby.Ability.Blueprint == ability)
            {
                var count_rule = new RuleCalculateAttacksCount(Owner);
                Context.TriggerRule(count_rule);
                count = count_rule.Result.PrimaryHand.MainAttacks + count_rule.Result.PrimaryHand.PenalizedAttacks + count_rule.Result.SecondaryHand.MainAttacks + count_rule.Result.SecondaryHand.PenalizedAttacks;
                Logger.Log("count initialized to: " + count);
                distance = Owner.Stats.Speed.ModifiedValue;
                Logger.Log("Distance initialized to: " + distance);
                Game.Instance.TurnBasedCombatController.CurrentTurn.SetMetersMovedByFiveFootStep(Owner, Game.Instance.TurnBasedCombatController.CurrentTurn.GetRemainingFiveFootStepRange(Owner));
                return;
            }
            if (count > 0)
            {
                if (cmd.TargetUnit.HPLeft < 0)
                    count++;
                count--;
                return;
            }
            Owner.RemoveFact(Fact);
        }

        public void HandleUnitRunCommand(UnitCommand cmd)
        {
            if (cmd.Executor != Owner)
                return;

            var atk = cmd as UnitAttack;
            if (count > 0 && atk != null)
            {
                Logger.Log("count is: " + count);
                atk.IgnoreCooldown();
                atk.IsSingleAttack = true;
            }
        }

        public void OnEventAboutToTrigger(RuleAttackWithWeapon evt)
        {
            evt.ForceFlatFooted = true;
        }

        public void OnEventDidTrigger(RuleAttackWithWeapon evt)
        {
        }
    }
}