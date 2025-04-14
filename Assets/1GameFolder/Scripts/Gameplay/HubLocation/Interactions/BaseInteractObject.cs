using UnityEngine;
using GameFolder.Gameplay.UnitComponents;

namespace GameFolder.Gameplay.HubLocation
{
    public abstract class BaseInteractObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _interractButton;

        protected abstract void OnClicked(Unit unit);
        
        private void OnTriggerEnter(Collider other)
        {
            var unit = other.GetComponent<Unit>();
            if (unit && unit.IsOwner)
            {
                EnableInteractions(unit);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var unit = other.GetComponent<Unit>();
            if (unit && unit.IsOwner)
            {
                DisableInteractions(unit);
            }
        }

        protected void DisableInteractions(Unit unit)
        {
            _interractButton.enabled = false;
            unit.Interactions.DisableInteractions();
            unit.Interactions.OnInteractionClicked -= OnClicked;
        }
        
        protected void EnableInteractions(Unit unit)
        {
            _interractButton.enabled = true;
            unit.Interactions.EnableInteractions();
            unit.Interactions.OnInteractionClicked += OnClicked;
        }
    }
}