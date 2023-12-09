using BlueprintCore.Actions.Builder;
using Kingmaker;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using PathofWar.Common;
using PathofWar.Utilities;

namespace PathofWar.Components.RadiantDawn
{
    internal class PushTheAdvantage : UnitFactComponentDelegate, IGlobalRulebookHandler<RuleDealDamage>, ITurnBasedModeHandler
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("PushTheAdvantage");

        private UnitEntityData new_unit = null;

        public void HandleRoundStarted(int round)
        {
        }

        public void HandleSurpriseRoundStarted()
        {
        }

        public void HandleTurnStarted(UnitEntityData unit)
        {
            var controller = Game.Instance.TurnBasedCombatController;

            if (new_unit != null)
            {
                if (new_unit == unit)
                {
                    Logger.Log("Interrupting unit's turn starting");
                    controller.CurrentTurn.m_RiderActionState = ActionStates.GetStandardActionState(controller.CurrentTurn, new_unit);
                    new_unit = null;
                    return;
                }
                Game.Instance.TurnBasedCombatController.StartTurn(new_unit);
            }
        }

        public void HandleUnitControlChanged(UnitEntityData unit)
        {
        }

        public void HandleUnitNotSurprised(UnitEntityData unit, RuleSkillCheck perceptionCheck)
        {
        }

        public void OnEventAboutToTrigger(RuleDealDamage evt)
        {
        }

        public void OnEventDidTrigger(RuleDealDamage evt)
        {
            if (evt.Target.Descriptor.HPLeft > 0 || evt.Target.IsAlly(Fact.MaybeContext.MaybeOwner))
                return;

            Fact.RunActionInContext(ActionsBuilder.New().Add<RadiantHeal>().Build());

            new_unit = evt.Initiator;
            Logger.Log("Interrupting unit is: " + new_unit);
        }
    }
}
