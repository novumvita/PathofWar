using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;

namespace PathofWar.Components.VeiledMoon
{
    internal class EtherGate : UnitFactComponentDelegate, ITurnBasedModeHandler
    {
        public float distance = 0f;

        public void HandleRoundStarted(int round)
        {
        }

        public void HandleSurpriseRoundStarted()
        {
        }

        public void HandleTurnStarted(UnitEntityData unit)
        {
            if (unit == Owner)
                distance = 0f;
        }

        public void HandleUnitControlChanged(UnitEntityData unit)
        {
        }

        public void HandleUnitNotSurprised(UnitEntityData unit, RuleSkillCheck perceptionCheck)
        {
        }
    }
}
