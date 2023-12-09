using Kingmaker.UnitLogic.Mechanics.Actions;

namespace PathofWar.Components.VeiledMoon
{
    internal class FadeThrough : ContextAction
    {
        public override string GetCaption()
        {
            return "Fade through";
        }

        public override void RunAction()
        {
            EtherGateTeleport.TeleportWithFx(Context.MaybeCaster, Context.MainTarget.Point);
        }
    }
}
