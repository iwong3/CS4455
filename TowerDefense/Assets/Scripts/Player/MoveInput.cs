using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveInput: MonoBehaviour
{
	public BoyMovement boyMove;

	[System.Serializable]
	public class InputSettings
	{
		public string verticalAxis = "Vertical";
		public string horizontalAxis = "Horizontal";
		public string jumpButton = "Jump";
	}
	[SerializeField]
	public InputSettings input;

	[System.Serializable]
	public class OtherSettings
	{
		public float lookSpeed = 5.0f;
		public float lookDistance = 500.0f;
		public bool requireInputForTurn = true;
	}
	[SerializeField]
	public OtherSettings other;

	Camera mainCam;

	// Use this for initialization
	void Start()
	{
		boyMove = GetComponent<BoyMovement>();
		mainCam = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		CharacterLogic();
		CameraLookLogic();
	}

	//Handles character logic
	void CharacterLogic()
	{
		if (!boyMove) {
			Debug.Log ("character move not set up !");
			return;
		}

		boyMove.Animate(Input.GetAxis(input.verticalAxis), Input.GetAxis(input.horizontalAxis));

		if (Input.GetButtonDown(input.jumpButton))
			boyMove.Jump();
	}

	//Handles camera logic
	void CameraLookLogic()
	{
		if (!mainCam){
			Debug.Log ("Main Camera not set up !");
			return;
		}

		if (other.requireInputForTurn) {
			if (Input.GetAxis (input.horizontalAxis) != 0 || Input.GetAxis (input.verticalAxis) != 0) {
				CharacterLook ();
			}
		}
		else {
			CharacterLook ();
		}
	}

	//Make the character look at a forward point from the camera
	void CharacterLook()
	{
		Transform mainCamT = mainCam.transform;
		Transform pivotT = mainCam.transform;
		Vector3 pivotPos = pivotT.position;
		Vector3 lookTarget = pivotPos + (pivotT.forward * other.lookDistance);
		Vector3 thisPos = transform.position;
		Vector3 lookDir = lookTarget - thisPos;
		Quaternion lookRot = Quaternion.LookRotation(lookDir);
		lookRot.x = 0;
		lookRot.z = 0;

		Quaternion newRotation = Quaternion.Lerp(transform.rotation, lookRot, Time.deltaTime * other.lookSpeed);
		transform.rotation = newRotation;
	}


}
