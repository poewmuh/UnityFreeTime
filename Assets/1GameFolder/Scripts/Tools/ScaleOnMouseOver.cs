using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameFolder.Tools
{
    public class ScaleOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        [Header("Scale")]
    [SerializeField] private float _scaleUp = 1.1f;
    [SerializeField] private float _scaleDuration = 0.25f;

    [Header("Rotation")]
    [SerializeField] private float _rotationAngle = 10f;
    [SerializeField] private float _rotationSmooth = 12f;
    [SerializeField] private float _sensitivityX = 120f;
    [SerializeField] private float _sensitivityY = 120f;

    [SerializeField] private RectTransform _rect;
    
    private Vector3 _baseScale;
    private Quaternion _baseRotation;
    private bool _isHover;
    private Vector2 _cursorScreenPos;
    private Tween _scaleTween;

    private void Awake()
    {
        _baseScale = _rect.localScale;
        _baseRotation = _rect.localRotation;
    }

    public void OnPointerEnter(PointerEventData e)
    {
        _isHover = true;

        _scaleTween?.Kill();
        _scaleTween = _rect.DOScale(_baseScale * _scaleUp, _scaleDuration).SetEase(Ease.OutBack);
    }

    public void OnPointerExit(PointerEventData e)
    {
        _isHover = false;
        _scaleTween?.Kill();
        _rect.localScale   = _baseScale;
        _rect.localRotation = _baseRotation;
    }

    public void OnPointerMove(PointerEventData e) => _cursorScreenPos = e.position;

    private void Update()
    {
        if (!_isHover) return;

        Vector2 center = RectTransformUtility.WorldToScreenPoint(null, _rect.position);
        Vector2 offset = _cursorScreenPos - center;

        float xPercent = Mathf.Clamp(offset.x / _sensitivityX, -1f, 1f);
        float yPercent = Mathf.Clamp(offset.y / _sensitivityY, -1f, 1f);

        float xRot = -yPercent * _rotationAngle;
        float yRot =  xPercent * _rotationAngle;

        Quaternion target = Quaternion.Euler(xRot, yRot, 0);
        _rect.localRotation = Quaternion.Lerp(_rect.localRotation, target, Time.deltaTime * _rotationSmooth);
    }
    }
}