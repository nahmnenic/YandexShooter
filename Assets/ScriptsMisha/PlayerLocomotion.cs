using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ScriptsMisha
{
    public class PlayerLocomotion : MonoBehaviour
    {
        private PlayerManager _palyerManager;
        private AnimatorManager _animatorManager;
        private InputManager inputManager;
        private Vector3 _moveDirection;
        private Transform _cameraObj;
        private Rigidbody _rb;

        [Header("Layer Check")]
        public LayerMask pozionLayer;
        public LayerMask slowLayer;

        [Header("Falling")] 
        public float inAirTimer;
        public float fallingVelocity;
        public float leapingVelocity;
        public float rayCastOffSet;
        public LayerMask groundLayer;

        [Header("Movement Flags")]
        public bool IsSprinting;
        public bool IsGrounded;
        public bool IsJumping;
        public bool IsCrouching;
        
        [Header("Other Speed")]
        public float pozionSpeed;
        public float slowlySpeed;

        [Header("Movement Speed")]
        public float walkingSpeed;
        public float runningSpeed;
        public float sprintingSpeed;
        public float aimingSpeed;

        [Header("Jump Speed")] 
        public float gravityIntensity;
        public float jumpHight;

        [Header("OnlyWalk")]
        public bool IsAiming;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            _rb = GetComponent<Rigidbody>();
            _animatorManager = GetComponent<AnimatorManager>();
            _palyerManager = GetComponent<PlayerManager>();
            _cameraObj = Camera.main.transform;
        }

        public void HandleAllMovement()
        {
            HandleFallingAndLanding();
            
            if (!IsJumping && !IsGrounded)
                return;
            HandleMovement();
        }
        
        private void HandleMovement()
        {
            if (IsJumping)
                return;
            
            _moveDirection = _cameraObj.forward * inputManager.verticalInput;
            _moveDirection += _cameraObj.right * inputManager.horizontalalInput;
            _moveDirection.Normalize();
            _moveDirection.y = 0;

            if (CheckSurface(pozionLayer))
            {
                _moveDirection *= pozionSpeed;
            }
            else if (CheckSurface(slowLayer))
            {
                _moveDirection *= slowlySpeed;
            }
            else
            {
                if (IsSprinting && IsGrounded && !IsAiming && !IsCrouching)
                {
                    _moveDirection *= sprintingSpeed;
                }
                else
                {
                    if (inputManager.moveAmount >= 0.5f && !IsAiming && !IsCrouching)
                    {
                        _moveDirection *= runningSpeed;
                    }
                    else if (IsAiming)
                    {
                        _moveDirection *= aimingSpeed;
                    }
                    else if (IsCrouching)
                    {
                        _moveDirection *= walkingSpeed;
                    }
                    else
                    {
                        _moveDirection *= walkingSpeed;
                    }
                }
            }

            Vector3 movementVelocity = _moveDirection;
            _rb.velocity = movementVelocity;
        }

        private void HandleFallingAndLanding()
        {
            RaycastHit hit;
            Vector3 rayCastOrirgin = transform.position;
            Vector3 targetPosition;
            rayCastOrirgin.y += rayCastOffSet;
            targetPosition = transform.position;
            
            if (!IsGrounded && !IsJumping)
            {
                inAirTimer += Time.deltaTime;
                _rb.AddForce(transform.forward * leapingVelocity);
                _rb.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
            }

            if (Physics.SphereCast(rayCastOrirgin, 0.2f, -Vector3.up, out hit,1f,groundLayer))
            {
                Vector3 rayCastHitPoint = hit.point;
                targetPosition.y = rayCastHitPoint.y;
                inAirTimer = 0;
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }

            if (IsGrounded && !IsJumping)
            {
                if (_palyerManager.isInteracting || inputManager.moveAmount > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
                }
                else
                {
                    transform.position = targetPosition;
                }
            }
        }

        public void HandleJumping()
        {
            if (IsGrounded)
            {
                IsJumping = true;
                StartCoroutine(TimeInJump());
                float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHight);
                Vector3 playerVelocity = _moveDirection;
                playerVelocity.y = jumpingVelocity;
                _rb.velocity = playerVelocity;
            }
        }

        private IEnumerator TimeInJump()
        {
            yield return new WaitForSeconds(0.2f);

            IsJumping = false;
        }
        
        private bool CheckSurface(int layer)
        {
            RaycastHit hit;
            Vector3 rayCastOrirgin = transform.position;
            rayCastOrirgin.y += rayCastOffSet;
            if (Physics.SphereCast(rayCastOrirgin, 0.2f, -Vector3.up, out hit,1f, layer))
            {
                return true;
            }

            return false;
        }
    }
}