using System;
using GameFolder.Tools;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;

namespace GameFolder.Gameplay.UnitComponents
{
    public class SimpleMovement : MonoBehaviour
    {
        [SerializeField] private Transform _mesh;
        [SerializeField] private float _moveSpeed = 1;
        [SerializeField] private float _rotateSpeed = 3;

        private Unit _unit;
        private Transform _transformN;
        private Rigidbody _rb;
        private bool _isCanUpdate;

        private void Awake()
        {
            _transformN = transform;
            _rb = GetComponent<Rigidbody>();
        }

        public void Init(Unit unit)
        {
            _unit = unit;
            _isCanUpdate = true;
        }

        public void SetActiveMovement(bool isActive)
        {
            _isCanUpdate = isActive;
        }

        private void Update()
        {
            if (!_isCanUpdate || !_unit || !_unit.IsOwner) return;
            
            MoveUpdate();
            RotateUpdate();
        }

        private void MoveUpdate()
        {
            var moveX = Input.GetAxisRaw("Horizontal");
            var moveZ = Input.GetAxisRaw("Vertical");

            if (moveX == 0 && moveZ == 0) return;

            var cameraForward = CameraController.Instance.transform.forward;
            cameraForward.y = 0;
            var cameraRight = CameraController.Instance.transform.right;
            cameraRight.y = 0;
            var moveDir = (cameraForward * moveZ + cameraRight * moveX).normalized;
            var newPosition = _rb.position + moveDir * _moveSpeed * Time.fixedDeltaTime;
            _rb.MovePosition(newPosition);
        }

        private void RotateUpdate()
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, CameraController.Instance.GetAngleByMousePos(transform.position), 0));
            _mesh.rotation = Quaternion.Lerp(_mesh.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
        }
    }
}