﻿using BlueprintCore.Blueprints.References;
using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker;
using Kingmaker.Blueprints;
using Kingmaker.Controllers.Combat;
using Kingmaker.Controllers.Projectiles;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Abilities.Components;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.Utility;
using Kingmaker.View;
using Kingmaker.Visual.Particles;
using Pathfinding;
using PathofWar.Components.VeiledMoon;
using PathofWar.Utilities;
using System;
using System.Linq;
using TurnBased.Controllers;
using UnityEngine;

namespace PathofWar.Patches
{
    internal class EtherGatePatch
    {
        private static AbilityCustomTeleportation ability = AbilityRefs.AeonTeleport_Cutscene.Reference.Get().GetComponent<AbilityCustomTeleportation>();
        private static readonly Logging.Logger Logger = Logging.GetLogger("EtherGate");

        [HarmonyPatch(typeof(UnitEntityView))]
        static class UnitEntityView_Patch
        {
            [HarmonyPatch(nameof(UnitEntityView.MoveTo)), HarmonyPrefix]
            static bool MoveTo(UnitEntityView __instance, UnitCommand command, Vector3 point, float approachRadius, float maxApproachRadius, [CanBeNull] UnitEntityView targetUnit, bool distanceXZ = false)
            {
                UnitEntityData initiator = __instance.Data;

                if (initiator.Facts.List.Where(c => c.GetComponent<EtherGate>()).Any())
                {
                    Logger.Log("Ether gate by: " + initiator);

                    FxHelper.SpawnFxOnUnit(ability.DisappearFx.Load(), __instance);

                    var initial_position = initiator.Position;

                    initiator.CombatState.PreventAttacksOfOpporunityNextFrame = true;
                    initiator.Position = point;

                    Game.Instance.ProjectileController.Launch(initiator, initiator, ProjectileRefs.Mythic4lvlAeon_UncertanityPrinciple00.Reference.Get(), initial_position, delegate (Projectile p)
                    {
                    });

                    FxHelper.SpawnFxOnUnit(ability.AppearFx.Load(), __instance);

                    return false;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(UnitMovementAgent))]
        static class UnitMovementAgent_Patch
        {
            [HarmonyPatch(nameof(UnitMovementAgent.FollowPrecomputedPath)), HarmonyPrefix]
            static bool FollowPrecomputedPath(UnitMovementAgent __instance, Path p, bool forcedPath, float maxApproachRadius = 0f)
            {

                UnitEntityView view = __instance.Unit;
                UnitEntityData initiator = view.Data;

                var controller = Game.Instance.TurnBasedCombatController.CurrentTurn;

                var final_position = p.vectorPath.Last();
                var initial_position = initiator.Position;
                var distance = Vector3.Distance(initial_position, final_position);
                var range = controller.GetRemainingMovementRange(initiator, true, false);
                Logger.Log("Remaining range is: " + range);

                if (Mathf.Approximately(controller.GetCooldown(initiator).MoveAction, 6f) || !controller.HasMovement(initiator))
                {
                    Logger.Log("Movement on cd");
                    return true;
                }

                if (distance > range)
                {
                    Logger.Log("Interpolated destination");
                    final_position = Vector3.Lerp(initial_position, final_position, range/distance);
                    distance = range;
                }

                if (initiator.Facts.List.Where(c => c.GetComponent<EtherGate>()).Any())
                {
                    Logger.Log("Ether gate by: " + initiator);


                    var component = initiator.Facts.List.Where(c => c.GetComponent<EtherGate>()).Select(c => c.GetComponent<EtherGate>()).First();
                    component.distance += distance;

                    Logger.Log("Distance set to:" + component.distance);

                    FxHelper.SpawnFxOnUnit(ability.DisappearFx.Load(), view);

                    initiator.CombatState.PreventAttacksOfOpporunityNextFrame = true;
                    initiator.Position = final_position;

                    Game.Instance.ProjectileController.Launch(initiator, initiator, ProjectileRefs.Mythic4lvlAeon_UncertanityPrinciple00.Reference.Get(), initial_position, delegate (Projectile projectile)
                    {
                    });

                    FxHelper.SpawnFxOnUnit(ability.AppearFx.Load(), view);

                    return false;
                }

                return true;
            }
        }

        [HarmonyPatch(typeof(TurnController))]
        static class TurnController_Patch
        {
            [HarmonyPatch(nameof(TurnController.GetCooldown)), HarmonyPostfix]
            static void GetCooldown(TurnController __instance, UnitEntityData unit, ref UnitCombatState.Cooldowns __result)
            {
                if (!unit.Facts.List.Where(c => c.GetComponent<EtherGate>()).Any())
                    return;

                var component = unit.Facts.List.Where(c => c.GetComponent<EtherGate>()).Select(c => c.GetComponent<EtherGate>()).First();

                if (component.distance == 0f)
                    return;

                Logger.Log("Patching getCooldown.");

                float time_moved = (component.distance / unit.CombatSpeedMps);

                if (component.distance < __instance.GetRemainingFiveFootStepRange(unit))
                {
                    Logger.Log("Treated as five foot step.");
                    __instance.SetMetersMovedByFiveFootStep(unit, component.distance);
                    component.distance = 0f;
                    return;
                }

                float move_cd = __result.MoveAction;

                __result.MoveAction = move_cd + time_moved;

                component.distance = 0f;

                Logger.Log("Move cooldown set to: " + __result.MoveAction);
            }
        }
    }
}
