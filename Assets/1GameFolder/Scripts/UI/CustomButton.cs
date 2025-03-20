using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameFolder.UI
{
    public class CustomButton : Selectable, IPointerClickHandler, ISubmitHandler
    {
        public UnityEvent onClick;

        [SerializeField] private TextMeshProUGUI _text;

        public override void OnPointerEnter(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject, eventData);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            _text.fontStyle |= FontStyles.Underline;
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            _text.fontStyle &= ~FontStyles.Underline;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CallOnClick();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            CallOnClick();
        }

        private void CallOnClick()
        {
            onClick?.Invoke();
        }
    }
}