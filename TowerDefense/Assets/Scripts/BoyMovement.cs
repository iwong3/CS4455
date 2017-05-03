using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class BoyMovement : MonoBehaviour {


	Animator anim;
	CharacterController cc;

    [SerializeField]
    public AudioSource footsteps;


    [System.Serializable]
	public class PhysicsSettings
	{
		public float gravityModifier = 9.81f;
		public float baseGravity = 50.0f;
		public float resetGravity = 5.0f; //The force to pull upon falling
	}

	[SerializeField]
	public PhysicsSettings physics;

	[System.Serializable]
	public class MovementSettings
	{
		public float speed = 10f;
		public float jumpSpeed = 6.0f;
		public float jumpTime = 1.25f;
	}

	[SerializeField]
	public MovementSettings movement;

	bool isJumping;
	bool hasReset;
	float gravity;
	bool isGrounded = true;

	// Use for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		cc = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {

        if(anim.GetFloat("Speed")>0 && !footsteps.isPlaying)
        {
            footsteps.Play();
            footsteps.loop = true;
        }
        if (anim.GetFloat("Speed") == 0)
        {
            footsteps.Stop();
        }

		ApplyGravity ();
		isGrounded = cc.isGrounded;

	}

	public void Animate(float forward, float strafe){
		anim.SetFloat ("Speed", forward);
		anim.SetFloat ("Direction", strafe);
		anim.SetBool ("isJumping", isJumping);
		anim.SetBool ("isGrounded", isGrounded);
	}

	void ApplyGravity (){
		
		Vector3 gravityVector = new Vector3 ();

		if (!cc.isGrounded) {
			if (!hasReset) {
				gravity = physics.resetGravity;
				hasReset = true;
			}
			gravity += Time.deltaTime * physics.gravityModifier;
		} else {
			gravity = physics.baseGravity;
			hasReset = false;
		}

		if (!isJumping) {
			gravityVector.y -= gravity;
		} else {
			gravityVector.y = movement.jumpSpeed;
		}
		cc.Move (gravityVector * Time.deltaTime);
	}	

	public void Jump(){
		if (isJumping) {
			return;
		}
		if (isGrounded) {
			isJumping = true;
			StartCoroutine (StopJump());
		}
	}

	IEnumerator StopJump(){
		yield return new WaitForSeconds (movement.jumpTime);
		isJumping = false;
	}
}
