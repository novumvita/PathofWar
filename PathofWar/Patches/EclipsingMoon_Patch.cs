using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Commands.Base;
using Kingmaker.View;
using PathofWar.Components.VeiledMoon;
using PathofWar.Utilities;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Pathfinding.Util.RetainedGizmos;

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

                Vector3 point = __instance.ApproachPoint + (__instance.ApproachPoint - initiator.Position).normalized * __instance.ApproachRadius;

                Logger.Log("In patch");

                EtherGateTeleport.TeleportWithFx(initiator, __instance.ApproachPoint);

                __instance.FinishedApproaching = true;
                __instance.TurnToTarget();
                return false;
            }
        }
    }
}
