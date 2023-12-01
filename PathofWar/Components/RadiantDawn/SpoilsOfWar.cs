using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.Visual.Particles;
using PathofWar.Utilities;
using static Kingmaker.UI.CanvasScalerWorkaround;

namespace PathofWar.Components.RadiantDawn
{
    internal class SpoilsOfWar : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleAttackRoll>, IInitiatorRulebookSubscriber, ITurnBasedModeHandler
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("SpoilsOfWar");
        private bool flag = false;

        public void HandleRoundStarted(int round)
        {
            flag = false;
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

        public void OnEventAboutToTrigger(RuleAttackRoll evt)
        {
        }

        public void OnEventDidTrigger(RuleAttackRoll evt)
        {
            if (!evt.IsHit || flag)
                return;

            var initiator = Fact.MaybeContext.MaybeCaster;
            var healingAmount = 3 + initiator.Stats.Charisma.Bonus * (initiator.Progression.CharacterLevel >= 10 ? 2 : initiator.Progression.CharacterLevel >= 5 ? 1 : 0);

            var res = Context.TriggerRule(new RuleHealDamage(initiator, evt.Initiator, healingAmount) { Reason = Context });
            FxHelper.SpawnFxOnUnit(AbilityRefs.CureLightWounds.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink.Load(), evt.Initiator.View);

            flag = true;
            Logger.Log("flag to true for: " + evt.Initiator);
        }
    }
}
