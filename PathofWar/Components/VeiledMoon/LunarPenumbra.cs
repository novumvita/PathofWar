using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Abilities;
using Kingmaker.UnitLogic;
using PathofWar.Utilities;
using System.Linq;

namespace PathofWar.Components.VeiledMoon
{
    internal class LunarPenumbra : UnitFactComponentDelegate, ITargetRulebookHandler<RuleCastSpell>
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("LunarPenumbra");
        public void OnEventAboutToTrigger(RuleCastSpell evt)
        {

            UnitEntityData initiator = evt.GetRuleTarget();
            UnitEntityData caster = evt.Initiator;

            if (caster.IsAlly(initiator))
                return;

            if (!initiator.Facts.List.Where(c => c.GetComponent<LunarPenumbra>()).Any())
                return;

            Logger.Log("Rolling for Lunar Penumbra");

            var initiator_rule = new RuleSkillCheck(initiator, StatType.SkillStealth, 0);
            Context.TriggerRule(initiator_rule);
            var enemy_rule = new RuleSkillCheck(caster, StatType.SkillPerception, initiator_rule.RollResult);
            Context.TriggerRule(enemy_rule);

            if (enemy_rule.Success)
                return;

            var initiator_position = initiator.Position;
            caster.CombatState.PreventAttacksOfOpporunityNextFrame = true;
            EtherGateTeleport.TeleportWithFx(initiator, caster.Position);
            caster.Position = initiator_position;

            var new_rule = new RuleCastSpell(evt.Spell, caster) { IsCutscene = true };
            Context.TriggerRule(new_rule);

            evt.ForceFail = true;

            Logger.Log("Activating for Lunar Penumbra");
        }

        public void OnEventDidTrigger(RuleCastSpell evt)
        {
        }
    }
}
