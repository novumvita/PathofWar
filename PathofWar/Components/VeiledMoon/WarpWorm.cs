using BlueprintCore.Utils.Types;
using Kingmaker;
using Kingmaker.Controllers;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.RuleSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic.Mechanics.Actions;
using System.Collections.Generic;
using System.Linq;

namespace PathofWar.Components.VeiledMoon
{
    internal class WarpWorm : ContextAction
    {
        internal int count = 3;
        public override string GetCaption()
        {
            return "Warp worm";
        }

        public override void RunAction()
        {
            var caster = Context.MaybeCaster;
            var target = Context.MainTarget.Unit;

            List<UnitEntityData> targetList = Context.MaybeCaster.Memory.Enemies.Select(c => c.Unit).ToList();
            targetList.Sort((UnitEntityData u1, UnitEntityData u2) => u1.DistanceTo(caster).CompareTo(u2.DistanceTo(caster)));

            int modifier = caster.Stats.Dexterity.Bonus;

            while (count > 0 && targetList.Count > 0)
            {
                EtherGateTeleport.TeleportWithFx(caster, target.Position);

                var rule = new RuleDealDamage(caster, target, DamageTypes.Force().GetDamageDescriptor(new DiceFormula(10, DiceType.D6)).CreateDamage());
                var saving_rule = new RuleSavingThrow(target, Kingmaker.EntitySystem.Stats.SavingThrowType.Will, 17 + modifier);

                Context.TriggerRule(saving_rule);

                rule.Half = saving_rule.Success ? true : false;

                Context.TriggerRule(rule);

                targetList.Remove(target);

                target = targetList.First();

                count--;
            }

            EtherGateTeleport.TeleportWithFx(caster, EtherGateTeleport.AdjustTargetPoint(caster, target, target.Position));
            return;
        }
    }
}
