using BlueprintCore.Blueprints.References;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Controllers.Projectiles;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.Visual.Particles;
using UnityEngine;

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

    internal static class EtherGateTeleport
    {
        private static AbilityCustomTeleportation ability = AbilityRefs.AeonTeleport_Cutscene.Reference.Get().GetComponent<AbilityCustomTeleportation>();
        public static void TeleportWithFx(UnitEntityData unit, Vector3 target_point)
        {
            var view = unit.View;

            FxHelper.SpawnFxOnUnit(ability.DisappearFx.Load(), view);

            var initial_position = unit.Position;
            unit.CombatState.PreventAttacksOfOpporunityNextFrame = true;
            unit.Position = target_point;

            Game.Instance.ProjectileController.Launch(unit, unit, ProjectileRefs.Mythic4lvlAeon_UncertanityPrinciple00.Reference.Get(), initial_position, delegate (Projectile projectile)
            {
            });

            FxHelper.SpawnFxOnUnit(ability.AppearFx.Load(), view);
        }
    }
}
