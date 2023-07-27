using Exiled.Events.EventArgs.Scp096;
using Exiled.Events.EventArgs.Warhead;

namespace PluginUtils.Plugins.SCP999.EventHandler
{
    public class ServerHandler
    {
        public void OnWarheadStop(StoppingEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                ev.IsAllowed = false;
            }
        }
        public void OnScpEnrage(EnragingEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                ev.IsAllowed = false;
            }
        }
        public void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (ev.Player.CustomInfo == "SCP-999")
            {
                ev.IsAllowed = false;
            }
        }
    }
}