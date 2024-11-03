using System;
using Cinemachine;
using UnityEngine;

namespace ScriptsMisha
{
    public class CameraController : MonoBehaviour
    {
        public Transform _currentCamFollowTransform;
        private InputSystem _inputSystem;
        public Vector2 rightStickInput;
        public float mouseSense;
        public float xAxis, yAxis;
        private GameObject _enemyMid;
        private float _progressRotate;

        [SerializeField] private Transform _currentGun;

        [Header("RayCast Aim")]
        [SerializeField] private float _aimSmoothSpeed;
        [SerializeField] private LayerMask aimMask;
        public Transform aimPos;

        private void Update()
        {
            xAxis += rightStickInput.x * mouseSense;
            yAxis -= rightStickInput.y * mouseSense;
            yAxis = Mathf.Clamp(yAxis,-16, 18);

            Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            {
                aimPos.position = Vector3.Lerp(aimPos.position, hit.point, _aimSmoothSpeed * Time.deltaTime);
            }
        }

        private void LateUpdate()
        {
            _currentCamFollowTransform.localEulerAngles = new Vector3(yAxis,  _currentCamFollowTransform.localEulerAngles.y,
                _currentCamFollowTransform.localEulerAngles.z);
            
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
        }

        private void OnEnable()
        {
            if (_inputSystem == null)
            {
                _inputSystem = new InputSystem();
                _inputSystem.PlayerMovement.Camera.performed += i => rightStickInput = i.ReadValue<Vector2>();
            }
            
            _inputSystem.Enable();
        }

        private void OnDisable()
        {
            _inputSystem.Disable();
        }
    }
}
