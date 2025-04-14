using System;
using GameFolder.Tools;
using UnityEngine;

namespace GameFolder.Gameplay
{
    public class CameraController : MonoSingleton<CameraController>
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _smoothTime;
        [SerializeField] private float _teleportDistance = 5;

        private Transform _focusable;
        
        public Camera gameCamera => _camera;

        public void SetFocusOn(Transform transform)
        {
            _focusable = transform;
        }

        public float GetAngleByMousePos(Vector3 position)
        {
            var screenPoint = Input.mousePosition;
            screenPoint.z = transform.position.y;
            var mousePosition = gameCamera.ScreenToWorldPoint(screenPoint);
            mousePosition.y = position.y;
            var direction  = mousePosition - position;
            direction = transform.rotation * direction;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
        }

        private void LateUpdate()
        {
            if (!_focusable) return;

            Vector3 desiredPosition = _focusable.position + _cameraOffset;
            if (Vector3.Distance(transform.position, desiredPosition) > _teleportDistance)
            {
                transform.position = desiredPosition;
            }
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothTime * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}