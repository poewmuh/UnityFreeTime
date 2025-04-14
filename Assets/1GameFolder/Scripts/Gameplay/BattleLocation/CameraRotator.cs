using UnityEngine;

namespace GameFolder.Gameplay.BattleLocation
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private Transform cameraRig;
        [SerializeField] private Transform target;
        [SerializeField] private float rotationSpeed = 5f;
        
        private bool _rotating = false;
        private Vector3 _prevMousePos;
        
        private void Update() 
        {
            if (Input.GetMouseButtonDown(2)) {
                _rotating = true;
                _prevMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(2)) 
            {
                _rotating = false;
            }

            if (_rotating) 
            {
                var delta = Input.mousePosition - _prevMousePos;
                _prevMousePos = Input.mousePosition;

                var angle = delta.x * rotationSpeed * Time.deltaTime;
                cameraRig.RotateAround(target.position, Vector3.up, angle);
            }
        }
    }
}