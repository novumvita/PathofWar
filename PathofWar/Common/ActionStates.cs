using Kingmaker.EntitySystem.Entities;
using Kingmaker.TurnBasedMode;
using Kingmaker.TurnBasedMode.Controllers;
using Kingmaker.UnitLogic.Commands.Base;
using TurnBased.Controllers;
using static Kingmaker.TurnBasedMode.ActionsState;

namespace PathofWar.Common
{
    internal class ActionStates
    {
        public static ActionsState GetStandardActionState(TurnController turn_controller, UnitEntityData unit)
        {
            int std_mvmt = (turn_controller.Rider.IsSurprising() ? turn_controller.GetRemainingActionMovementRangeFeet(unit, UnitCommand.CommandType.Move).Value : turn_controller.GetRemainingActionMovementRangeFeet(unit, UnitCommand.CommandType.Standard).Value);

            var actions = new Actions()
            {
                FiveFootStep = new CombatAction(CombatAction.ActivityState.Unable, null, null, TurnController.FiveFootStep.Value),
                Free = new CombatAction(null, null, CombatAction.ActivityState.Available),
                Swift = new CombatAction(null, null, CombatAction.ActivityState.Unable),
                Move = new CombatAction(CombatAction.ActivityState.Unable, CombatAction.ActivityState.Unable, CombatAction.ActivityState.Unable, TurnController.FiveFootStep.Value),
                Standard = new CombatAction(CombatAction.ActivityState.Available, CombatAction.ActivityState.Available, CombatAction.ActivityState.Available, std_mvmt)
            };
            return new ActionsState(actions, false);
        }
    }
}
