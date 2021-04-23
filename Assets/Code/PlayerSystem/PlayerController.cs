using System;
using UnityEngine;

namespace UEGP3.PlayerSystem
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("General Settings")]
        [Tooltip("The speed with which the player moves forward.")]
        [SerializeField]
        private float _movementSpeed = 5.0f;
        [Tooltip("The speed used when the player sprints")]
        [SerializeField] 
        private float _sprintSpeed = 10.0f;
        [Tooltip("The graphical representation of the character. It is used for things like rotation")]
        [SerializeField] 
        private Transform _graphicsObject = null;
        
        [Header("Ground Check")]
        [Tooltip("A transform used to detect the ground")] 
        [SerializeField]
        private Transform _groundCheckTransform;
        [Tooltip("The radius around the transform which is used to detect the ground")]
        [SerializeField] 
        private float _groundCheckRadius = 0.05f;
        [Tooltip("A layermask used to exclude certain layers from the \"ground\"")]
        [SerializeField] 
        private LayerMask _excludeFromGroundCheckMask = default;
        [Tooltip("A modifier used to manipulate gravity influence for this object")] [SerializeField]
        private float _gravityModifier;

        [Header("Movement")] [Tooltip("Smoothing time for turns")] [SerializeField]
        private float _turnSmoothTime = 0.15f;
        [Tooltip("Smoothing time to reach target speed")] 
        [SerializeField]
        private float _speedSmoothTime = 0.25f;
        [Tooltip("The height in meters the character can jump")] [SerializeField]
        private float _jumpHeight;
        [Tooltip("The gravity modifier when falling")] [SerializeField]
        private float _fallingGravityMultiplier;
        [Tooltip("Velocity in m/s for the dash")] [SerializeField]
        private float _dashVelocity = 3.5f; 
        [Tooltip("Slider used to influence air control - 0 is no air control, 1 is full control")] 
        [Range(0, 1)] 
        [SerializeField]
        private float _airControl;

        private PlayerAnimationHandler _animationHandler;
        
        // Reference to Unitys Character Controller component
        private CharacterController _characterController;
        // Reference to the transform of the camera
        private Transform _cameraTransform;
        // bool to detect whether the character is grounded or not
        private bool _isGrounded;
        // Vertical Velocity used for jumping and falling
        private float _currentVerticalVelocity;
        // Current Velocity in forwards direction (speed)
        private float _currentForwardVelocity;
        // Used as ref value by SmoothDamp function
        private float _speedSmoothVelocity;
        
        private void Awake()
        {
            // Get dependencies on own GO
            _characterController = GetComponent<CharacterController>();
            _animationHandler = GetComponentInChildren<PlayerAnimationHandler>();
        }

        private void Start()
        {
            // Wait until Start() with external dependencies
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            // Fetch inputs
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            bool isSprinting = Input.GetButton("Sprint");
            bool jumpDown = Input.GetButtonDown("Jump");
            bool dashDown = Input.GetButtonDown("Fire1");
            
            // Calculate a direction from input data
            Vector3 direction = new Vector3(horizontalInput, 0, verticalInput).normalized;

            // If the player has given any input, adjust the characters rotation
            if (direction != Vector3.zero)
            {
                // Direction can be received by calculating the tan value of the input vector.
                // Drawing the input vector into a unit circle helps visualize this. Usually, 0° is represented by
                // the vector (1.0, 0.0). In our context (e.g. analogue stick) forward vector (0.0, 1.0) is 0°.
                // Use 90 - atan(y/x) or the equivalent: atan(x/y) to receive the correct values. 
                // Current rotation should always be relative to the cameras rotation, so take its yaw into account.
                float lookRotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _cameraTransform.eulerAngles.y;
                Quaternion targetRotation = Quaternion.Euler(0, lookRotationAngle, 0);
                _graphicsObject.rotation = Quaternion.Slerp(_graphicsObject.rotation, targetRotation, GetSmoothTimeAfterAirControl(_turnSmoothTime, false));
            }

            // Calculate velocity based on gravity formula: delta-y = 1/2 * g * t^2.
            // We ignore the 1/2 to safe a multiplication and because it feels better.
            // Second Time.deltaTime is done in controller.Move()-call so we save one multiplication
            if (_currentVerticalVelocity >= 0)
            {
                _currentVerticalVelocity += Physics.gravity.y * _gravityModifier * Time.deltaTime;
            }
            else
            {
                _currentVerticalVelocity += Physics.gravity.y * _gravityModifier * Time.deltaTime * _fallingGravityMultiplier;
            }
            
            // Determine target speed - running or sprinting?
            float targetSpeed = (isSprinting ? _sprintSpeed : _movementSpeed) * direction.magnitude;
            _currentForwardVelocity = Mathf.SmoothDamp(_currentForwardVelocity, targetSpeed, ref _speedSmoothVelocity, GetSmoothTimeAfterAirControl(_speedSmoothTime, true));
            if (dashDown)
            {
                _currentForwardVelocity = _dashVelocity;
            }
            // Calculate velocity vector based on gravity and speed
            Vector3 velocity = _graphicsObject.forward * _currentForwardVelocity + Vector3.up * _currentVerticalVelocity;

            // Move in direction of velocity
            _characterController.Move(velocity * Time.deltaTime);

            // Check if we are grounded, if so reset gravity
            _isGrounded = Physics.CheckSphere(_groundCheckTransform.position, _groundCheckRadius, _excludeFromGroundCheckMask);
            _animationHandler.SetGrounded(_isGrounded);
            if (_isGrounded && (velocity.y < 0))
            {
                _currentVerticalVelocity = -2f;
            }

            // If we are grounded and jump was pressed, jump
            if (_isGrounded && jumpDown)
            {
                _animationHandler.DoJump();
                _currentVerticalVelocity = Mathf.Sqrt(_jumpHeight * -2 * Physics.gravity.y);
            }
            
            _animationHandler.SetSpeeds(_currentForwardVelocity, _currentVerticalVelocity);
        }

        /// <summary>
        /// Calculates the smoothTime based on airControl.
        /// </summary>
        /// <param name="smoothTime">The initial smoothTIme</param>
        /// <param name="zeroControlIsMaxValue">If we do not have air control, is the smooth time float.MaxValue or float.MinValue?</param>
        /// <returns>The smoothTime after regarding air control</returns>
        private float GetSmoothTimeAfterAirControl(float smoothTime, bool zeroControlIsMaxValue)
        {
            // We are grounded, don't modify smoothTime
            if (_characterController.isGrounded)
            {
                return smoothTime;
            }

            // Avoid divide by 0 exception
            if (Math.Abs(_airControl) < Mathf.Epsilon)
            {
                return zeroControlIsMaxValue ? float.MaxValue : float.MinValue;
            }

            // smoothTime is influenced by air control
            return smoothTime / _airControl;
        }
    }
}
