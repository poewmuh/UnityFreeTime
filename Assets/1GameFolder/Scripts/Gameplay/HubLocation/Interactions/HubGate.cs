using GameFolder.Gameplay.UnitComponents;

namespace GameFolder.Gameplay.HubLocation
{
    public class HubGate : BaseInteractObject
    {
        protected override void OnClicked(Unit unit)
        {
            HubGateManager.Instance.OnGateClicked(unit);
        }
    }
}