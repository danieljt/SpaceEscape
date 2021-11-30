using UnityEngine;

[CreateAssetMenu(menuName = "StateMachine/States/DeadState")]
public class PlayerDeadState : StateMachineState
{
	public override void StateEnter(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{
		rbody.velocity = new Vector2(0, rbody.velocity.y);
		animator.SetBool(input.isAliveHash, false);
		owner.GetAudioSource.clip = owner.deathClip;
		owner.GetAudioSource.PlayOneShot(owner.deathClip);
	}

	public override void StateExit(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{

	}

	public override void StateFixedUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{

	}

	public override void StateUpdate(PlayerControllerDynamic2D owner, InputContext input, Rigidbody2D rbody, Animator animator)
	{

	}
}
