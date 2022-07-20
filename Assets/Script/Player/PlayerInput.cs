using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 _move;
	public bool _attack;
	public bool _jump;
	public bool _dash;

#if ENABLE_INPUT_SYSTEM
	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnJump(InputValue value)
	{
		JumpInput(value.isPressed);
	}
	public void OnAttack(InputValue value)
	{
		AttackInput(value.isPressed);
	}
	public void OnDash(InputValue value)
	{
		DashInput(value.isPressed);
	}
#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


	public void MoveInput(Vector2 newMoveDirection)
	{
		_move = newMoveDirection;
	}

	public void JumpInput(bool newJumpState)
	{
		_jump = newJumpState;
	}
	public void AttackInput(bool newJumpState)
	{
		_attack = newJumpState;
	}
	public void DashInput(bool newDashState)
	{
		_dash = newDashState;
	}
}