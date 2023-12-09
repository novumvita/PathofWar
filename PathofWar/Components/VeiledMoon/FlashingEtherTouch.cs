using BlueprintCore.Actions.Builder;
using BlueprintCore.Actions.Builder.ContextEx;
using BlueprintCore.Utils.Types;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic.Mechanics.Actions;

namespace PathofWar.Components.VeiledMoon
{
    internal class FlashingEtherTouch : ContextAction
    {
        public override string GetCaption()
        {
            return "Flashing ether touch";
        }

        public override void RunAction()
        {
            var caster = Context.MaybeCaster;
            var target = Context.MainTarget.Unit;

            var rule = new RuleSkillCheck(target, StatType.SaveReflex, caster.Stats.Dexterity.Bonus + 17);
            Context.TriggerRule(rule);

            EtherGateTeleport.TeleportWithFx(target, target.Position + (target.Position - caster.Position) * 5);

            if (!rule.Success)
            {
                ActionsBuilder.New().DealDamage(DamageTypes.Force(), ContextDice.Value(DiceType.D6, 10)).KnockdownTarget().Build().Run();
            }
        }
    }
}
