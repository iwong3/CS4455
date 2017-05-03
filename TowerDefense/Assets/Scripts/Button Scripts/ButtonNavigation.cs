using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonNavigation : MonoBehaviour {
	int index = 0;
	public int totalButtons = 3; 
	public float yOffset = 61.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (index < totalButtons - 1) {
				index++;
				Vector2 position = transform.position;
				position.y -= yOffset;
				transform.position = position;
			} else if (index == totalButtons-1) {
				index = 0;
				Vector2 position = transform.position;
				position.y += (totalButtons-1) * yOffset;
				transform.position = position;
			}
		}
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (index > 0) {
				index--;
				Vector2 position = transform.position;
				position.y += yOffset;
				transform.position = position;
			} else if (index == 0) {
				index = 2;
				Vector2 position = transform.position;
				position.y -= (totalButtons-1) * yOffset;
				transform.position = position;
			}
		}
		if (Input.GetKeyDown (KeyCode.Return)) {
			switch (index){
			case 0:
				SceneManager.LoadScene ("Level1");
				break;
			case 1:
				SceneManager.LoadScene ("Options");
				break;
			case 2:
				SceneManager.LoadScene ("Credits");
				break;
			}
		}
	}
}
