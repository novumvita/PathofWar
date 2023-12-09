using Kingmaker.UnitLogic.Mechanics.Actions;
using UnityEngine;

namespace PathofWar.Components.VeiledMoon
{
    internal class TwistingEther : ContextAction
    {
        public override string GetCaption()
        {
            return "Twisting ether";
        }

        public override void RunAction()
        {
            Vector3 caster_pos = Context.MaybeCaster.Position;
            EtherGateTeleport.TeleportWithFx(Context.MaybeCaster, Context.MainTarget.Point);
            EtherGateTeleport.TeleportWithFx(Context.MainTarget.Unit, caster_pos);
        }
    }
}
