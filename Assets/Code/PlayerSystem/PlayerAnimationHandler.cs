using System;
using UnityEngine;

namespace UEGP3.PlayerSystem
{
	/// <summary>
	/// Animation handler that observes several player states and translates them into animation states.
	/// </summary>
	[RequireComponent(typeof(Animator))]
	public class PlayerAnimationHandler : MonoBehaviour
	{
		// Cache all Animator-Strings because this is faster than using strings
		private static readonly int Jump = Animator.StringToHash("Jump");
		private static readonly int MovementSpeed = Animator.StringToHash("MovementSpeed");
		private static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
		private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
		
		// The animator used for the player character
		private Animator _animator;
		// Is the player currently grounded?
		private bool _isGrounded;
		// Current movement speed of the player
		private float _movementSpeed;
		// Current vertical velocity of the player
		private float _verticalVelocity;
		
		private void Awake()
		{
			_animator = GetComponentInChildren<Animator>();
		}

		private void Update()
		{
			// Set speed values each update
			_animator.SetFloat(MovementSpeed, _movementSpeed);
			_animator.SetFloat(VerticalVelocity, _verticalVelocity);
		}

		/// <summary>
		/// Tells the player whether the player is grounded or not
		/// </summary>
		/// <param name="grounded"></param>
		public void SetGrounded(bool grounded)
		{
			_isGrounded = grounded;
			_animator.SetBool(IsGrounded, _isGrounded);
		}
		
		/// <summary>
		/// Triggers the animator jump trigger
		/// </summary>
		public void DoJump()
		{
			_animator.SetTrigger(Jump);
		}

		/// <summary>
		/// Set the players velocities.
		/// </summary>
		/// <param name="movementSpeed">Players movement speed</param>
		/// <param name="verticalVelocity">Players vertical velocity</param>
		public void SetSpeeds(float movementSpeed, float verticalVelocity)
		{
			_movementSpeed = movementSpeed;
			_verticalVelocity = verticalVelocity;
		}
	}
}
