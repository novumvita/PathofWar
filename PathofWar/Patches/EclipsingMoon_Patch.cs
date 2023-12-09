using HarmonyLib;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic.Commands.Base;
using PathofWar.Components.VeiledMoon;
using PathofWar.Utilities;
using System.Linq;
using UnityEngine;

namespace PathofWar.Patches
{
    internal class EclipsingMoon_Patch
    {
        private static readonly Logging.Logger Logger = Logging.GetLogger("EclipsingMoon");

        [HarmonyPatch(typeof(UnitCommand))]
        static class UnitCommand_Patch
        {
            [HarmonyPatch(nameof(UnitCommand.TickApproaching)), HarmonyPrefix]
            static bool TickApproaching(UnitCommand __instance)
            {
                UnitEntityData initiator = __instance.Executor;

                if (!initiator.Facts.List.Where(c => c.GetComponent<EclipsingMoon>()).Any())
                    return true;

                if (initiator.Position == __instance.ApproachPoint)
                    return true;

                var fact = initiator.Facts.List.Where(c => c.GetComponent<EclipsingMoon>()).Select(c => c.GetComponent<EclipsingMoon>()).First();

                if (fact.count < 1)
                    return true;

                float distance = Vector3.Distance(initiator.Position, __instance.ApproachPoint);
                if (fact.distance < distance)
                    return true;

                Vector3 point = EtherGateTeleport.AdjustTargetPoint(initiator, __instance.TargetUnit, __instance.ApproachPoint);

                Logger.Log("In patch");

                EtherGateTeleport.TeleportWithFx(initiator, __instance.ApproachPoint);

                __instance.FinishedApproaching = true;
                __instance.TurnToTarget();
                return false;
            }
        }
    }
}
