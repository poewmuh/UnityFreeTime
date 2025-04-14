using UnityEngine;

namespace GameFolder.Gameplay.UnitComponents
{
    public class StatsBoard : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _readyMark;

        public void ShowReadyMark()
        {
            _readyMark.enabled = true;
        }

        public void HideReadyMark()
        {
            _readyMark.enabled = false;
        }
    }
}