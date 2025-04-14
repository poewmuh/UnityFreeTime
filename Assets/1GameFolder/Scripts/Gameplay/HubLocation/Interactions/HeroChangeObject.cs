using GameFolder.Gameplay.UnitComponents;

namespace GameFolder.Gameplay.HubLocation
{
    public class HeroChangeObject : BaseInteractObject
    {
        protected override void OnClicked(Unit unit)
        {
            HubLocationHUD.Instance.ShowChangeHeroWindow();
            DisableInteractions(unit);
        }
    }
}