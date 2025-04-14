using GameFolder.Gameplay;
using UnityEngine;

namespace GameFolder.Tools
{
    public class LookAtCamera : MonoBehaviour
    {
        private void Start()
        {
            var cameraTransform = CameraController.Instance.transform;
            if (cameraTransform != null)
            {
                transform.LookAt(transform.position + cameraTransform.forward);
            }
        }
    }
}