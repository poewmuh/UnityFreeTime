using System;
using GameFolder.Tools;
using TriInspector;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.UnitComponents
{
    [DeclareBoxGroup("PositionToSync", Title = "PositionToSync")]
    public class TransformSync : NetworkBehaviour
    {
        [SerializeField] private Transform _mesh;
        [SerializeField] private float _syncTickRate = .1f;
        [SerializeField] private float _syncSmoothSpeed = 6f;

        private float _syncTickTime;

        private float _previousRotation;
        private Vector3 _previousPosition;
        private float _serverYRotation;
        private Vector3 _serverPosition;
        private Transform _transform;
        private bool _waitFirstUpdate = true;

        private void Awake()
        {
            _transform = transform;
        }

        public override void OnNetworkSpawn()
        {
            _waitFirstUpdate = !IsOwner;
            if (_waitFirstUpdate)
            {
                GetPosAndRotRpc();
            }
            else
            {
                transform.position = Vector3.zero;
            }
        }

        private void Update()
        {
            if (_waitFirstUpdate) return;
            
            if (!IsOwner)
            {
                _transform.position = Vector3.Lerp(_transform.position, _serverPosition, _syncSmoothSpeed * Time.deltaTime);
                
                var yRotation = Mathf.LerpAngle(_mesh.eulerAngles.y, _serverYRotation, _syncSmoothSpeed * Time.deltaTime);
                _mesh.rotation = Quaternion.Euler(0, yRotation, 0);
                return;
            }
            
            _syncTickTime += Time.deltaTime;
            if (_syncTickTime > _syncTickRate)
            {
                _syncTickTime = 0;
                TrySyncPlayerPos();
                TrySyncPlayerRot();
            }
        }

        private void TrySyncPlayerPos()
        {
            var delta = _previousPosition.OnlyXZ() - _transform.position.OnlyXZ();
            if (delta.magnitude < 0.1f) return;
            SendPosRpc(_transform.position.x, _transform.position.z, _transform.position.y);
            _previousPosition = _transform.position;
        }

        private void TrySyncPlayerRot()
        {
            var delta = (_previousRotation - _mesh.eulerAngles.y).Abs();
            if (delta < 5) return;
            SendRotRpc(_mesh.eulerAngles.y);
            _previousRotation = _mesh.eulerAngles.y;
        }

        [Rpc(SendTo.NotMe)]
        private void SendPosRpc(float clientX, float clientZ, float clientY)
        {
            _serverPosition = new Vector3(clientX, clientY, clientZ);
            _waitFirstUpdate = false;
        }
        
        [Rpc(SendTo.NotMe)]
        private void SendRotRpc(float clientY)
        {
            _serverYRotation = clientY;
        }

        [Rpc(SendTo.Owner)]
        private void GetPosAndRotRpc()
        {
            SendPosRpc(_transform.position.x, _transform.position.z, _transform.position.y);
            SendRotRpc(_mesh.rotation.y);
        }
    }
}