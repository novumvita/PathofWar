using BlueprintCore.Blueprints.References;
using Kingmaker.Blueprints;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components.Base;
using Kingmaker.UnitLogic.Mechanics.Actions;
using Kingmaker.Visual.Particles;
using PathofWar.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PathofWar.Components.RadiantDawn
{
    internal class RadiantHeal : ContextAction
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("RadiantHeal");

        public override string GetCaption()
        {
            return "Healed by Radiance.";
        }

        public override void RunAction()
        {
            var amount = Context.MaybeCaster.Stats.Charisma.Bonus * (1 + Context.MaybeCaster.Progression.CharacterLevel / 4);
            var allyList = new List<UnitEntityData> { };
            foreach (UnitGroupMemory.UnitInfo unitInfo in Context.MaybeCaster.Memory.UnitsList)
            {
                UnitEntityData unit = unitInfo.Unit;
                Logger.Log("unit: " + unit);
                if (unit.IsAlly(Context.MaybeCaster) && unit.HPLeft < unit.MaxHP)
                    allyList.Add(unit);
            }
            allyList.Sort((UnitEntityData u1, UnitEntityData u2) => u1.HPLeft.CompareTo(u2.HPLeft));

            var validTargets = new List<UnitEntityData>(allyList);

            Logger.Log("validTargets: " + validTargets.Count);

            while (validTargets.Count > 0 && amount > 0)
            {
                var currentTarget = validTargets.FirstOrDefault();
                if (currentTarget == null)
                    break;

                Logger.Log("currentTarget: " + currentTarget);
                Logger.Log("amount: " + amount);

                var missingHP = currentTarget.MaxHP - currentTarget.HPLeft;
                var healingAmount = Math.Min(missingHP, amount);

                var res = Context.TriggerRule(new RuleHealDamage(Context.MaybeCaster, currentTarget, healingAmount) { Reason = Context });
                FxHelper.SpawnFxOnUnit(AbilityRefs.CureLightWounds.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink.Load(), currentTarget.View);

                amount -= healingAmount;
                validTargets.Remove(currentTarget);
            }

            if (amount > 0)
            {
                var res = Context.TriggerRule(new RuleHealDamage(Context.MaybeCaster, Context.MaybeCaster, amount) { Reason = Context });
                FxHelper.SpawnFxOnUnit(AbilityRefs.CureLightWounds.Reference.Get().GetComponent<AbilitySpawnFx>().PrefabLink.Load(), Context.MaybeCaster.View);
            }
        }
    }
}
