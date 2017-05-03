using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GanonMenuScript : MonoBehaviour {

	Animator anim;
	float timer;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		Time.timeScale = 1;
		timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;
		if (timer >= 5) {
			float rand = Random.value;
			if (rand >= .66f) {
				anim.SetTrigger ("Wave");
			} else if (rand >= .33f) {
				anim.SetTrigger ("Thump");
			} else {
				anim.SetTrigger ("Battlecry");
			}
			timer = 0;
		}
	}
}
