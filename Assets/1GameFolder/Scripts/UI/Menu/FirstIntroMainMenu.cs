using DG.Tweening;
using UnityEngine;

namespace GameFolder.UI
{
    public class FirstIntroMainMenu : MonoBehaviour
    {
        private static bool isFirstIntro = true;

        [SerializeField] private CanvasGroup _buttonsHodler;

        private void Start()
        {
            var buttonsAlpha = isFirstIntro ? 0 : 1;
            gameObject.SetActive(isFirstIntro);
            isFirstIntro = false;
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                ShowButtonsAndDisable();
            }
        }

        private void ShowButtonsAndDisable()
        {
            gameObject.SetActive(false);
            _buttonsHodler.DOFade(1, 1);
        }
    }
}