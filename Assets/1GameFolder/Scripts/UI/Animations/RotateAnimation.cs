using DG.Tweening;
using TriInspector;
using UnityEngine;

namespace GameFolder.UI
{
    public class RotateAnimation : MonoBehaviour
    {
        [SerializeField] private float _rotateSpeed = 1;
        [SerializeField] private Vector3 _rotateVector;

        private void LateUpdate()
        {
            transform.Rotate(_rotateVector * _rotateSpeed);
        }

        
    }
}