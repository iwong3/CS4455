using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuzzMenuScript : MonoBehaviour {

	Animator anim;
	float timer;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		timer = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;
		if (timer >= 5) {
			float rand = Random.value;
			if (rand >= .5f) {
				anim.SetTrigger ("Wave");
			} else {
				anim.SetTrigger ("Taunt");
			}
			timer = 0;
		}
	}
}
